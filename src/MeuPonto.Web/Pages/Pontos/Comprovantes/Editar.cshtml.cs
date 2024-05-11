using MeuPonto.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Features.BackupComprovantes;
using MeuPonto.Models.Pontos;

namespace MeuPonto.Pages.Pontos.Comprovantes;

public class EditarModel : FormPageModel
{
    private readonly Data.MeuPontoDbContext _db;

    [BindProperty]
    public Comprovante Comprovante { get; set; } = default!;

    [BindProperty]
    public IFormFile? Imagem { get; set; }

    public EditarModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Comprovantes == null)
        {
            return NotFound();
        }

        var comprovante = await _db.Comprovantes
            .Include(x => x.Ponto)
                .ThenInclude(x => x.Contrato)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (comprovante == null)
        {
            return NotFound();
        }

        Comprovante = comprovante;

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        var transaction = User.CreateTransaction();

        Comprovante.RecontextualizaComprovante(transaction, id);

        ModelState.Remove<EditarModel>(x => x.Comprovante.PontoId);
        ModelState.Remove<EditarModel>(x => x.Comprovante.Imagem);

        var comprovante = await _db.Comprovantes
            .Include(x => x.Ponto)
                .ThenInclude(x => x.Contrato)
            .FirstOrDefaultAsync(m => m.Id == Comprovante.Id);

        if (!ModelState.IsValid)
        {
            Comprovante.Ponto = comprovante.Ponto;

            Comprovante.Imagem = comprovante.Imagem;

            return Page();
        }

        byte[] imagem;

        if (Imagem == null)
        {
            imagem = comprovante.Imagem;
        }
        else
        {
            using (var memoryStream = new MemoryStream())
            {
                await Imagem.CopyToAsync(memoryStream);

                imagem = memoryStream.ToArray();
            }
        }

        comprovante.Imagem = imagem;
        comprovante.TipoImagemId = Comprovante.TipoImagemId;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PontoComprovanteExists(Comprovante.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        var detalharPage = Url.Page("Detalhar", new { id = Comprovante.Id });

        AddTempSuccessMessage("Comprovante editado com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }

    private bool PontoComprovanteExists(Guid? id)
    {
        return _db.Comprovantes.Any(e => e.Id == id);
    }
}
