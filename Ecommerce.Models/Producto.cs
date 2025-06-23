using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [StringLength(500, ErrorMessage = "La descripcion no puede superar los 500 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La imagen  es obligatoria")]
        [StringLength(300, ErrorMessage = "La ruta de la imagen no puede superar los 300 caracteres ")]
        public string Imagen { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio"]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }


        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0, int.MaxValue, ErrorMessage = "la cantidad no puede ser negativa ")]
        public int CantidadDisponible { get; set; }

        [Required(ErrorMessage ="La categoria es obligatoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]

        public Categoria Categoria { get; set; }

    }
}
