using DOT_NET_MVC_INVENTORY.DataAccessLayer;
using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DOT_NET_MVC_INVENTORY.Controllers
{
    public class SupplierController : Controller
    {
        public SupplierGateway supplierGateway { get; set; }
        public SupplierController(IConfiguration configuration)
        {
            supplierGateway = new SupplierGateway(configuration);
        }
        public IActionResult Index()
        {
            List<Supplier> supplier = supplierGateway.GetList();
            return View(supplier);
        }
        public ActionResult AddSupplier ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSupplier(Supplier supplier)
        {
            int res = supplierGateway.Add(supplier);
            return Redirect("/Supplier");
        }
        [HttpGet]
        public ActionResult UpdateSupplier(int id)
        {
            Supplier supplier = supplierGateway.GetById(id);
            return View(supplier);
        }
        [HttpPost]
        public ActionResult UpdateSupplier(Supplier supplier)
        {
            int res = supplierGateway.Update(supplier);
            return Redirect("/Supplier");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            int res = supplierGateway.Delete(id);
            return Redirect("/Supplier");
        }
    }
}
