using MeuPonto.Data;
using MeuPonto.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeuPonto.Features.GestaoFolha;
using MeuPonto.Models.Folhas;

namespace MeuPonto.Pages.Pontos.Folhas;

public class CriarModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Folha AberturaFolha { get; set; }

    public CriarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        var transaction = User.CreateTransaction();

        ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

        AberturaFolha = transaction.IniciarAberturaFolha();

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(string? command)
    {
        var transaction = User.CreateTransaction();

        transaction.RecontextualizaFolha(AberturaFolha);

        if (!ModelState.IsValid)
        {
            ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

            return Page();
        }

        AberturaFolha.StatusId = StatusFolhaEnum.Aberta;

        var contrato = await _db.Contratos.FindByIdAsync(AberturaFolha.ContratoId, User.GetUserId());

        AberturaFolha.AssociarAo(contrato);

        AberturaFolha.ConfirmarCompetencia(contrato);

        if (command == "ConfirmarCompetencia")
        {
            var states = ModelState.Where(state => state.Key.Contains($"{nameof(AberturaFolha.ApuracaoMensal)}"));

            foreach (var state in states)
            {
                if (ModelState.ContainsKey(state.Key)) ModelState.Remove(state.Key);
            }

            ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

            return Page();
        }

        transaction.RecontextualizaFolha(AberturaFolha);

        _db.Folhas.Add(AberturaFolha);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = AberturaFolha.Id });

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
