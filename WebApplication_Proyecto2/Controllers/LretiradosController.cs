using Microsoft.AspNetCore.Mvc;
using WebApplication_Proyecto2.Data;
using WebApplication_Proyecto2.Models.Domain;
using WebApplication_Proyecto2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication_Proyecto2.Controllers
{
    public class LretiradosController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public LretiradosController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Lretirados = await mvcDemoDbContext.Lretirados.ToListAsync();
            return View(Lretirados);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLretiradosViewModel addLretiradosRequest)
        {
            var lretirados = new Lretirados()
            {
                Id = Guid.NewGuid(),
                Nombre = addLretiradosRequest.Nombre,
                Descripcion = addLretiradosRequest.Descripcion,
                Cod_Cliente = addLretiradosRequest.Cod_Cliente,
                Fec_Retiro = addLretiradosRequest.Fec_Retiro
            };
            await mvcDemoDbContext.Lretirados.AddAsync(lretirados);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var lretirados = await mvcDemoDbContext.Lretirados.FirstOrDefaultAsync(x => x.Id == id);
            if (lretirados != null)
            {
                var viewModel = new UpdateLretiradosViewModel()
                {
                    Id = lretirados.Id,
                    Nombre = lretirados.Nombre,
                    Descripcion = lretirados.Descripcion,
                    Cod_Cliente = lretirados.Cod_Cliente,
                    Fec_Retiro = lretirados.Fec_Retiro
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateLretiradosViewModel model)
        {
            var lretirados = await mvcDemoDbContext.Lretirados.FindAsync(model.Id);
            if (lretirados != null)
            {
                lretirados.Nombre = model.Nombre;
                lretirados.Descripcion = model.Descripcion;
                lretirados.Cod_Cliente = model.Descripcion;
                lretirados.Fec_Retiro = model.Descripcion;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateLretiradosViewModel model)
        {
            var lretirados = await mvcDemoDbContext.Lretirados.FindAsync(model.Id);
            if (lretirados != null)
            {
                mvcDemoDbContext.Lretirados.Remove(lretirados);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
