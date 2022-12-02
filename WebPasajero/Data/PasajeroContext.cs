using Microsoft.EntityFrameworkCore;
using System;
using WebPasajero.Models;

namespace WebPasajero.Data
{
    public class PasajeroContext : DbContext
    {
        public PasajeroContext(DbContextOptions<PasajeroContext> options) : base(options) { }

        public DbSet<Pasajero> Pasajeros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pasajero>().HasData(
               new Pasajero
               {
                   PasajeroId = 1,
                   Nombre = "Matias",
                   Apellido = "Tito",
                   FechaNacimiento = new DateTime(1996,10,27),
                   Ciudad = "Mar del Plata"
               },
               new Pasajero
               {
                   PasajeroId = 2,
                   Nombre = "Tara",
                   Apellido = "Brewer",
                   FechaNacimiento = new DateTime(2000, 1, 1),
                   Ciudad = "Chaco"
               });

        }
    }
}
