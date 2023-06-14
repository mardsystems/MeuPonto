using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Controllers;

public class PerfisController : Controller
{
    private readonly MeuPontoDbContext _db;

    public PerfisController(MeuPontoDbContext db)
    {
        _db = db;
    }

    // GET: Perfis
    public async Task<IActionResult> Index()
    {
          return View(await _db.Perfis.ToListAsync());
    }

    // GET: Perfis/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        var perfil = await _db.Perfis
            .FirstOrDefaultAsync(m => m.Id == id);
        if (perfil == null)
        {
            return NotFound();
        }

        return View(perfil);
    }

    // GET: Perfis/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Perfis/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Matricula,Nome,Pis,Empresa,Id,CreationDate,Version")] Perfil perfil)
    {
        if (ModelState.IsValid)
        {
            _db.Add(perfil);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(perfil);
    }

    // GET: Perfis/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        var perfil = await _db.Perfis.FindAsync(id);
        if (perfil == null)
        {
            return NotFound();
        }
        return View(perfil);
    }

    // POST: Perfis/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Matricula,Nome,Pis,Id,CreationDate,Version")] Perfil perfil)
    {
        if (id != perfil.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _db.Update(perfil);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilExists(perfil.Id))
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
        return View(perfil);
    }

    // GET: Perfis/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        var perfil = await _db.Perfis
            .FirstOrDefaultAsync(m => m.Id == id);
        if (perfil == null)
        {
            return NotFound();
        }

        return View(perfil);
    }

    // POST: Perfis/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (_db.Perfis == null)
        {
            return Problem("Entity set 'MeuPontoDbContext.Perfis'  is null.");
        }
        var perfil = await _db.Perfis.FindAsync(id);
        if (perfil != null)
        {
            _db.Perfis.Remove(perfil);
        }
        
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PerfilExists(int? id)
    {
      return _db.Perfis.Any(e => e.Id == id);
    }
}
