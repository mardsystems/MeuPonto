using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;

namespace MeuPonto.Modules.Pontos.Folhas;

public class EditarModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Folha Folha { get; set; } = default!;

    public EditarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Folhas == null)
        {
            return NotFound();
        }

        var folha = await _db.Folhas.FirstOrDefaultAsync(m => m.Id == id);

        if (folha == null)
        {
            return NotFound();
        }

        Folha = folha;

        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid? id, string? command)
    {
        var transaction = User.CreateTransaction();

        Folha.RecontextualizaFolha(transaction, id);

        if (!ModelState.IsValid)
        {
            ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

            return Page();
        }

        var perfil = await _db.Perfis.FindByIdAsync(Folha.PerfilId, User.GetUserId());

        perfil.QualificaFolha(Folha);

        if (command == "ConfirmarCompetencia")
        {
            var states = ModelState.Where(state => state.Key.Contains($"{nameof(Folha.ApuracaoMensal)}"));

            foreach (var state in states)
            {
                if (ModelState.ContainsKey(state.Key)) ModelState.Remove(state.Key);
            }

            Folha.ApuracaoMensal.Dias.Clear();

            Folha.ConfirmarCompetencia(perfil);

            ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

            return Page();
        }

        if (Folha.ApuracaoMensal.Dias.Count == 0)
        {
            Folha.ConfirmarCompetencia(perfil);
        }

        Folha.RecontextualizaFolha(transaction);

        try
        {
            _db.Attach(Folha).State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FolhaExists(Folha.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        var detalharPage = Url.Page("Detalhar", new { id = Folha.Id });

        AddTempSuccessMessage("Folha editada com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }

    private bool FolhaExists(Guid? id)
    {
        return _db.Folhas.Any(e => e.Id == id);
    }
}
