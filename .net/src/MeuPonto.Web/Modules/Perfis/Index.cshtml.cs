using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;

namespace MeuPonto.Modules.Perfis
{
    public class IndexModel : PageModel
    {
        private readonly MeuPonto.Data.MeuPontoDbContext _db;

        public IndexModel(MeuPonto.Data.MeuPontoDbContext db)
        {
            _db = db;
        }

        public IList<Perfil> Perfil { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_db.Perfis != null)
            {
                Perfil = await _db.Perfis.ToListAsync();
            }
        }
    }
}
