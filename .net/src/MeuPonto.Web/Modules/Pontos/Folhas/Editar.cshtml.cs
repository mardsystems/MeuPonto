﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MeuPonto.Modules.Perfis;

namespace MeuPonto.Modules.Pontos.Folhas;

public class EditarFolhaModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public EditarFolhaModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Folha Folha { get; set; } = default!;

    [BindProperty]
    [Required]
    [DisplayName("Ano")]
    public int? CompetenciaAno { get; set; }

    [BindProperty]
    [Required]
    [DisplayName("Mês")]
    public int? CompetenciaMes { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Folhas == null)
        {
            return NotFound();
        }

        var folha =  await _db.Folhas.FirstOrDefaultAsync(m => m.Id == id);
        if (folha == null)
        {
            return NotFound();
        }
        Folha = folha;

        CompetenciaAno = folha.Competencia.Value.Year;
        
        CompetenciaMes = folha.Competencia.Value.Month;

        ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(string? command)
    {
        if (ModelState.ContainsKey($"{nameof(Folha)}.{nameof(Folha.Competencia)}")) ModelState.Remove($"{nameof(Folha)}.{nameof(Folha.Competencia)}");

        if (!ModelState.IsValid)
        {
            ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");

            return Page();
        }        

        var perfil = await _db.Perfis.FindAsync(Folha.PerfilId, User.Identity.Name);

        Folha.Perfil = new PerfilRef
        {
            Nome = perfil?.Nome
        };

        if (command == "ConfirmarCompetencia")
        {
            var states = ModelState.Where(state => state.Key.Contains($"{nameof(Folha.ApuracaoMensal)}"));

            foreach (var state in states)
            {
                if (ModelState.ContainsKey(state.Key)) ModelState.Remove(state.Key);
            }

            ConfirmarCompetencia(perfil);

            ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");

            return Page();
        }
        else
        {
            _db.Attach(Folha).State = EntityState.Modified;

            //ConfirmarCompetencia(perfil);

            var competenciaAtual = new DateTime(CompetenciaAno.Value, CompetenciaMes.Value, 1);

            Folha.Competencia = competenciaAtual;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PontoFolhaExists(Folha.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Detalhar", new { id = Folha.Id });
        }
    }

    private void ConfirmarCompetencia(Perfil? perfil)
    {
        Folha.ApuracaoMensal.Dias.Clear();

        var competenciaAtual = new DateTime(CompetenciaAno.Value, CompetenciaMes.Value, 1);

        Folha.Competencia = competenciaAtual;

        var competenciaPosterior = competenciaAtual.AddMonths(1);

        var dias = (competenciaPosterior - competenciaAtual).Days;

        Folha.ApuracaoMensal.TempoTotalPrevisto = TimeSpan.Zero;

        for (int dia = 1; dia <= dias; dia++)
        {
            var data = competenciaAtual.AddDays(dia - 1);

            var apuracaoDiaria = new ApuracaoDiaria
            {
                Dia = dia,
                TempoPrevisto = perfil.JornadaTrabalhoSemanalPrevista.Semana.Single(x => x.DiaSemana == data.DayOfWeek).Tempo,
                TempoApurado = null,
                DiferencaTempo = null,
                Feriado = false,
                Falta = false
            };

            Folha.ApuracaoMensal.Dias.Add(apuracaoDiaria);

            Folha.ApuracaoMensal.TempoTotalPrevisto += apuracaoDiaria.TempoPrevisto;
        }

        Folha.ApuracaoMensal.TempoTotalPeriodoAnterior = TimeSpan.Zero;
    }

    private bool PontoFolhaExists(Guid? id)
    {
      return _db.Folhas.Any(e => e.Id == id);
    }
}
