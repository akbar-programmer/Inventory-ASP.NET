using Microsoft.AspNetCore.Mvc;
using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using DOT_NET_MVC_INVENTORY.DataAccessLayer;
using System.Collections.Generic;

namespace DOT_NET_MVC_INVENTORY.Controllers
{
	public class ItemController : Controller
	{
		public ItemGateway itemGateway { get; set; }
		public ItemController(IConfiguration configuration)
		{
			itemGateway = new ItemGateway(configuration);
		}
		public IActionResult Index()
		{
			List<Item> items = itemGateway.GetList();
			return View(items);
		}

		[HttpGet]
		public ActionResult AddItem()
		{
			List<Brand> brand = itemGateway.GetBrandList();
			List<Category> category = itemGateway.GetCategoryList();
			ViewBag.Category = category;
			ViewBag.Brand = brand;
			return View();
		}
		[HttpPost]
		public ActionResult AddItem(Item item)
		{
			int res = itemGateway.AddItem(item);
			return Redirect("/Item");
		}
		[HttpGet]
		public ActionResult UpdateItem(int id)
		{
			List<Brand> brand = itemGateway.GetBrandList();
			List<Category> category = itemGateway.GetCategoryList();
			Item item = itemGateway.GetById(id);
			ViewBag.Category = category;
			ViewBag.Brand = brand;
			return View(item);
		}
		[HttpPost]
		public ActionResult UpdateItem(Item item)
		{
			int res = itemGateway.Update(item);
			return Redirect("/Item");
		}
		[HttpGet]
		public ActionResult Delete(int id)
		{
			int res = itemGateway.Delete(id);
			return Redirect("/Item");
		}
	}
}
