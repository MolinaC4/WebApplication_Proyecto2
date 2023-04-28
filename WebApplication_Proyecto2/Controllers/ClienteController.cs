using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_Proyecto2.Data;
using WebApplication_Proyecto2.Models;
using WebApplication_Proyecto2.Models.Domain;

namespace WebApplication_Proyecto2.Controllers
{
    public class ClienteController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public ClienteController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Cliente = await mvcDemoDbContext.Clientes.ToListAsync();
            return View(Cliente);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClienteViewModel addClienteRequest)
        {
            var cliente = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nombre = addClienteRequest.Nombre,
                N_Identificacion = addClienteRequest.N_Identificacion,
                Fec_Nac = addClienteRequest.Fec_Nac
            };
            await mvcDemoDbContext.Clientes.AddAsync(cliente);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var cliente = await mvcDemoDbContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            if (cliente != null)
            {
                var viewModel = new UpdateClienteViewModel()
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    N_Identificacion = cliente.N_Identificacion,
                    Fec_Nac = cliente.Fec_Nac
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateClienteViewModel model)
        {
            var cliente = await mvcDemoDbContext.Clientes.FindAsync(model.Id);
            if (cliente != null)
            {
                cliente.Nombre = model.Nombre;
                cliente.N_Identificacion = model.N_Identificacion;
                cliente.Fec_Nac = model.Fec_Nac;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateClienteViewModel model)
        {
            var cliente = await mvcDemoDbContext.Clientes.FindAsync(model.Id);
            if (cliente != null)
            {
                mvcDemoDbContext.Clientes.Remove(cliente);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
