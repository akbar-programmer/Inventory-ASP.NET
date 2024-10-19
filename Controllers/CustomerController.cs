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
                    cust.IsActive = Convert.ToInt32( reader["IsActive"].ToString());
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

            return View();
        }
    }
}
