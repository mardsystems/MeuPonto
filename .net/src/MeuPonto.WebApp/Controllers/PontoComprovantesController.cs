using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Controllers;

public class PontoComprovantesController : Controller
{
    private readonly MeuPontoDbContext _db;

    public PontoComprovantesController(MeuPontoDbContext db)
    {
        _db = db;
    }

    // GET: Comprovantes
    public async Task<IActionResult> Index()
    {
        var meuPontoDbContext = _db.Comprovantes.Include(p => p.Ponto).Include(p => p.ImagemTipo);
        return View(await meuPontoDbContext.ToListAsync());
    }

    // GET: Comprovantes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _db.Comprovantes == null)
        {
            return NotFound();
        }

        var comprovante = await _db.Comprovantes
            .Include(p => p.Ponto)
            .Include(p => p.ImagemTipo)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (comprovante == null)
        {
            return NotFound();
        }

        return View(comprovante);
    }

    // GET: Comprovantes/Create
    public IActionResult Create()
    {
        ViewData["PontoId"] = new SelectList(_db.Pontos, "Id", "Data");
        ViewData["ImagemTipoId"] = new SelectList(_db.PontoComprovanteImagemTipos, "Id", "Nome");
        return View();
    }

    // POST: Comprovantes/Create
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

            var comprovante = new Comprovante()
            {
                PontoId = model.PontoId,
                Numero = model.Numero,
                Imagem = imagem,
                ImagemTipoId = model.ImagemTipoId
            };

            _db.Add(comprovante);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PontoId"] = new SelectList(_db.Pontos, "Id", "Data", model.PontoId);
        ViewData["ImagemTipoId"] = new SelectList(_db.PontoComprovanteImagemTipos, "Id", "Nome", model.ImagemTipoId);
        return View(model);
    }

    // GET: Comprovantes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _db.Comprovantes == null)
        {
            return NotFound();
        }

        var comprovante = await _db.Comprovantes.FindAsync(id);
        if (comprovante == null)
        {
            return NotFound();
        }
        ViewData["PontoId"] = new SelectList(_db.Pontos, "Id", "Data", comprovante.PontoId);
        ViewData["ImagemTipoId"] = new SelectList(_db.PontoComprovanteImagemTipos, "Id", "Nome", comprovante.ImagemTipoId);

        var model = new PontoComprovanteViewModel()
        {
            Id = comprovante.Id,
            PontoId = comprovante.PontoId,
            Numero = comprovante.Numero,
            ImagemTipoId = comprovante.ImagemTipoId,
            CreationDate = comprovante.CreationDate,
            Version = comprovante.Version
        };

        return View(model);
    }

    // POST: Comprovantes/Edit/5
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

            var comprovante = new Comprovante()
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
                _db.Update(comprovante);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PontoComprovanteExists(comprovante.Id))
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

    // GET: Comprovantes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _db.Comprovantes == null)
        {
            return NotFound();
        }

        var comprovante = await _db.Comprovantes
            .Include(p => p.Ponto)
            .Include(p => p.ImagemTipo)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (comprovante == null)
        {
            return NotFound();
        }

        return View(comprovante);
    }

    // POST: Comprovantes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (_db.Comprovantes == null)
        {
            return Problem("Entity set 'MeuPontoDbContext.Comprovantes'  is null.");
        }
        var comprovante = await _db.Comprovantes.FindAsync(id);
        if (comprovante != null)
        {
            _db.Comprovantes.Remove(comprovante);
        }

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PontoComprovanteExists(int? id)
    {
        return _db.Comprovantes.Any(e => e.Id == id);
    }
}
