using MeuPonto.Modules.Empregadores;
using MeuPonto.Modules.Trabalhadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Empregadores;

public class EditarModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public EditarModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Empregador Empregador { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        var empregador = await _db.Empregadores.FirstOrDefaultAsync(m => m.Id == id);
        if (empregador == null)
        {
            return NotFound();
        }
        Empregador = empregador;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        var transaction = User.CreateTransaction();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Trabalhador.Default.RecontextualizaEmpregador(Empregador, transaction, id);

        _db.Attach(Empregador).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmpregadorExists(Empregador.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Detalhar", new { id = Empregador.Id });
    }

    private bool EmpregadorExists(Guid? id)
    {
        return _db.Perfis.Any(e => e.Id == id);
    }
}
