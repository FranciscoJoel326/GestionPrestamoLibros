using GestionPrestamoLibro.Components;
using GestionPrestamoLibro.Context;
using Microsoft.EntityFrameworkCore;
using P1_AP1_FranciscoSeverino.Components;
using P1_AP1_FranciscoSeverino.Dal;
using P1_AP1_FranciscoSeverino.Services;

namespace P1_AP1_FranciscoSeverino;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var ConStr = builder.Configuration.GetConnectionString("ConStr");

        builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlite(ConStr));

        builder.Services.AddScoped<AutoresServices>();

        var app = builder.Build();

        var factory = app.Services.GetRequiredService<IDbContextFactory<Contexto>>();
        using (var contexto = factory.CreateDbContext())
        {
            contexto.Database.EnsureCreated();
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}

