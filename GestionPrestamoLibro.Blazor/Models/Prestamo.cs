using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPrestamoLibro.Models;

public partial class Prestamo
{
    [Key]
    public int PrestamoId { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public int EstudianteId { get; set; } 

    [Range(1, int.MaxValue, ErrorMessage = "El concepto no puede ser menor a 1")]
    public double Balance { get; set; }

    public double LibroId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un deudor válido")]

    [ForeignKey("DeudorId")]
 
    public virtual Deudores Deudor { get; set; } = null!;
}


