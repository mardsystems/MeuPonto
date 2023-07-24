﻿using MeuPonto.Data;
using MeuPonto.Helpers;
using MeuPonto.Modules.Trabalhadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MeuPonto.Modules.Perfis;

public class EditarModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public EditarModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Perfil Perfil { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        var perfil = await _db.Perfis.FirstOrDefaultAsync(m => m.Id == id);
        if (perfil == null)
        {
            return NotFound();
        }
        Perfil = perfil;

        ViewData["EmpregadorId"] = new SelectList(_db.Empregadores.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome").AddEmptyValue();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        var transaction = User.CreateTransaction();

        Trabalhador.Default.RecontextualizaPerfil(Perfil, transaction, id);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Perfil.EmpregadorId.HasValue)
        {
            var empregador = await _db.Empregadores.FindByIdAsync(Perfil.EmpregadorId, Trabalhador.Default);

            Perfil.VinculaEmpregador(empregador);
        }

        try
        {
            _db.Attach(Perfil).State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PerfilExists(Perfil.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        catch (Exception _)
        {

        }

        return RedirectToPage("./Detalhar", new { id = Perfil.Id });
    }

    private bool PerfilExists(Guid? id)
    {
        return _db.Perfis.Any(e => e.Id == id);
    }
}
