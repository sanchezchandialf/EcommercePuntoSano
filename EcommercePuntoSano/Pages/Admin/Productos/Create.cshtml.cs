using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ecommerce.DataAccess.Repository.Irepository;
using Ecommerce.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EcommercePuntoSano.Pages.Admin.Productos
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public Producto Producto { get; set; } = new();

        public IEnumerable<SelectListItem> Categorias { get; set; }

        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult OnGet()
        {
            // Carga las categorías para el dropdown.
            PopulateCategorias();

            // Si no hay categorías, añade un error para notificar al usuario.
            if (!Categorias.Any())
            {
                ModelState.AddModelError(string.Empty, "No hay categorías disponibles. Por favor, agregue una antes de crear un producto.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // 1. Validación personalizada: Comprobar si el nombre ya existe.
            if (_unitOfWork.Producto.ExisteNombre(Producto.Nombre))
            {
                ModelState.AddModelError("Producto.Nombre", "El nombre del producto ya existe. Por favor elige otro.");
            }

            // 2. Si el modelo no es válido (incluyendo nuestra validación personalizada).
            if (!ModelState.IsValid)
            {
                // Si la validación falla, debemos volver a cargar las categorías para mostrar el formulario correctamente.
                PopulateCategorias();
                return Page();
            }

            // 3. Procesar la imagen subida si existe.
            if (Producto.ImagenSubida != null)
            {
                // Validaciones de la imagen
                if (!IsImageValid(Producto.ImagenSubida))
                {
                    PopulateCategorias();
                    return Page();
                }

                // Guardar la imagen y obtener la ruta
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Producto.ImagenSubida.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\productos");

                // Asegurarse que el directorio exista
                if (!Directory.Exists(productPath))
                {
                    Directory.CreateDirectory(productPath);
                }

                // Creamos el FileStream y copiamos el archivo
                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    await Producto.ImagenSubida.CopyToAsync(fileStream);
                }

                // Guardamos la ruta completa relativa para usarla fácilmente en las etiquetas <img>
                Producto.Imagen = @"\images\productos\" + fileName;
            }

            // 4. Guardar el producto en la base de datos.
            _unitOfWork.Producto.Add(Producto);
            _unitOfWork.Save();

            TempData["Success"] = "¡Producto creado con éxito! 🎉";
            return RedirectToPage("Index");
        }

        /// <summary>
        /// Carga y prepara la lista de categorías para el dropdown.
        /// </summary>
        private void PopulateCategorias()
        {
            Categorias = _unitOfWork.Categoria.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                });
        }

        /// <summary>
        /// Valida el tamaño y la extensión del archivo de imagen.
        /// </summary>
        private bool IsImageValid(IFormFile image)
        {
            // Restricción de tamaño (ej: 2MB)
            if (image.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Producto.ImagenSubida", "El tamaño máximo de la imagen es de 2 MB.");
                return false;
            }

            // Restricción de extensión
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("Producto.ImagenSubida", "El archivo debe ser una imagen con formato .jpg, .jpeg, .png o .gif.");
                return false;
            }

            return true;
        }
    }
}