using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.Data.SqlClient;

namespace DOT_NET_MVC_INVENTORY.DataAccessLayer
{
    public class CustomerGateway
    {
        public string _connString { get; set; }

        public CustomerGateway(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Customer> GetList()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from Customer";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer newcust = new Customer();
                    newcust.Id = Convert.ToInt32(reader["Id"].ToString());
                    newcust.Name = reader["Name"].ToString();
                    newcust.Mobile = reader["Mobile"].ToString();
                    newcust.Email = reader["Email"].ToString();
                    newcust.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                    customers.Add(newcust);
                }
                conn.Close();
            }
            return customers;
        }
        public int Add(Customer category)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "insert into Customer (Name,Mobile,Email,IsActive) values (@Name,@Mobile,@Email,@IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", category.Name);
                cmd.Parameters.AddWithValue("@Mobile", category.Mobile);
                cmd.Parameters.AddWithValue("@Email", category.Email);
                cmd.Parameters.AddWithValue("@IsActive", category.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
                return response;
            }

        }
        public Customer GetById(int id)
        {
            Customer customers = new Customer();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from Customer where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    customers.Id = Convert.ToInt32(reader["Id"].ToString());
                    customers.Name = reader["Name"].ToString();
                    customers.Mobile = reader["Mobile"].ToString();
                    customers.Email = reader["Email"].ToString();
                    customers.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                }
                conn.Close();
            }
            return customers;
        }
        public int Update(Customer Category)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "update Customer set Name=@Name,IsActive=@IsActive,Mobile=@Mobile,Email=@Email where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", Category.Name);
                cmd.Parameters.AddWithValue("@Mobile", Category.Mobile);
                cmd.Parameters.AddWithValue("@Email", Category.Email);
                cmd.Parameters.AddWithValue("@IsActive", Category.IsActive);
                cmd.Parameters.AddWithValue("@Id", Category.Id);

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
                string query = "delete from Customer where Id=@Id";
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
