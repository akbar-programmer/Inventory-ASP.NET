using DOT_NET_MVC_INVENTORY.DataAccessLayer;
using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DOT_NET_MVC_INVENTORY.Controllers
{
    public class BrandController : Controller
    {
        public BrandGateway brandGateway { get; set; }
        public BrandController(IConfiguration configuration)
        {
            brandGateway = new BrandGateway(configuration);
        }
        public IActionResult Index()
        {
            List<Brand> brand = brandGateway.GetList();
            return View(brand);
        }
        public ActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBrand(Brand brand)
        {
            int res = brandGateway.Add(brand);           
            return Redirect("/Brand");
        }
        [HttpGet]
        public ActionResult UpdateBrand(int id)
        {
            Brand brand = brandGateway.GetById(id);
            return View(brand);
        }
        [HttpPost]
        public ActionResult UpdateBrand(Brand brand)
        {
            int res = brandGateway.Update(brand);
            return Redirect("/brand");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            int res = brandGateway.Delete(id);
            return Redirect("/Brand");
        }
    }
}
