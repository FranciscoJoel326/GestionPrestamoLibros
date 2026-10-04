using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestionPrestamoLibro.Models;

public partial class Partidas
{
    [Key]
    public int DeudorId { get; set; }

    public string Nombres { get; set; } = null!;

    [InverseProperty("Deudor")]
    public virtual ICollection<Cobros> Cobros { get; set; } = new List<Cobros>();

    [InverseProperty("Deudor")]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}

