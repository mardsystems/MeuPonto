using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeuPonto.Data;

namespace MeuPonto.Modules.Perfis
{
    public class CreateModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public CreateModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Perfil Perfil { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Perfis.Add(Perfil);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
