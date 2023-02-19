using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Pages.Pontos
{
    public class IndexModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public IndexModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

        public IList<Ponto> Ponto { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_db.Pontos != null)
            {
                Ponto = await _db.Pontos
                .Include(p => p.Perfil).ToListAsync();
            }
        }
    }
}
