using MeuPonto.Data;
using MeuPonto.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using MeuPonto.Features.BackupComprovantes;
using MeuPonto.Models.Pontos;

namespace MeuPonto.Pages.Pontos.Comprovantes;

public class CriarModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty(SupportsGet = true)]
    public Guid? PontoId { get; set; }

    [BindProperty]
    public Comprovante Comprovante { get; set; }

    [BindProperty]
    [Required]
    public IFormFile? Imagem { get; set; }

    public CriarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGet()
    {
        var transaction = User.CreateTransaction();

        Comprovante = BackupComprovantesFacade.CriaComprovante(transaction);

        var ponto = await _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefaultAsync(m => m.Id == PontoId);

        if (ponto != default)
        {
            Comprovante.ComprovaPonto(ponto);
        }

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var transaction = User.CreateTransaction();

        Comprovante.RecontextualizaComprovante(transaction);

        ModelState.Remove<CriarModel>(x => x.Comprovante.PontoId);
        ModelState.Remove<CriarModel>(x => x.Comprovante.Imagem);

        var ponto = await _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefaultAsync(m => m.Id == PontoId);

        Comprovante.ComprovaPonto(ponto);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        byte[] imagem;

        using (var memoryStream = new MemoryStream())
        {
            await Imagem.CopyToAsync(memoryStream);

            imagem = memoryStream.ToArray();
        }

        Comprovante.Imagem = imagem;

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

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = Comprovante.Id });

        AddTempSuccessMessageWithDetailLink("Comprovante criado com sucesso", detalharPage);

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }
}
