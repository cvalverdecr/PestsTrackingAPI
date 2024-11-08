using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PestTracking.Models;

namespace PestTracking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //aqui todos los modelos se deben agregar como Dbset(Modelos)
        public DbSet<Pais> Pais{ get; set; }
        public DbSet<Empresa> Empresa{ get; set; }
    }
}