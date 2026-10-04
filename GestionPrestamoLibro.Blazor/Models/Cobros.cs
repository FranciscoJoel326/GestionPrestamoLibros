using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPrestamoLibro.Models;

public partial class Libro
{
    [Key]
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public int LibroId { get; set; }
  
    [ForeignKey("DeudorId")]
    [InverseProperty("Cobros")]
    public virtual Deudores Deudor { get; set; } = null!;
}
