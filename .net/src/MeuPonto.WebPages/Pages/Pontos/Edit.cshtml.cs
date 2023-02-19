using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Pages.Pontos
{
    public class EditModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public EditModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Ponto Ponto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _db.Pontos == null)
            {
                return NotFound();
            }

            var ponto =  await _db.Pontos.FirstOrDefaultAsync(m => m.Id == id);
            if (ponto == null)
            {
                return NotFound();
            }
            Ponto = ponto;
           ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Matricula");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(Ponto).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PontoExists(Ponto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PontoExists(int? id)
        {
          return _db.Pontos.Any(e => e.Id == id);
        }
    }
}
