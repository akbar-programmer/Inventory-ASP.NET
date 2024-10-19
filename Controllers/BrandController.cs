using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DOT_NET_MVC_INVENTORY.Controllers
{
    public class BrandController : Controller
    {
        public AppDBContext dbContext { get; set; }
        public BrandController()
        {
            dbContext = new AppDBContext();
        }
        public IActionResult Index()
        {
            List<Brand> brand = new List<Brand>();
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "select * from Brand";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Brand newbrand = new Brand();
                    newbrand.Name = reader["Name"].ToString();
                    brand.Add(newbrand);
                }
                conn.Close();
            }
            return View(brand);
        }
        public ActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBrand(Brand brand)
        {
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "insert into Brand (Name,IsActive) values (@Name,@IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", brand.Name);
                if (brand.IsActive)
                {
                    brand.IsActive = true;
                }
                else
                {
                    brand.IsActive = false;
                }
                cmd.Parameters.AddWithValue("@IsActive", brand.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
            }

            return View();
        }
    }
}
