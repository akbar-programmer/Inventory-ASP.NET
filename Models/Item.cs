using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DOT_NET_MVC_INVENTORY.Models
{
    public class Item
    {
        public int Id { get; set; }
        [DisplayName("Item Code")]
        public string ItemCode { get; set; }
        [DisplayName("Item Name")]
        public string Name { get; set; }            
        public string? Description { get; set; }
        [DisplayName("Brand")]
        public string? BrandId { get; set; }
        [DisplayName("Category")]
        public string? CategoryId { get; set; }
        public bool IsActive { get; set; }

    }
}
