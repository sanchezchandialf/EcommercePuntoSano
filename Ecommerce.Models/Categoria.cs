using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Categoria
    {
        [Key] // Indica que es la clave primaria
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        [Display(Name = "Nombre de la Categoría")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El orden de visualizacion es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El orden debe ser mayor a 1")]
        [Display(Name = "Orden de Visualizacion ")]
        public int OrdenVisualizacion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        //Relacion de uno a muchos : Una categoria puede tener muchos productos 
        public  ICollection <Producto> Productos { get; set; }
    }
}
