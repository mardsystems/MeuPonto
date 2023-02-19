using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Pages.Perfis
{
    public class DetailsModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public DetailsModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

      public Perfil Perfil { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _db.Perfis == null)
            {
                return NotFound();
            }

            var perfil = await _db.Perfis.FirstOrDefaultAsync(m => m.Id == id);
            if (perfil == null)
            {
                return NotFound();
            }
            else 
            {
                Perfil = perfil;
            }
            return Page();
        }
    }
}
