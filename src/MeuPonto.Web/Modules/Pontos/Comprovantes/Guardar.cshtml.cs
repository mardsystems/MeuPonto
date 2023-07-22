using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public class GuardarComprovanteModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    public GuardarComprovanteModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);

        var userId = Guid.Parse(nameIdentifier.Value);

        var transaction = new TransactionContext(userId);

        Comprovante = ComprovanteFactory.CriaComprovante(transaction);

        Comprovante.TipoImagemId = TipoImagemEnum.Original;

        ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");
        return Page();
    }

    [BindProperty]
    public Comprovante Comprovante { get; set; }

    [BindProperty]
    [Required]
    public IFormFile? Imagem { get; set; }

    [BindProperty]
    public Ponto Ponto { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(string? command)
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);

        var userId = Guid.Parse(nameIdentifier.Value);

        var transaction = new TransactionContext(userId);

        Comprovante.RecontextualizaComprovante(transaction);

        if (ModelState.ContainsKey($"{nameof(Comprovante)}.{nameof(Comprovante.PontoId)}")) ModelState.Remove($"{nameof(Comprovante)}.{nameof(Comprovante.PontoId)}");

        if (ModelState.ContainsKey($"{nameof(Comprovante)}.{nameof(Comprovante.Imagem)}")) ModelState.Remove($"{nameof(Comprovante)}.{nameof(Comprovante.Imagem)}");

        if (!ModelState.IsValid)
        {
            ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");
            return Page();
        }

        if (command == "Escanear")
        {
            ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");

            ModelState.Remove($"{nameof(Ponto)}.{nameof(Ponto.DataHora)}");

            Ponto.DataHora = new DateTime(2023, 02, 17, 17, 07, 0);

            //ModelState.Remove($"{nameof(Ponto)}.{nameof(Ponto.Momento)}");

            //Ponto.Momento = Momento.Saida;

            return Page();
        }
        else
        {
            Ponto.RecontextualizaPonto(transaction);

            var perfil = await _db.Perfis.FindByIdAsync(Ponto.PerfilId, nameIdentifier.Value);

            perfil.QualificaPonto(Ponto);

            _db.Pontos.Add(Ponto);

            await _db.SaveChangesAsync();

            //

            Comprovante.ComprovaPonto(Ponto);

            byte[] imagem;

            using (var memoryStream = new MemoryStream())
            {
                await Imagem.CopyToAsync(memoryStream);

                imagem = memoryStream.ToArray();
            }

            Comprovante.Imagem = imagem;

            Comprovante.TipoImagemId = TipoImagemEnum.Original;

            //var comprovante = new Comprovante()
            //{
            //    Id = Comprovante.Id,
            //    PartitionKey = Comprovante.PartitionKey,
            //    CreationDate = Comprovante.CreationDate,
            //    PontoId = Comprovante.PontoId,
            //    Ponto = Comprovante.Ponto,
            //    Numero = Comprovante.Numero,
            //    Imagem = imagem,
            //    ImagemTipoId = Comprovante.ImagemTipoId,
            //    ImagemTipo = Comprovante.ImagemTipo
            //};

            _db.Comprovantes.Add(Comprovante);

            //

            await _db.SaveChangesAsync();

            return RedirectToPage("./Detalhar", new { id = Comprovante.Id });
        }
    }
}
