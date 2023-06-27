﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeuPonto.Modules.Perfis.Empregadores;

public class CriarModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public CriarModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Empregador Empregador { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var transaction = new TransactionContext(User.Identity.Name);

        Empregador.RecontextualizaEmpregador(transaction);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            _db.Empregadores.Add(Empregador);
            await _db.SaveChangesAsync();
        }
        catch (Exception _)
        {
            throw;
        }

        return RedirectToPage("./Detalhar", new { id = Empregador.Id });
    }
}