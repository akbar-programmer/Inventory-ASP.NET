using DOT_NET_MVC_INVENTORY.DataAccessLayer;
using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DOT_NET_MVC_INVENTORY.Controllers
{
    public class CustomerController : Controller
    {
        public CustomerGateway customerGateway { get; set; }
        public CustomerController(IConfiguration configuration)
        {
            customerGateway = new CustomerGateway(configuration);
        }
        public IActionResult Index()
        {
            List<Customer> customer = customerGateway.GetList();
            return View(customer);
        }
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            int res = customerGateway.Add(customer);
            return Redirect("/Customer");
        }
        [HttpGet]
        public ActionResult UpdateCustomer(int id)
        {
            Customer customer = customerGateway.GetById(id);
            return View(customer);
        }
        [HttpPost]
        public ActionResult UpdateCustomer(Customer customer)
        {
            int res = customerGateway.Update(customer);
            return Redirect("/Customer");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            int res = customerGateway.Delete(id);
            return Redirect("/Customer");
        }
    }
}
