
using Data;
using Data.Repository.IRepository;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Data.Seeders;
using Business.Services.IService;
using Business.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBRemote"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceManagement, ServiceManagement>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });



var app = builder.Build();

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
