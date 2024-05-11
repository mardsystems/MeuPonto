using MeuPonto.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Models.Folhas;

namespace MeuPonto.Pages.Pontos.Folhas;

public class DetalharModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public Folha Folha { get; set; }

    public IList<DetalharFolhaDiaModel> Dias { get; set; } = default!;

    public DetalharModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Folhas == null)
        {
            return NotFound();
        }

        var folha = await _db.Folhas
            .Include(x => x.Contrato)
            .FirstOrDefaultAsync(x => x.Id == id);

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
                .Where(x => true
                    && x.DataHora >= competenciaAtual && x.DataHora < competenciaPosterior
                    && x.UserId == User.GetUserId())
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
