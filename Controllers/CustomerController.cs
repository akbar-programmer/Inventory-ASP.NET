using DOT_NET_MVC_INVENTORY.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DOT_NET_MVC_INVENTORY.Controllers
{
    public class CustomerController : Controller
    {
        public AppDBContext dbContext { get; set; }
        public CustomerController()
        {
            dbContext = new AppDBContext();
        }
        public IActionResult Index()
        {
            List<Customer> customer = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "select * from Customer";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer cust = new Customer();
                    cust.Id = Convert.ToInt32(reader["Id"].ToString());
                    cust.Name = reader["Name"].ToString();
                    cust.Mobile = reader["Mobile"].ToString();
                    cust.Email = reader["Email"].ToString();
                    cust.IsActive = Convert.ToBoolean( reader["IsActive"].ToString());
                    customer.Add(cust);
                }
                conn.Close();
            }


            return View(customer);
        }
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {           
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "insert into Customer (Name,Mobile,Email,IsActive) values (@Name,@Mobile,@Email,@IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Mobile", customer.Mobile);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
            }

            return Redirect("/Customer");
        }
        [HttpGet]
        public ActionResult UpdateCustomer(int id)
        {
            Customer customer = new Customer();
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "select * from Customer where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    customer.Id = Convert.ToInt32(reader["Id"].ToString());
                    customer.Name = reader["Name"].ToString();
                    customer.Mobile = reader["Mobile"].ToString();
                    customer.Email = reader["Email"].ToString();
                    customer.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                }
                conn.Close();
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = @"update Customer set 
                                    Name=@Name,
                                    Mobile=@Mobile,
                                    Email=@Email,
                                    IsActive=@IsActive
                                    where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);
                cmd.Parameters.AddWithValue("@Mobile", customer.Mobile);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Id", customer.Id);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
            }


            return Redirect("/Customer");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(dbContext.ConnectionString))
            {
                string query = "delete from Customer where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int response = cmd.ExecuteNonQuery();

                conn.Close();
            }
            return Redirect("/Customer");
        }
    }
}
