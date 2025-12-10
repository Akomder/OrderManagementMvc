using System.ComponentModel.DataAnnotations;

namespace OrderManagementMvc.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 1000000)]
        [Display(Name = "Unit Price")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 100000)]
        [Display(Name = "Stock Quantity")]
        public int StockQuantity { get; set; }

        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation property
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
