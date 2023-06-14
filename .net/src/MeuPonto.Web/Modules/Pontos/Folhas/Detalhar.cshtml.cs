using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Pontos.Folhas;

public class DetalharFolhaModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public DetalharFolhaModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public Folha Folha { get; set; }

    public IList<DetalharFolhaDiaModel> Dias { get; set; } = default!;

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
        else
        {
            Folha = folha;

            Dias = new List<DetalharFolhaDiaModel>();

            var competenciaAtual = Folha.Competencia.Value;

            var competenciaPosterior = competenciaAtual.AddMonths(1);

            var pontos = await _db.Pontos
                .Where(x => x.DataHora >= competenciaAtual && x.DataHora < competenciaPosterior)
                .ToListAsync();

            foreach (var apuracaoDiaria in Folha.ApuracaoMensal.Dias)
            {
                var detalharFolhaDiaModel = new DetalharFolhaDiaModel
                {
                    ApuracaoDiaria = apuracaoDiaria
                };

                Dias.Add(detalharFolhaDiaModel);
            }
        }

        return Page();
    }
}

public class DetalharFolhaDiaModel
{
    public ApuracaoDiaria ApuracaoDiaria { get; set; }
}
