using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPrestamoLibro.Models;

public partial class Libro
{
    [Key]
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public int LibroId { get; set; }

    [Range(1, double.MaxValue, ErrorMessage = "Debe introducir un monto valido")]
    public double Monto { get; set; }

    [InverseProperty("Cobro")]
    public virtual ICollection<CobrosDetalle> CobrosDetalle { get; set; } = new List<CobrosDetalle>();

    [ForeignKey("DeudorId")]
    [InverseProperty("Cobros")]
    public virtual Deudores Deudor { get; set; } = null!;
}
