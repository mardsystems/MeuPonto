using MeuPonto.Data;
using MeuPonto.Modules.Trabalhadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Cosmos;
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
        var transaction = User.CreateTransaction();

        Comprovante = Trabalhador.Default.CriaComprovante(transaction);

        Comprovante.TipoImagemId = TipoImagemEnum.Original;

        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");
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
        var transaction = User.CreateTransaction();

        Trabalhador.Default.RecontextualizaComprovante(Comprovante, transaction);

        if (ModelState.ContainsKey($"{nameof(Comprovante)}.{nameof(Comprovante.PontoId)}")) ModelState.Remove($"{nameof(Comprovante)}.{nameof(Comprovante.PontoId)}");

        if (ModelState.ContainsKey($"{nameof(Comprovante)}.{nameof(Comprovante.Imagem)}")) ModelState.Remove($"{nameof(Comprovante)}.{nameof(Comprovante.Imagem)}");

        if (!ModelState.IsValid)
        {
            ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");
            return Page();
        }

        if (command == "Escanear")
        {
            ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");

            ModelState.Remove($"{nameof(Ponto)}.{nameof(Ponto.DataHora)}");

            Ponto.DataHora = new DateTime(2023, 02, 17, 17, 07, 0);

            //ModelState.Remove($"{nameof(Ponto)}.{nameof(Ponto.Momento)}");

            //Ponto.Momento = Momento.Saida;

            return Page();
        }
        else
        {
            Trabalhador.Default.RecontextualizaPonto(Ponto, transaction);

            var perfil = await _db.Perfis.FindByIdAsync(Ponto.PerfilId, Trabalhador.Default);

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

            try
            {
                await _db.SaveChangesAsync();

                return RedirectToPage("./Detalhar", new { id = Comprovante.Id });
            }
            catch (Exception _)
            {
                if (_.InnerException is CosmosException ex)
                {
                    if (ex.ResponseBody.Contains("Request size is too large"))
                    {
                        ModelState.AddModelError("Imagem", "Arquivo muito grande");

                        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");
                        return Page();
                    }
                }

                throw;
            }
        }
    }
}
