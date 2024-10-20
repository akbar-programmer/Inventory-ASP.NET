using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DOT_NET_MVC_INVENTORY.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string Name { get; set; }
            
        public string? Description { get; set; }
       // [DisplayName->"Item Code"]
        public string? BrandId { get; set; }
        public string? CategoryId { get; set; }
        public bool IsActive { get; set; }

    }
}
