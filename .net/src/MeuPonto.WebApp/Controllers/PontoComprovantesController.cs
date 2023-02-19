using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Controllers
{
    public class PontoComprovantesController : Controller
    {
        private readonly MeuPontoDbContext _db;

        public PontoComprovantesController(MeuPontoDbContext db)
        {
            _db = db;
        }

        // GET: PontoComprovantes
        public async Task<IActionResult> Index()
        {
            var meuPontoDbContext = _db.PontoComprovantes.Include(p => p.Ponto).Include(p => p.ImagemTipo);
            return View(await meuPontoDbContext.ToListAsync());
        }

        // GET: PontoComprovantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.PontoComprovantes == null)
            {
                return NotFound();
            }

            var pontoComprovante = await _db.PontoComprovantes
                .Include(p => p.Ponto)
                .Include(p => p.ImagemTipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontoComprovante == null)
            {
                return NotFound();
            }

            return View(pontoComprovante);
        }

        // GET: PontoComprovantes/Create
        public IActionResult Create()
        {
            ViewData["PontoId"] = new SelectList(_db.Pontos, "Id", "Data");
            ViewData["ImagemTipoId"] = new SelectList(_db.PontoComprovanteImagemTipos, "Id", "Nome");
            return View();
        }

        // POST: PontoComprovantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PontoId,Numero,Imagem,ImagemTipoId,Id,CreationDate,Version")] PontoComprovanteViewModel model)
        {
            if (ModelState.IsValid)
            {
                byte[] imagem;

                using (var memoryStream = new MemoryStream())
                {
                    await model.Imagem.CopyToAsync(memoryStream);

                    imagem = memoryStream.ToArray();
                }

                var pontoComprovante = new PontoComprovante()
                {
                    PontoId = model.PontoId,
                    Numero = model.Numero,
                    Imagem = imagem,
                    ImagemTipoId = model.ImagemTipoId
                };

                _db.Add(pontoComprovante);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PontoId"] = new SelectList(_db.Pontos, "Id", "Data", model.PontoId);
            ViewData["ImagemTipoId"] = new SelectList(_db.PontoComprovanteImagemTipos, "Id", "Nome", model.ImagemTipoId);
            return View(model);
        }

        // GET: PontoComprovantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.PontoComprovantes == null)
            {
                return NotFound();
            }

            var pontoComprovante = await _db.PontoComprovantes.FindAsync(id);
            if (pontoComprovante == null)
            {
                return NotFound();
            }
            ViewData["PontoId"] = new SelectList(_db.Pontos, "Id", "Data", pontoComprovante.PontoId);
            ViewData["ImagemTipoId"] = new SelectList(_db.PontoComprovanteImagemTipos, "Id", "Nome", pontoComprovante.ImagemTipoId);

            var model = new PontoComprovanteViewModel()
            {
                Id = pontoComprovante.Id,
                PontoId = pontoComprovante.PontoId,
                Numero = pontoComprovante.Numero,
                ImagemTipoId = pontoComprovante.ImagemTipoId,
                CreationDate = pontoComprovante.CreationDate,
                Version = pontoComprovante.Version
            };

            return View(model);
        }

        // POST: PontoComprovantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("PontoId,Numero,Imagem,ImagemTipoId,Id,CreationDate,Version")] PontoComprovanteViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                byte[] imagem;

                using (var memoryStream = new MemoryStream())
                {
                    await model.Imagem.CopyToAsync(memoryStream);

                    imagem = memoryStream.ToArray();
                }

                var pontoComprovante = new PontoComprovante()
                {
                    Id = model.Id,
                    PontoId = model.PontoId,
                    Numero = model.Numero,
                    Imagem = imagem,
                    ImagemTipoId = model.ImagemTipoId,
                    CreationDate = model.CreationDate,
                    Version = model.Version
                };

                try
                {
                    _db.Update(pontoComprovante);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontoComprovanteExists(pontoComprovante.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PontoId"] = new SelectList(_db.Pontos, "Id", "Data", model.PontoId);
            ViewData["ImagemTipoId"] = new SelectList(_db.PontoComprovanteImagemTipos, "Id", "Nome", model.ImagemTipoId);
            return View(model);
        }

        // GET: PontoComprovantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.PontoComprovantes == null)
            {
                return NotFound();
            }

            var pontoComprovante = await _db.PontoComprovantes
                .Include(p => p.Ponto)
                .Include(p => p.ImagemTipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontoComprovante == null)
            {
                return NotFound();
            }

            return View(pontoComprovante);
        }

        // POST: PontoComprovantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_db.PontoComprovantes == null)
            {
                return Problem("Entity set 'MeuPontoDbContext.PontoComprovantes'  is null.");
            }
            var pontoComprovante = await _db.PontoComprovantes.FindAsync(id);
            if (pontoComprovante != null)
            {
                _db.PontoComprovantes.Remove(pontoComprovante);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontoComprovanteExists(int? id)
        {
            return _db.PontoComprovantes.Any(e => e.Id == id);
        }
    }
}
