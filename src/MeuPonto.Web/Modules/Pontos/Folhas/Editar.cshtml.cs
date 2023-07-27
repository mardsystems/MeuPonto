using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MeuPonto.Data;
using System.Security.Claims;
using MeuPonto.Modules.Trabalhadores;

namespace MeuPonto.Modules.Pontos.Folhas;

public class EditarFolhaModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    public EditarFolhaModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Folha Folha { get; set; } = default!;

    [BindProperty]
    [Required]
    [DisplayName("Ano")]
    public int? CompetenciaAno { get; set; }

    [BindProperty]
    [Required]
    [DisplayName("Mês")]
    public int? CompetenciaMes { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Folhas == null)
        {
            return NotFound();
        }

        var folha =  await _db.Folhas.FirstOrDefaultAsync(m => m.Id == id);
        if (folha == null)
        {
            return NotFound();
        }
        Folha = folha;

        CompetenciaAno = folha.Competencia.Value.Year;
        
        CompetenciaMes = folha.Competencia.Value.Month;

        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid? id, string? command)
    {
        var transaction = User.CreateTransaction();

        Trabalhador.Default.RecontextualizaFolha(Folha, transaction, id);

        if (ModelState.ContainsKey($"{nameof(Folha)}.{nameof(Folha.Competencia)}")) ModelState.Remove($"{nameof(Folha)}.{nameof(Folha.Competencia)}");

        if (!ModelState.IsValid)
        {
            ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");

            return Page();
        }

        var perfil = await _db.Perfis.FindByIdAsync(Folha.PerfilId, Trabalhador.Default);

        perfil.QualificaFolha(Folha);

        if (command == "ConfirmarCompetencia")
        {
            var states = ModelState.Where(state => state.Key.Contains($"{nameof(Folha.ApuracaoMensal)}"));

            foreach (var state in states)
            {
                if (ModelState.ContainsKey(state.Key)) ModelState.Remove(state.Key);
            }

            Folha.ConfirmarCompetencia(perfil, CompetenciaAno.Value, CompetenciaMes.Value);

            ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");

            return Page();
        }
        else
        {
            //ConfirmarCompetencia(perfil);

            var competenciaAtual = new DateTime(CompetenciaAno.Value, CompetenciaMes.Value, 1);

            Folha.Competencia = competenciaAtual;

            Folha.PartitionKey = $"{Trabalhador.Default.Id}|{Folha.Competencia:yyyy}";

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

            return RedirectToPage("./Detalhar", new { id = Folha.Id });
        }
    }

    private bool FolhaExists(Guid? id)
    {
      return _db.Folhas.Any(e => e.Id == id);
    }
}
