# PestsTrackingAPI
PestsTrackingAPI

Dotnet new --list
dotnet new webapi --name ApiPeliculas
dotnet build
dotnet run 

//Llave para configurar la cadena de conexión
 "ConnectionStrings": {
    "ConexionSql": "Server=localhost;Database=ApiPeliculasNET8;User Id=sa;Password=Asterix1973;Trusted_Connection=false;TrustServerCertificate=true;MultipleActiveResultSets=true"
  },


  //Instalar extensiones necesarias.
  dotnet add package Microsoft.EntityFrameworkCore --version 8.0.7
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.7
  dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.7


// Es necesario hacer la inyeccion del SQL a nivel del proyecto
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

//Para hacer una migracion

dotnet ef migrations add Initial
dotnet ef remove-migration
dotnet ef database update
dotnet ef dbcontext info
dotnet ef dbcontext list

command + shift + P = Nuget Add Nuget Package

Es necesario agrega los repositorios y el automapper al program.cs

control G multi cursos

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

instalar el paquete XAct.Core.PCL

instalar extensiones para versionamiento del ApiPeliculas
Asp.Versioning.Mvc
Asp.Versioning.Mvc.ApiExplorer

Agregar soporte para las versiones.


Agregar soporte para .Net identity
//Soporte para autenticacion con .Net Identity
builder.Services.AddIdentity<
IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
