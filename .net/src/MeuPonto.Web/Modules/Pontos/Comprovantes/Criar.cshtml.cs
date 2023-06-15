using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public class CriarComprovanteModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public CriarComprovanteModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty(SupportsGet = true)]
    public Guid? PontoId { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Comprovante = new Comprovante();

        var ponto = await _db.Pontos.FindAsync(PontoId, User.Identity.Name);

        Comprovante.Ponto = new PontoRef
        {
            DataHora = ponto?.DataHora,
            PerfilId = ponto?.PerfilId,
            Perfil = ponto?.Perfil,
            MomentoId = ponto?.MomentoId,
            PausaId = ponto?.PausaId
        };

        return Page();
    }

    [BindProperty]
    public Comprovante Comprovante { get; set; }

    [BindProperty]
    [Required]
    public IFormFile? Imagem { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.ContainsKey($"{nameof(Comprovante)}.{nameof(Comprovante.Imagem)}")) ModelState.Remove($"{nameof(Comprovante)}.{nameof(Comprovante.Imagem)}");

        var ponto = await _db.Pontos.FindAsync(PontoId, User.Identity.Name);

        Comprovante.Ponto = new PontoRef
        {
            DataHora = ponto?.DataHora,
            PerfilId = ponto?.PerfilId,
            Perfil = ponto?.Perfil,
            MomentoId = ponto?.MomentoId,
            PausaId = ponto?.PausaId
        };

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Comprovante.Id = Guid.NewGuid();

        Comprovante.PartitionKey = User.Identity.Name;

        Comprovante.CreationDate = DateTime.Now;

        byte[] imagem;

        using (var memoryStream = new MemoryStream())
        {
            await Imagem.CopyToAsync(memoryStream);

            imagem = memoryStream.ToArray();
        }

        Comprovante.Imagem = imagem;

        //var comprovante = new Comprovante()
        //{
        //    Id = Comprovante.Id,
        //    PartitionKey = Comprovante.PartitionKey,
        //    CreationDate = Comprovante.CreationDate,
        //    PontoId = Comprovante.PontoId,
        //    Ponto = Comprovante.Ponto,
        //    Numero = Comprovante.Numero,
        //    Imagem = imagem,
        //    ImagemTipoId = Comprovante.ImagemTipoId,
        //    ImagemTipo = Comprovante.ImagemTipo
        //};

        _db.Comprovantes.Add(Comprovante);
        await _db.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
