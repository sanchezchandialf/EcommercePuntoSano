using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

public class Categoria
{
    [Key]
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

    [ValidateNever]
    public ICollection<Producto> Productos { get; set; }
}
