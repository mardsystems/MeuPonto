using MeuPonto.Data;
using MeuPonto.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timesheet.Models.Pontos.Folhas;

namespace MeuPonto.Pages.Pontos.Folhas;

public class CriarModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Folha Folha { get; set; }

    public CriarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        var transaction = User.CreateTransaction();

        ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

        Folha = FolhaFactory.CriaFolha(transaction);

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(string? command)
    {
        var transaction = User.CreateTransaction();

        Folha.RecontextualizaFolha(transaction);

        if (!ModelState.IsValid)
        {
            ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

            return Page();
        }

        Folha.StatusId = StatusFolhaEnum.Aberta;

        var contrato = await _db.Contratos.FindByIdAsync(Folha.ContratoId, User.GetUserId());

        contrato.QualificaFolha(Folha);

        Folha.ConfirmarCompetencia(contrato);

        if (command == "ConfirmarCompetencia")
        {
            var states = ModelState.Where(state => state.Key.Contains($"{nameof(Folha.ApuracaoMensal)}"));

            foreach (var state in states)
            {
                if (ModelState.ContainsKey(state.Key)) ModelState.Remove(state.Key);
            }

            ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

            return Page();
        }

        Folha.RecontextualizaFolha(transaction);

        _db.Folhas.Add(Folha);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = Folha.Id });

        AddTempSuccessMessageWithDetailLink("Folha criada com sucesso", detalharPage);

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
