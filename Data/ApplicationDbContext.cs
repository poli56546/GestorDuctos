using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestorDuctos.Models;

namespace GestorDuctos.Data
{
    // Cambiamos DbContext por IdentityDbContext para habilitar usuarios y roles
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Oficio> Oficios { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Procedimiento> Procedimientos { get; set; }
    }
}