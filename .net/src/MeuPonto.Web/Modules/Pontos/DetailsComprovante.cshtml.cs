using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;

namespace MeuPonto.Modules.Pontos
{
    public class DetailsComprovanteModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public DetailsComprovanteModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

      public PontoComprovante PontoComprovante { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _db.PontoComprovantes == null)
            {
                return NotFound();
            }

            var pontocomprovante = await _db.PontoComprovantes.FirstOrDefaultAsync(m => m.Id == id);
            if (pontocomprovante == null)
            {
                return NotFound();
            }
            else 
            {
                PontoComprovante = pontocomprovante;
            }
            return Page();
        }
    }
}
