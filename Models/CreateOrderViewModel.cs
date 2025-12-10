namespace OrderManagementMvc.Models
{
    public class CreateOrderViewModel
    {
        public int CustomerId { get; set; }
        public int? AgentId { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<OrderDetailItem> OrderDetails { get; set; } = new List<OrderDetailItem>();
    }

    public class OrderDetailItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
