using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.Data.SqlClient;

namespace DOT_NET_MVC_INVENTORY.DataAccessLayer
{
    public class ItemGateway
    {
        public string _connString { get; set;}

        public ItemGateway(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Item> GetList() {
            List<Item> items = new List<Item>();

            using (SqlConnection conn = new SqlConnection(_connString))
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
                    item.CategoryId = reader["CategoryName"].ToString();
                    item.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                    items.Add(item);
                }
                conn.Close();
            }

            return items;
        }
        public Item GetById(int id)
        {
            Item item = new Item();
            using (SqlConnection conn = new SqlConnection(_connString))
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
                    item.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                }
                conn.Close();
            }
            return item;
        }
        public int Update(Item item)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
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
                return response;
            }
        }

        public int Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "delete from Item where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int response = cmd.ExecuteNonQuery();

                conn.Close();
                return response;
            }
        }
        public List<Brand> GetBrandList()
        {
            List<Brand> brand = new List<Brand>();
            using (SqlConnection conn = new SqlConnection(_connString))
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
            return brand;
        } 
        public List<Category> GetCategoryList()
        {
            List<Category> category = new List<Category>();
            using (SqlConnection conn = new SqlConnection(_connString))
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
            return category;
        }

        public int AddItem(Item item)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "insert into item (ItemCode,Name,CategoryId,BrandId,Description,IsActive) values (@ItemCode,@Name,@CategoryId,@BrandId,@Description,@IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemCode", item.ItemCode);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@CategoryId", item.CategoryId);
                cmd.Parameters.AddWithValue("@BrandId", item.BrandId);
                cmd.Parameters.AddWithValue("@Description", item.Description);
                cmd.Parameters.AddWithValue("@IsActive", item.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();

                return response;
            }           
        }
    }
}
