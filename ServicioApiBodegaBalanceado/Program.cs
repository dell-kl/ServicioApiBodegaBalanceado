
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
using Microsoft.IdentityModel.Tokens;

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
    if (context.Request.Query.TryGetValue("imagen", out var imagen) && context.Request.Query.TryGetValue("tipo", out var tipo)
    && !string.IsNullOrWhiteSpace(imagen) && !string.IsNullOrWhiteSpace(tipo))
    {
        Dictionary<string, string> rutasRecursos = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "catalogProduct", Path.Combine(Directory.GetCurrentDirectory(), "FilesPublic", "ImageCatalogProduction") },
            { "rawMaterial", Path.Combine(Directory.GetCurrentDirectory(), "FilesPublic", "ImageRawMaterial") }
        };

        if (!rutasRecursos.TryGetValue(tipo!, out var pathPartial))
            return Results.BadRequest("Tipo de recurso no válido.");

        string fileName = Path.GetFileName(imagen!);
        string fullname = Path.Combine(pathPartial, fileName);

        if (File.Exists(fullname))
            return Results.File(fullname, contentType: "image/png");

    }
    return Results.BadRequest("Error solicitud");
});

using (var db = app.Services.CreateScope())
{

    try
    {
        var services = db.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        new Seeder(context).cargarDatos();
    }
    catch (Exception)
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
