namespace DOT_NET_MVC_INVENTORY.Models
{
    public class AppDBContext
    {
        public string ConnectionString { get; set; }
        public AppDBContext() {
            ConnectionString = "Server=AKBAR-DESKTOP\\SQLEXPRESS;Database=InventoryDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";
        }

    }
}
