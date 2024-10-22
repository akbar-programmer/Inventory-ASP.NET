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
                    newbrand.Id = Convert.ToInt32(reader["Id"].ToString());
                    newbrand.Name = reader["Name"].ToString();
                    newbrand.IsActive = Convert.ToBoolean( reader["IsActive"].ToString());
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
                cmd.Parameters.AddWithValue("@IsActive", brand.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
            }

            return View();
        }
        [HttpGet]
        public ActionResult UpdateBrand(int id)
        {
            Brand brand = new Brand();
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "select * from Brand where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    brand.Name = reader["Name"].ToString();

                }
                conn.Close();
            }
            return View(brand);
        }
        [HttpPost]
        public ActionResult UpdateBrand(Brand brand)
        {
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "update Brand set Name=@Name,IsActive=@IsActive where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", brand.Name);
                cmd.Parameters.AddWithValue("@IsActive", brand.IsActive);
                cmd.Parameters.AddWithValue("@Id", brand.Id);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
            }


            return Redirect("/brand");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "delete from Brand where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int response = cmd.ExecuteNonQuery();

                conn.Close();
            }
            return Redirect("/Brand");
        }
    }
}
