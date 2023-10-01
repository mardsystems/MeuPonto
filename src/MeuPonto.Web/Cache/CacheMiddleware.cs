using MeuPonto.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace MeuPonto.Cache;

public class CacheMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IMemoryCache _cache;

    public CacheMiddleware(
        RequestDelegate next,
        IMemoryCache cache)
    {
        _next = next;

        _cache = cache;
    }

    public async Task Invoke(
        HttpContext context,
        MeuPontoDbContext db,
        ILogger<CacheMiddleware> logger)
    {
        var javascriptIsEnabled = await _cache.GetOrCreateAsync("JavascriptIsEnabled", async entry =>
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var nameIdentifier = context.User.FindFirst(ClaimTypes.NameIdentifier);

                var userId = Guid.Parse(nameIdentifier.Value).ToString();

                var configuracoes = await db.Configuracoes.FirstOrDefaultAsync(x => x.UserId == userId); //FindAsync(context.User.Identity.Name);

                if (configuracoes == null)
                {
                    return true;
                }
                else
                {
                    return configuracoes.JavascriptIsEnabled;
                }
            }
            else
            {
                return true;
            }
        });

        context.Items.Add("JavascriptIsEnabled", javascriptIsEnabled);

        await _next(context);
    }
}
