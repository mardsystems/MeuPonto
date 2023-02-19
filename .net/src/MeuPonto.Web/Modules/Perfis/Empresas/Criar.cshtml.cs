using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeuPonto.Modules.Perfis.Empresas;

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
    public Empresa Empresa { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Empresa.Id = Guid.NewGuid();

        Empresa.PartitionKey = User.Identity.Name; //Empresa.Empresa.Cnpj;

        Empresa.CreationDate = DateTime.Now;

        try
        {
            _db.Empresas.Add(Empresa);
            await _db.SaveChangesAsync();
        }
        catch (Exception _)
        {
            throw;
        }

        return RedirectToPage("./Detalhar", new { id = Empresa.Id });
    }
}
