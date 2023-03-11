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
    public class DeleteComprovanteModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public DeleteComprovanteModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _db.PontoComprovantes == null)
            {
                return NotFound();
            }
            var pontocomprovante = await _db.PontoComprovantes.FindAsync(id);

            if (pontocomprovante != null)
            {
                PontoComprovante = pontocomprovante;
                _db.PontoComprovantes.Remove(PontoComprovante);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
