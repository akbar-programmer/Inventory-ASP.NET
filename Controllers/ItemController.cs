using Microsoft.AspNetCore.Mvc;
using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DOT_NET_MVC_INVENTORY.Controllers
{
    public class ItemController : Controller
    {
        public AppDBContext dbContext { get; set; }
        public ItemController() {
            dbContext=new AppDBContext();
        }

        public IActionResult Index()
        {

            List<Item> items = new List<Item>();
            //{
            //    new Item
            //    {
            //        Id = 1,
            //        ItemCode="I-1001",
            //        Name="Monitor",
            //        Description="Periperal items",
            //        Category="IT",
            //        Brand="HP"
            //    },
            //      new Item
            //    {
            //        Id = 2,
            //        ItemCode="I-1002",
            //        Name="Mouse",
            //        Description="Periperal items",
            //        Category="IT",
            //        Brand="DELL"
            //    }
            //};


            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "select i.*,c.Name CategoryName,b.Name BrandName from Item i join Category c on c.Id = i.CategoryId join Brand b on b.id=i.BrandId";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Item item = new Item();
                    item.Id = Convert.ToInt32(reader["Id"].ToString());
                    item.ItemCode = reader["ItemCode"].ToString();
                    item.Name = reader["Name"].ToString();
                    item.Description = reader["Description"].ToString();
                    item.BrandId = reader["BrandName"].ToString();
                    item.CategoryId =reader["CategoryName"].ToString();
                    //item.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                    items.Add(item);
                }
                conn.Close();
            }

            return View(items);
        }
        [HttpGet]
        public ActionResult AddItem()
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
                    Category category1 = new Category();
                    category1.Id = Convert.ToInt32(reader["Id"].ToString());
                    category1.Name = reader["Name"].ToString();
                    category.Add(category1);
                }
                conn.Close();
            }
           

            List<Brand> brand = new List<Brand>();
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "select * from Brand";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Brand brand1 = new Brand();
                    brand1.Id = Convert.ToInt32(reader["Id"].ToString());
                    brand1.Name = reader["Name"].ToString();
                    brand.Add(brand1);
                }
                conn.Close();
            }
            ViewBag.Category = category;
            ViewBag.Brand = brand;

            return View();
        }
        [HttpPost]
        public ActionResult AddItem(Item item)
        {
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "insert into item (ItemCode,Name,CategoryId,BrandId,Description,IsActive) values (@ItemCode,@Name,@CategoryId,@BrandId,@Description,@IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemCode", item.ItemCode);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@CategoryId",item.CategoryId);
                cmd.Parameters.AddWithValue("@BrandId", item.BrandId);
                cmd.Parameters.AddWithValue("@Description", item.Description);
                cmd.Parameters.AddWithValue("@IsActive", item.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();               
                conn.Close();
            }

            return Redirect("/Item");
        }
        [HttpGet]
        public ActionResult UpdateItem(int id)
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
                    Category category1 = new Category();
                    category1.Id = Convert.ToInt32(reader["Id"].ToString());
                    category1.Name = reader["Name"].ToString();
                    category.Add(category1);
                }
                conn.Close();
            }

            List<Brand> brand = new List<Brand>();
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "select * from Brand";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Brand brand1 = new Brand();
                    brand1.Id = Convert.ToInt32(reader["Id"].ToString());
                    brand1.Name = reader["Name"].ToString();
                    brand.Add(brand1);
                }
                conn.Close();
            }
            
            Item item = new Item();
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "select * from Item where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    item.Id = Convert.ToInt32(reader["Id"].ToString());
                    item.ItemCode = reader["ItemCode"].ToString();
                    item.Name = reader["Name"].ToString();
                    item.Description = reader["Description"].ToString();
                    item.BrandId = reader["BrandId"].ToString();
                    item.CategoryId = reader["CategoryId"].ToString();
                    //item.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                }
                conn.Close();
            }
            ViewBag.Category = category;
            ViewBag.Brand = brand;
            return View(item);
        }
        [HttpPost]
        public ActionResult UpdateItem(Item item)
        {
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = @"update Item set 
                                    ItemCode=@ItemCode,
                                    Name=@Name,
                                    Description=@Description,
                                    CategoryId=@CategoryId,
                                    BrandId=@BrandId,
                                    IsActive=@IsActive
                                    where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemCode", item.ItemCode);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@CategoryId", item.CategoryId);
                cmd.Parameters.AddWithValue("@BrandId", item.BrandId);
                cmd.Parameters.AddWithValue("@Description", item.Description);
                cmd.Parameters.AddWithValue("@IsActive", item.IsActive);
                cmd.Parameters.AddWithValue("@Id", item.Id);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
            }


            return Redirect("/Item");
        }
    }
}
