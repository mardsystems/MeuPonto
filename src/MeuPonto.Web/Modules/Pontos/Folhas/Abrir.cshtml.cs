﻿using MeuPonto.Data;
using MeuPonto.Modules.Trabalhadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MeuPonto.Modules.Pontos.Folhas;

public class AbrirFolhaModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    public AbrirFolhaModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        var transaction = User.CreateTransaction();

        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");

        Folha = Trabalhador.Default.CriaFolha(transaction);

        Folha.StatusId = StatusEnum.Aberta;

        return Page();
    }

    [BindProperty]
    public Folha Folha { get; set; }

    [BindProperty]
    [Required]
    [DisplayName("Ano")]
    public int? CompetenciaAno { get; set; }

    [BindProperty]
    [Required]
    [DisplayName("Mês")]
    public int? CompetenciaMes { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(string? command)
    {
        var transaction = User.CreateTransaction();

        Trabalhador.Default.RecontextualizaFolha(Folha, transaction);

        if (ModelState.ContainsKey($"{nameof(Folha)}.{nameof(Folha.Competencia)}")) ModelState.Remove($"{nameof(Folha)}.{nameof(Folha.Competencia)}");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");

        Folha.StatusId = StatusEnum.Aberta;

        var perfil = await _db.Perfis.FindByIdAsync(Folha.PerfilId, Trabalhador.Default);

        perfil.QualificaFolha(Folha);

        if (command == "ConfirmarCompetencia")
        {
            var states = ModelState.Where(state => state.Key.Contains($"{nameof(Folha.ApuracaoMensal)}"));

            foreach (var state in states)
            {
                if (ModelState.ContainsKey(state.Key)) ModelState.Remove(state.Key);
            }

            Folha.ConfirmarCompetencia(perfil, CompetenciaAno.Value, CompetenciaMes.Value);

            ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");

            return Page();
        }
        else
        {
            var competenciaAtual = new DateTime(CompetenciaAno.Value, CompetenciaMes.Value, 1);

            if (Folha.Competencia == competenciaAtual)
            {
                Folha.PartitionKey = $"{Folha.TrabalhadorId}|{Folha.Competencia:yyyy}";

                _db.Folhas.Add(Folha);
                await _db.SaveChangesAsync();

                return RedirectToPage("./Detalhar", new { id = Folha.Id });
            }
            else
            {
                ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");

                return Page();
            }
        }
    }

}
