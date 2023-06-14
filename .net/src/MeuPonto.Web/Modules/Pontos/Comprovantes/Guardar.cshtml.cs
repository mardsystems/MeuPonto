using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public class GuardarComprovanteModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public GuardarComprovanteModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        //Ponto.Id = Guid.NewGuid();

        //Comprovante.PontoId = Ponto.Id;
        //Comprovante.Id = Guid.NewGuid();

        Comprovante = new Comprovante();

        Comprovante.TipoImagem = TipoImagemEnum.Original;

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
            var agora = DateTime.Now;

            //

            Ponto.Id = Guid.NewGuid();

            Ponto.PartitionKey = User.Identity.Name; //Ponto.Data.ToString();

            Ponto.CreationDate = agora;

            var perfil = await _db.Perfis.FindAsync(Ponto.PerfilId, User.Identity.Name);

            Ponto.Perfil = new PerfilRef
            {
                Nome = perfil?.Nome
            };

            _db.Pontos.Add(Ponto);

            //

            Comprovante.Id = Guid.NewGuid();

            Comprovante.PartitionKey = User.Identity.Name;

            Comprovante.CreationDate = agora;

            var ponto = Ponto;

            Comprovante.PontoId = ponto.Id;

            Comprovante.Ponto = new PontoRef
            {
                DataHora = ponto?.DataHora,
                PerfilId = ponto?.PerfilId,
                Perfil = ponto?.Perfil,
                Momento = ponto?.Momento,
                Pausa = ponto?.Pausa
            };

            byte[] imagem;

            using (var memoryStream = new MemoryStream())
            {
                await Imagem.CopyToAsync(memoryStream);

                imagem = memoryStream.ToArray();
            }

            Comprovante.Imagem = imagem;

            Comprovante.TipoImagem = TipoImagemEnum.Original;

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
