using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeuPonto.Data;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos
{
    public class CreateComprovanteModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public CreateComprovanteModel(MeuPonto.Data.MeuPontoDbContext db)
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

            PontoComprovante.Id = Guid.NewGuid();

            PontoComprovante.PartitionKey = User.Identity.Name;

            PontoComprovante.CreationDate = DateTime.Now;

            var ponto = await _db.Pontos.FindAsync(PontoComprovante.PontoId, User.Identity.Name);

            PontoComprovante.Ponto = new PontoRef
            {
                Data = ponto?.Data
            };

            var pontoComprovanteImagemTipo = await _db.PontoComprovanteImagemTipos.FindAsync(PontoComprovante.ImagemTipoId);

            PontoComprovante.ImagemTipo = new PontoComprovanteImagemTipoRef
            {
                Nome = pontoComprovanteImagemTipo?.Nome
            };

            byte[] imagem;

            using (var memoryStream = new MemoryStream())
            {
                await Imagem.CopyToAsync(memoryStream);

                imagem = memoryStream.ToArray();
            }

            PontoComprovante.Imagem = imagem;

            //var pontoComprovante = new PontoComprovante()
            //{
            //    Id = PontoComprovante.Id,
            //    PartitionKey = PontoComprovante.PartitionKey,
            //    CreationDate = PontoComprovante.CreationDate,
            //    PontoId = PontoComprovante.PontoId,
            //    Ponto = PontoComprovante.Ponto,
            //    Numero = PontoComprovante.Numero,
            //    Imagem = imagem,
            //    ImagemTipoId = PontoComprovante.ImagemTipoId,
            //    ImagemTipo = PontoComprovante.ImagemTipo
            //};

            _db.PontoComprovantes.Add(PontoComprovante);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Comprovantes");
        }
    }
}
