using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.Data.SqlClient;

namespace DOT_NET_MVC_INVENTORY.DataAccessLayer
{
    public class BrandGateway
    {
        public string _connString { get; set; }

        public BrandGateway(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Brand> GetList()
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
                    Brand newbrand = new Brand();
                    newbrand.Id = Convert.ToInt32(reader["Id"].ToString());
                    newbrand.Name = reader["Name"].ToString();
                    newbrand.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                    brand.Add(newbrand);
                }
                conn.Close();
            }
            return brand;
        }
        public int Add(Brand brand)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "insert into Brand (Name,IsActive) values (@Name,@IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", brand.Name);
                cmd.Parameters.AddWithValue("@IsActive", brand.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
                return response;
            }

        }
        public Brand GetById(int id)
        {
            Brand brand = new Brand();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from Brand where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    brand.Id = Convert.ToInt32(reader["Id"].ToString());
                    brand.Name = reader["Name"].ToString();
                    brand.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                }
                conn.Close();
            }
            return brand;
        }
        public int Update(Brand brand)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "update Brand set Name=@Name,IsActive=@IsActive where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", brand.Name);
                cmd.Parameters.AddWithValue("@IsActive", brand.IsActive);
                cmd.Parameters.AddWithValue("@Id", brand.Id);

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
                string query = "delete from Brand where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
                return response;
            }
        }
     
      
    }
}
