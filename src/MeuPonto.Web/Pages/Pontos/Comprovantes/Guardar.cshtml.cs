using MeuPonto.Data;
using MeuPonto.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Timesheet.Features.BackupComprovantes;
using Timesheet.Features.RegistroPontos;
using Timesheet.Models.Pontos;

namespace MeuPonto.Pages.Pontos.Comprovantes;

public class GuardarModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Comprovante Comprovante { get; set; }

    [BindProperty]
    [Required]
    public IFormFile? Imagem { get; set; }

    [BindProperty]
    public Ponto Ponto { get; set; }

    public GuardarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        var transaction = User.CreateTransaction();

        Comprovante = BackupComprovantesFacade.CriaComprovante(transaction);

        Comprovante.TipoImagemId = TipoImagemEnum.Original;

        ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(string? command)
    {
        var transaction = User.CreateTransaction();

        Comprovante.RecontextualizaComprovante(transaction);

        ModelState.Remove<EditarModel>(x => x.Comprovante.PontoId);
        ModelState.Remove<EditarModel>(x => x.Comprovante.Imagem);

        if (!ModelState.IsValid)
        {
            ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

            return Page();
        }

        if (command == "Escanear")
        {
            ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

            ModelState.Remove($"{nameof(Ponto)}.{nameof(Ponto.DataHora)}");

            Ponto.DataHora = new DateTime(2023, 02, 17, 17, 07, 0);

            //ModelState.Remove($"{nameof(Ponto)}.{nameof(Ponto.Momento)}");

            //Ponto.Momento = Momento.Saida;

            return Page();
        }

        Ponto.RecontextualizaPonto(transaction);

        var contrato = await _db.Contratos.FindByIdAsync(Ponto.ContratoId, User.GetUserId());

        contrato.QualificaPonto(Ponto);

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

            var detalharPage = Url.Page("Detalhar", new { id = Comprovante.Id });

            AddTempSuccessMessageWithDetailLink("Comprovante guardado com sucesso", detalharPage);

            if (ShouldRedirectToRefererPage())
            {
                return RedirectToRefererPage();
            }
            else
            {
                return Redirect(detalharPage);
            }
        }
        catch (Exception ex)
        {
            var message = ex.HandleException();

            if (message == "Request size is too large")
            {
                ModelState.AddModelError("Imagem", "Arquivo muito grande");

                ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

                return Page();
            }

            throw;
        }
    }
}
