using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPrestamoLibro.Models;

public partial class PrestamoDetalle
{
    [Key]
    public int PrestamoDetalleId { get; set; }

    public int PrestamoId { get; set; }

    public int LibroId { get; set; }

    public int Cantidad { get; set; }

    [ForeignKey("PrestamoId")]
    [InverseProperty("PrestamoDetalle")]
    public virtual Prestamo Prestamo  { get; set; } = null!;
}

