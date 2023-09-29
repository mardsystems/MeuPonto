using MeuPonto.Data;
using MeuPonto.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Perfis;

public class EditarModel : FormPageModel
{
    private readonly Data.MeuPontoDbContext _db;

    [BindProperty]
    public Perfil Perfil { get; set; } = default!;

    public EditarModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        var perfil = await _db.Perfis.FirstOrDefaultAsync(m => m.Id == id);

        if (perfil == null)
        {
            return NotFound();
        }

        Perfil = perfil;

        ViewData["EmpregadorId"] = new SelectList(_db.Empregadores.Where(x => x.UserId == User.GetUserId()), "Id", "Nome").AddEmptyValue();

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        var transaction = User.CreateTransaction();

        Perfil.RecontextualizaPerfil(transaction, id);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Perfil.EmpregadorId.HasValue)
        {
            var empregador = await _db.Empregadores.FindByIdAsync(Perfil.EmpregadorId, User.GetUserId());

            Perfil.VinculaEmpregador(empregador);
        }

        try
        {
            _db.Attach(Perfil).State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PerfilExists(Perfil.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        var detalharPage = Url.Page("Detalhar", new { id = Perfil.Id });

        AddTempSuccessMessage("Perfil editado com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }

    private bool PerfilExists(Guid? id)
    {
        return _db.Perfis.Any(e => e.Id == id);
    }
}
