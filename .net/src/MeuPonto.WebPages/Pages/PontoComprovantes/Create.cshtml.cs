using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeuPonto.Data;
using MeuPonto.Models;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Pages.PontoComprovantes
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
            ViewData["ImagemTipoId"] = new SelectList(_db.PontoComprovanteImagemTipos, "Id", "Nome");
            ViewData["PontoId"] = new SelectList(_db.Pontos, "Id", "Data");
            return Page();
        }

        [BindProperty]
        public PontoComprovante PontoComprovante { get; set; }

        [BindProperty]
        [Required]
        public IFormFile? Imagem { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
                PontoId = PontoComprovante.PontoId,
                Numero = PontoComprovante.Numero,
                Imagem = imagem,
                ImagemTipoId = PontoComprovante.ImagemTipoId
            };

            _db.PontoComprovantes.Add(pontoComprovante);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
