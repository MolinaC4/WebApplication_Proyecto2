using Microsoft.EntityFrameworkCore;
using WebApplication_Proyecto2.Models.Domain;
using System.Collections.Generic;

namespace WebApplication_Proyecto2.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Lretirados> Lretirados { get; set; }

        public DbSet<Lstock> Lstock { get; set; }
    }
}
