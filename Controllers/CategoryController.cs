using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DOT_NET_MVC_INVENTORY.Controllers
{
    public class CategoryController : Controller
    {
        public AppDBContext dbContext { get; set; }
        public CategoryController()
        {
            dbContext = new AppDBContext();
        }
        public IActionResult Index()
        {
            List<Category> category = new List<Category>();
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "select * from Category";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Category cat = new Category();
                    cat.Id = Convert.ToInt32( reader["Id"].ToString());
                    cat.Name = reader["Name"].ToString();
                    category.Add(cat);
                }
                conn.Close();
            }
            return View(category);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "insert into Category (Name,IsActive) values (@Name,@IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", category.Name);
                if (category.IsActive)
                {
                    category.IsActive = true;
                }
                else
                {
                    category.IsActive = false;
                }
                cmd.Parameters.AddWithValue("@IsActive", category.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
            }

            return View();
        }

        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            Category category = new Category();
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "select * from Category where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    category.Name = reader["Name"].ToString();
                 
                }
                conn.Close();
            }
            return View(category);
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "update Category set Name=@Name,IsActive=@IsActive where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", category.Name);
                cmd.Parameters.AddWithValue("@IsActive", category.IsActive);
                cmd.Parameters.AddWithValue("@Id", category.Id);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
            }


            return Redirect("/Category");
        }
    }
}
