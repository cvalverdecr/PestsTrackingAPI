using Microsoft.EntityFrameworkCore;
using PestTracking.Data;
using PestTracking.PestTrackingMappers;
using PestTracking.Repositorio;
using PestTracking.Repositorio.IRepositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexionSQL")));

//Agregamos los repositorios
builder.Services.AddScoped<IPaisRepositorio, PaisRepositorio>();
builder.Services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();

//agregamos el automapper
builder.Services.AddAutoMapper(typeof(PestsTrackingMapper));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();

