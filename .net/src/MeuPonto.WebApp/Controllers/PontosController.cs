using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Controllers;

public class PontosController : Controller
{
    private readonly MeuPontoDbContext _db;

    public PontosController(MeuPontoDbContext db)
    {
        _db = db;
    }

    // GET: Pontos
    public async Task<IActionResult> Index()
    {
        var meuPontoDbContext = _db.Pontos.Include(p => p.Perfil);
        return View(await meuPontoDbContext.ToListAsync());
    }

    // GET: Pontos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _db.Pontos == null)
        {
            return NotFound();
        }

        var ponto = await _db.Pontos
            .Include(p => p.Perfil)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ponto == null)
        {
            return NotFound();
        }

        return View(ponto);
    }

    // GET: Pontos/Create
    public IActionResult Create()
    {
        ViewData["PerfilId"] = new SelectList(_db.Set<Perfil>(), "Id", "Nome");
        return View();
    }

    // POST: Pontos/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Data,PerfilId,Observacao,Id,CreationDate,Version")] Ponto ponto)
    {
        if (ModelState.IsValid)
        {
            _db.Add(ponto);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PerfilId"] = new SelectList(_db.Set<Perfil>(), "Id", "Nome", ponto.PerfilId);
        return View(ponto);
    }

    // GET: Pontos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _db.Pontos == null)
        {
            return NotFound();
        }

        var ponto = await _db.Pontos.FindAsync(id);
        if (ponto == null)
        {
            return NotFound();
        }
        ViewData["PerfilId"] = new SelectList(_db.Set<Perfil>(), "Id", "Nome", ponto.PerfilId);
        return View(ponto);
    }

    // POST: Pontos/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Data,PerfilId,Observacao,Id,CreationDate,Version")] Ponto ponto)
    {
        if (id != ponto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _db.Update(ponto);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PontoExists(ponto.Id))
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
        ViewData["PerfilId"] = new SelectList(_db.Set<Perfil>(), "Id", "Nome", ponto.PerfilId);
        return View(ponto);
    }

    // GET: Pontos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _db.Pontos == null)
        {
            return NotFound();
        }

        var ponto = await _db.Pontos
            .Include(p => p.Perfil)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ponto == null)
        {
            return NotFound();
        }

        return View(ponto);
    }

    // POST: Pontos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (_db.Pontos == null)
        {
            return Problem("Entity set 'MeuPontoDbContext.Pontos'  is null.");
        }
        var ponto = await _db.Pontos.FindAsync(id);
        if (ponto != null)
        {
            _db.Pontos.Remove(ponto);
        }
        
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PontoExists(int? id)
    {
      return _db.Pontos.Any(e => e.Id == id);
    }
}
