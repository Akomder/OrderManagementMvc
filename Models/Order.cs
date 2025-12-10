using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementMvc.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order Number")]
        [StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Agent")]
        public int? AgentId { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } = null!;

        [ForeignKey("AgentId")]
        public Agent? Agent { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
