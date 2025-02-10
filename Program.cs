// TODO Uninstall this package if there is a failure "Microsoft.VisualStudio.Web.CodeGeneration.Design"
// TODO Uninstall this gloabal package if codegenerator does not work for mysql "aspnet-codegenerator"
using LimsDepartementService.Data;
using Microsoft.EntityFrameworkCore;
using LimsDepartementService.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // This line is essential!

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<DepartementContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mySqlOptions => {
        mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

// For injection to controller
builder.Services.AddScoped<IDepartementService, DepartementService>();
builder.Services.AddScoped<IRecettePrevisionnelleService, RecettePrevisionnelleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // This line is crucial!

app.Run();
