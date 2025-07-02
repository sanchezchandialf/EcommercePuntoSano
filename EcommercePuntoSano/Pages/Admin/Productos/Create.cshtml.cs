
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ecommerce.DataAccess;
using Ecommerce.Models;
using Ecommerce.DataAccess.Repository.Irepository;


namespace EcommercePuntoSano.Pages.Admin.Productos
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Producto Producto { get; set; } = new Producto();



        // Lista de categorías para el dropdown
        public IEnumerable<SelectListItem> Categorias { get; set; }

        public IActionResult OnGet()
        {
            // Carga las categorías desde la base de datos
            Categorias = _unitOfWork.Categoria.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                });

            //Validación por si la tabla categorías no tiene ni una sola categoría creada
            if (!Categorias.Any())
            {
                ModelState.AddModelError(string.Empty, "No hay categorías disponibles. Por favor, agregue categorías primero.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {


                    Categorias = _unitOfWork.Categoria.GetAll()
             .Select(c => new SelectListItem
             {
                 Value = c.Id.ToString(),
                 Text = c.Nombre
             });

            // Validación personalizada: comprobar si el nombre ya existe V 2.0 con Repository
            if (_unitOfWork.Producto.ExisteNombre(Producto.Nombre))
            {
                ModelState.AddModelError("Producto.Nombre", "El nombre del producto ya existe. Por favor elige otro.");
                return Page();
            }


            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Procesar la imagen subida
            if (Producto.ImagenSubida != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "productos");
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Producto.ImagenSubida.FileName);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Restricciones: Tamaño y formato
                if (Producto.ImagenSubida.Length > 2 * 1024 * 1024) // 2 mb
                {
                    ModelState.AddModelError("ImagenSubida", "El tamaño máximo permitido es de 2 MB.");
                    return Page();
                }

                //Extensiones permitidas
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(Path.GetExtension(Producto.ImagenSubida.FileName).ToLower()))
                {
                    ModelState.AddModelError("ImagenSubida", "El archivo debe ser una imagen (.jpg, .jpeg, .png, .gif).");
                    return Page();
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Producto.ImagenSubida.CopyTo(fileStream);
                }

                Producto.Imagen = uniqueFileName;
            }

            // Asignar la fecha de creación
            Producto.FechaCreacion = DateTime.Now;

            _unitOfWork.Producto.Add(Producto);
            _unitOfWork.Save();

            // Usar TempData para mostrar el mensaje en la página de índice
            TempData["Success"] = "Producto creado con éxito";

            return RedirectToPage("Index");
        }
    }
}
