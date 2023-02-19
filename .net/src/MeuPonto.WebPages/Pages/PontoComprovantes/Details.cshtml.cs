using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Pages.PontoComprovantes
{
    public class DetailsModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public DetailsModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

      public PontoComprovante PontoComprovante { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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
