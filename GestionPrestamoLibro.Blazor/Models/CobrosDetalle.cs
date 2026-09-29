using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPrestamoLibro.Models;

public partial class CobrosDetalle
{
    [Key]
    public int DetalleId { get; set; }

    public int CobroId { get; set; }

    public int PrestamoId { get; set; }

    public double ValorCobrado { get; set; }

    [ForeignKey("CobroId")]
    [InverseProperty("CobrosDetalle")]
    public virtual Cobros Cobro { get; set; } = null!;
}
