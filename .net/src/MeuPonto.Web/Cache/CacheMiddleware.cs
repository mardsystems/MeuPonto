using MeuPonto.Data;
using Microsoft.Extensions.Caching.Memory;

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
            var configuracoes = await db.Configuracoes.FindAsync(context.User.Identity.Name);

            if (configuracoes == null)
            {
                return true;
            }
            else
            {
                return configuracoes.JavascriptIsEnabled;
            }
        });

        context.Items.Add("JavascriptIsEnabled", javascriptIsEnabled);

        await _next(context);
    }
}
