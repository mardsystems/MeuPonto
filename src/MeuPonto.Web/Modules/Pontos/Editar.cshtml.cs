using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Pontos;

public class EditarModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    public EditarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Ponto Ponto { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Pontos == null)
        {
            return NotFound();
        }

        var ponto = await _db.Pontos.FirstOrDefaultAsync(m => m.Id == id);
        if (ponto == null)
        {
            return NotFound();
        }
        Ponto = ponto;

        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == User.GetUserId()), "Id", "Nome");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid? id, string layout)
    {
        var transaction = User.CreateTransaction();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var perfil = await _db.Perfis.FindByIdAsync(Ponto.PerfilId, User.GetUserId());

        perfil.QualificaPonto(Ponto);

        Ponto.RecontextualizaPonto(transaction, id);

        _db.Attach(Ponto).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PontoExists(Ponto.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Detalhar", new { id = Ponto.Id, layout });
    }

    private bool PontoExists(Guid? id)
    {
        return _db.Pontos.Any(e => e.Id == id);
    }
}
