using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos
{
    public class EditComprovanteModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public EditComprovanteModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public PontoComprovante PontoComprovante { get; set; } = default!;

        [BindProperty]
        [Required]
        public IFormFile? Imagem { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _db.PontoComprovantes == null)
            {
                return NotFound();
            }

            var pontocomprovante =  await _db.PontoComprovantes.FirstOrDefaultAsync(m => m.Id == id);
            if (pontocomprovante == null)
            {
                return NotFound();
            }
            PontoComprovante = pontocomprovante;
           ViewData["ImagemTipoId"] = new SelectList(_db.PontoComprovanteImagemTipos, "Id", "Nome");
           ViewData["PontoId"] = new SelectList(_db.Pontos, "Id", "Data");
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

            byte[] imagem;

            using (var memoryStream = new MemoryStream())
            {
                await Imagem.CopyToAsync(memoryStream);

                imagem = memoryStream.ToArray();
            }

            var pontoComprovante = new PontoComprovante()
            {
                Id = PontoComprovante.Id,
                PontoId = PontoComprovante.PontoId,
                Numero = PontoComprovante.Numero,
                Imagem = imagem,
                ImagemTipoId = PontoComprovante.ImagemTipoId,
                CreationDate = PontoComprovante.CreationDate,
                Version = PontoComprovante.Version
            };

            _db.Attach(pontoComprovante).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PontoComprovanteExists(PontoComprovante.Id))
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

        private bool PontoComprovanteExists(Guid? id)
        {
          return _db.PontoComprovantes.Any(e => e.Id == id);
        }
    }
}
