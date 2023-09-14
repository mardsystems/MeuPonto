using MeuPonto.Data;
using MeuPonto.Modules.Trabalhadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Folhas;

public class AbrirFolhaModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    public AbrirFolhaModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        var transaction = User.CreateTransaction();

        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == User.GetUserId()), "Id", "Nome");

        Folha = FolhaFactory.CriaFolha(transaction);

        Folha.StatusId = StatusEnum.Aberta;

        return Page();
    }

    [BindProperty]
    public Folha Folha { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(string? command)
    {
        var transaction = User.CreateTransaction();

        Folha.RecontextualizaFolha(transaction);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == User.GetUserId()), "Id", "Nome");

        Folha.StatusId = StatusEnum.Aberta;

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

            ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == User.GetUserId()), "Id", "Nome");

            return Page();
        }
        else
        {
            if (Folha.ApuracaoMensal.Dias.Count == 0)
            {
                Folha.ConfirmarCompetencia(perfil);
            }

            Folha.RecontextualizaFolha(transaction);

            _db.Folhas.Add(Folha);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Detalhar", new { id = Folha.Id });
        }
    }
}
