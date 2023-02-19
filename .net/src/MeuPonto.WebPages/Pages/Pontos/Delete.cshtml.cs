using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Pages.Pontos
{
    public class DeleteModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public DeleteModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

        [BindProperty]
      public Ponto Ponto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _db.Pontos == null)
            {
                return NotFound();
            }
            var ponto = await _db.Pontos.FindAsync(id);

            if (ponto != null)
            {
                Ponto = ponto;
                _db.Pontos.Remove(Ponto);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
