using Microsoft.EntityFrameworkCore;
using Pustok.Database;
using Pustok.Services.Abstracts;
using Pustok.Services.Concretes;

namespace Pustok;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Services
        builder.Services
            .AddControllersWithViews()
            .AddRazorRuntimeCompilation();

        builder.Services
            .AddDbContext<PustokDbContext>(o =>
            {
                o.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
            })
            .AddSingleton<IFileService, ServerFileService>()
            .AddScoped<IEmailService, MailkitEmailService>();

        var app = builder.Build();

        //Middleware (Chain of responsibily)
        app.UseStaticFiles();

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

        app.Run();
    }
}