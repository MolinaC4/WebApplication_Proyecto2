using Microsoft.AspNetCore.Mvc;
using WebApplication_Proyecto2.Data;
using WebApplication_Proyecto2.Models.Domain;
using WebApplication_Proyecto2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication_Proyecto2.Controllers
{
    public class LstockController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public LstockController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Lstock = await mvcDemoDbContext.Lstock.ToListAsync();
            return View(Lstock);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLstockViewModel addLstockRequest)
        {
            var lstock = new Lstock()
            {
                Id = Guid.NewGuid(),
                Descripcion = addLstockRequest.Descripcion,
                Precio = addLstockRequest.Precio,
                Cod_Cliente = addLstockRequest.Cod_Cliente,
                Fec_Ingreso = addLstockRequest.Fec_Ingreso
            };
            await mvcDemoDbContext.Lstock.AddAsync(lstock);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var lstock = await mvcDemoDbContext.Lstock.FirstOrDefaultAsync(x => x.Id == id);
            if (lstock != null)
            {
                var viewModel = new UpdateLstockViewModel()
                {
                    Id = lstock.Id,
                    Descripcion = lstock.Descripcion,
                    Precio = lstock.Precio,
                    Cod_Cliente = lstock.Cod_Cliente,
                    Fec_Ingreso = lstock.Fec_Ingreso
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateLstockViewModel model)
        {
            var lstock = await mvcDemoDbContext.Lstock.FindAsync(model.Id);
            if (lstock != null)
            {
                lstock.Descripcion = model.Descripcion;
                lstock.Precio = model.Precio;
                lstock.Cod_Cliente = model.Cod_Cliente;
                lstock.Fec_Ingreso = model.Fec_Ingreso;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateLstockViewModel model)
        {
            var lstock = await mvcDemoDbContext.Lstock.FindAsync(model.Id);
            if (lstock != null)
            {
                mvcDemoDbContext.Lstock.Remove(lstock);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
