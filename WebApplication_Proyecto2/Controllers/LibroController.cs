using Microsoft.AspNetCore.Mvc;
using WebApplication_Proyecto2.Data;
using WebApplication_Proyecto2.Models.Domain;
using WebApplication_Proyecto2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication_Proyecto2.Controllers
{
    public class LibroController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public LibroController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Libro = await mvcDemoDbContext.Libros.ToListAsync();
            return View(Libro);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLibroViewModel addLibroRequest)
        {
            var libro = new Libro()
            {
                Id = Guid.NewGuid(),
                Nombre = addLibroRequest.Nombre,
                Nombre_Empresa = addLibroRequest.Nombre_Empresa
            };
            await mvcDemoDbContext.Libros.AddAsync(libro);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var libro = await mvcDemoDbContext.Libros.FirstOrDefaultAsync(x => x.Id == id);
            if (libro != null)
            {
                var viewModel = new UpdateLibroViewModel()
                {
                    Id = libro.Id,
                    Nombre = libro.Nombre,
                    Nombre_Empresa = libro.Nombre_Empresa
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateLibroViewModel model)
        {
            var libro = await mvcDemoDbContext.Libros.FindAsync(model.Id);
            if (libro != null)
            {
                libro.Nombre = model.Nombre;
                libro.Nombre_Empresa = model.Nombre_Empresa;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateLibroViewModel model)
        {
            var libro = await mvcDemoDbContext.Libros.FindAsync(model.Id);
            if (libro != null)
            {
                mvcDemoDbContext.Libros.Remove(libro);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
