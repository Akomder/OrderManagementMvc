using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementMvc.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order")]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 10000)]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Subtotal")]
        public decimal Subtotal { get; set; }

        // Navigation properties
        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}
