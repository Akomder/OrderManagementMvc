using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderManagementMvc.Models;

namespace OrderManagementMvc.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Agent)
                .Include(o => o.OrderDetails)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Agent)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null) return NotFound();
            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name");
            ViewData["Products"] = _context.Products.Where(p => p.StockQuantity > 0).ToList();
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int CustomerId, int? AgentId, string Notes, List<int> ProductIds, List<int> Quantities)
        {
            if (ProductIds == null || !ProductIds.Any())
            {
                TempData["Error"] = "Please add at least one product to the order.";
                ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", CustomerId);
                ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name", AgentId);
                ViewData["Products"] = _context.Products.Where(p => p.StockQuantity > 0).ToList();
                return View();
            }

            var order = new Order
            {
                OrderNumber = GenerateOrderNumber(),
                CustomerId = CustomerId,
                AgentId = AgentId,
                OrderDate = DateTime.Now,
                Notes = Notes ?? string.Empty,
                Status = "Pending"
            };

            decimal totalAmount = 0;
            var orderDetails = new List<OrderDetail>();

            for (int i = 0; i < ProductIds.Count; i++)
            {
                if (Quantities[i] > 0)
                {
                    var product = await _context.Products.FindAsync(ProductIds[i]);
                    if (product != null && product.StockQuantity >= Quantities[i])
                    {
                        var detail = new OrderDetail
                        {
                            ProductId = ProductIds[i],
                            Quantity = Quantities[i],
                            UnitPrice = product.Price,
                            Subtotal = product.Price * Quantities[i]
                        };
                        orderDetails.Add(detail);
                        totalAmount += detail.Subtotal;

                        // Update stock
                        product.StockQuantity -= Quantities[i];
                    }
                }
            }

            order.TotalAmount = totalAmount;
            order.OrderDetails = orderDetails;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Order created successfully!";
            return RedirectToAction(nameof(Details), new { id = order.Id });
        }

        // GET: Orders/Print/5
        public async Task<IActionResult> Print(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Agent)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null) return NotFound();
            return View(order);
        }

        // POST: Orders/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.Status = status;
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Order status updated successfully!";
            return RedirectToAction(nameof(Details), new { id });
        }

        private string GenerateOrderNumber()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(1000, 9999);
            return $"ORD-{timestamp}-{random}";
        }
    }
}
