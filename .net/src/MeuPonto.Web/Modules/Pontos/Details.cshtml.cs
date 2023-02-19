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
    public class DetailsModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public DetailsModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

      public Ponto Ponto { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _db.Pontos == null)
            {
                return NotFound();
            }

            var ponto = await _db.Pontos.FirstOrDefaultAsync(m => m.Id == id);
            if (ponto == null)
            {
                return NotFound();
            }
            else 
            {
                Ponto = ponto;
            }
            return Page();
        }
    }
}
