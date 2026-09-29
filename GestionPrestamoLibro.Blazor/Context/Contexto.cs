using GestionPrestamoLibro.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionPrestamoLibro.Context;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public virtual DbSet<Deudores> Deudores { get; set; }
    public virtual DbSet<Prestamos> Prestamos { get; set; }
    public virtual DbSet<Cobros> Cobros { get; set; }
    public virtual DbSet<CobrosDetalle> CobrosDetalle { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Deudores deudor1 = new Deudores();
        deudor1.DeudorId = 1;
        deudor1.Nombres = "Jose Lopez";

        Deudores deudor2 = new Deudores();
        deudor2.DeudorId = 2;
        deudor2.Nombres = "Maria Perez";

        List<Deudores> listaDeudores = new List<Deudores>();
        listaDeudores.Add(deudor1);
        listaDeudores.Add(deudor2);

        modelBuilder.Entity<Deudores>().HasData(listaDeudores);
        base.OnModelCreating(modelBuilder);
    }
}
