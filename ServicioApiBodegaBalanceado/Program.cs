
using Data;
using Data.Repository.IRepository;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Data.Seeders;
using Business.Services.IService;
using Business.Services.ProductService;
using Microsoft.AspNetCore.Http.HttpResults;
using Utility.DetectSO;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbSqlServerRemote"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceManagement, ServiceManagement>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = (context) =>
    {
        if (context.ProblemDetails.Status is 400)
        {
            context.ProblemDetails.Detail = "Error en tu solicitud, confirma que has completado todos tus datos respectivos y envialos nuevamente.";
        }
    };
});

var app = builder.Build();


//esta api minima va a ser de manera general para ver nuestras imagenes.
app.MapGet("/visor_imagenes", async (HttpContext context) =>
{
    context.Response.ContentType = "image/jpeg";
    var queryStrings = context.Request.Query;

    if (
        queryStrings!.Keys.Contains("imagen") && queryStrings!.Keys.Contains("tipo")
        &&
        queryStrings["imagen"].Any() && queryStrings["tipo"].Any()
    )
    {
        string? pathPartial = null;

        if (queryStrings["tipo"] == "catalogProduct")
            pathPartial = "\\FilesPublic\\ImageCatalogProduction";
        else if (queryStrings["tipo"] == "rawMaterial")
            pathPartial = "\\FilesPublic\\ImageRawMaterial";

        if (pathPartial is not null)
        {
            if (DetectSystemOperation.IsLinux())
                pathPartial = pathPartial!.Replace("\\", "//");

            string ruta = Path.Combine($"{Directory.GetCurrentDirectory()}{pathPartial}", queryStrings["imagen"]!);

            if (Path.Exists(ruta))
            {
                byte[] byteLists = System.IO.File.ReadAllBytes(ruta);

                await context.Response.Body.WriteAsync(byteLists, 0, byteLists.Length);

                return;
            }
        }

    }

    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync("Recurso no encontrado");
});

using (var db = app.Services.CreateScope())
{

    try
    {
        var services = db.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        new Seeder(context).cargarDatos();
    }
    catch (Exception e)
    {
        Console.WriteLine(" ====================================== ");
        Console.WriteLine("[!] No se puedo cargar datos iniciales");
        Console.WriteLine(" ====================================== ");
    }

}
// Configure the HTTP request pipeline.
//app.UseHttpsRedirection();
//app.UseHsts();

app.UseAuthorization();

app.MapControllers();

app.Run();
