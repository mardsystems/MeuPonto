using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;

namespace MeuPonto.Modules.Perfis
{
    public class DeleteModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public DeleteModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

        [BindProperty]
      public Perfil Perfil { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _db.Perfis == null)
            {
                return NotFound();
            }
            var perfil = await _db.Perfis.FindAsync(id);

            if (perfil != null)
            {
                Perfil = perfil;
                _db.Perfis.Remove(Perfil);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
