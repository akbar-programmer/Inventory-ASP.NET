using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.Data.SqlClient;

namespace DOT_NET_MVC_INVENTORY.DataAccessLayer
{
    public class SupplierGateway
    {
        public string _connString { get; set; }

        public SupplierGateway(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Supplier> GetList()
        {
            List<Supplier> supplier = new List<Supplier>();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from Supplier";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Supplier row = new Supplier();
                    row.Id = Convert.ToInt32(reader["Id"].ToString());
                    row.Name = reader["Name"].ToString();
                    row.Mobile = reader["Mobile"].ToString();
                    row.Email = reader["Email"].ToString();
                    row.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                    supplier.Add(row);
                }
                conn.Close();
            }
            return supplier;
        }
        public int Add(Supplier Category)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "insert into Supplier (Name,Mobile,Email,IsActive) values (@Name,@Mobile,@Email,@IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", Category.Name);
                cmd.Parameters.AddWithValue("@Mobile", Category.Mobile);
                cmd.Parameters.AddWithValue("@Email", Category.Email);
                cmd.Parameters.AddWithValue("@IsActive", Category.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
                return response;
            }

        }
        public Supplier GetById(int id)
        {
            Supplier supplier = new Supplier();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from Supplier where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    supplier.Id = Convert.ToInt32(reader["Id"].ToString());
                    supplier.Name = reader["Name"].ToString();
                    supplier.Mobile = reader["Mobile"].ToString();
                    supplier.Email = reader["Email"].ToString();
                    supplier.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                }
                conn.Close();
            }
            return supplier;
        }
        public int Update(Supplier supplier)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "update Supplier set Name=@Name,IsActive=@IsActive,Mobile=@Mobile,Email=@Email where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", supplier.Name);
                cmd.Parameters.AddWithValue("@Mobile", supplier.Mobile);
                cmd.Parameters.AddWithValue("@Email", supplier.Email);
                cmd.Parameters.AddWithValue("@IsActive", supplier.IsActive);
                cmd.Parameters.AddWithValue("@Id", supplier.Id);

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
                string query = "delete from Supplier where Id=@Id";
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
