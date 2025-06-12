using System.ComponentModel.DataAnnotations;

namespace EcommercePuntoSano.Modelos
{
    public class Categoria
    {
        [Key] // Indica que es la clave primaria
        public int Id { get; set; }
        [Required (ErrorMessage ="El campo Nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        [Display(Name = "Nombre de la Categoría")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El orden de visualizacion es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        [Display(Name = "Orden de Visualizacion ")]
        public string OrdenVisualizacion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
