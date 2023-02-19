using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public class EditarComprovanteModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public EditarComprovanteModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Comprovante Comprovante { get; set; } = default!;

    [BindProperty]
    public IFormFile? Imagem { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Comprovantes == null)
        {
            return NotFound();
        }

        var comprovante = await _db.Comprovantes.FirstOrDefaultAsync(m => m.Id == id);
        if (comprovante == null)
        {
            return NotFound();
        }
        Comprovante = comprovante;

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.ContainsKey($"{nameof(Comprovante)}.{nameof(Comprovante.Imagem)}")) ModelState.Remove($"{nameof(Comprovante)}.{nameof(Comprovante.Imagem)}");

        var pontoRef = new PontoRef
        {
            PerfilId = Comprovante.Ponto?.PerfilId,
            Perfil = Comprovante.Ponto?.Perfil,
            DataHora = Comprovante.Ponto?.DataHora,
            Momento = Comprovante.Ponto?.Momento,
            Pausa = Comprovante.Ponto?.Pausa
        };

        var comprovante = await _db.Comprovantes.FirstOrDefaultAsync(m => m.Id == Comprovante.Id);

        if (!ModelState.IsValid)
        {
            Comprovante.Ponto = pontoRef;

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

        comprovante.PontoId = Comprovante.PontoId;
        comprovante.Ponto = Comprovante.Ponto;
        comprovante.Numero = Comprovante.Numero;
        comprovante.Imagem = imagem;
        comprovante.TipoImagem = Comprovante.TipoImagem;

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

        return RedirectToPage("./Index");
    }

    private bool PontoComprovanteExists(Guid? id)
    {
        return _db.Comprovantes.Any(e => e.Id == id);
    }
}
