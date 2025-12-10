using System.ComponentModel.DataAnnotations;

namespace OrderManagementMvc.Models
{
    public class Agent
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Agent Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(255)]
        public string Address { get; set; } = string.Empty;

        [StringLength(100)]
        public string Company { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation property
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
