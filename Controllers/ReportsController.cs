using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementMvc.Models;

namespace OrderManagementMvc.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // Best selling products
        public async Task<IActionResult> BestItems()
        {
            var bestItems = await _context.OrderDetails
                .Include(od => od.Product)
                .GroupBy(od => new { od.ProductId, od.Product.Name, od.Product.Category })
                .Select(g => new
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    Category = g.Key.Category,
                    TotalQuantitySold = g.Sum(od => od.Quantity),
                    TotalRevenue = g.Sum(od => od.Subtotal),
                    OrderCount = g.Count()
                })
                .OrderByDescending(x => x.TotalQuantitySold)
                .Take(20)
                .ToListAsync();

            return View(bestItems);
        }

        // Items purchased by specific customer
        public async Task<IActionResult> CustomerPurchases(int? customerId)
        {
            ViewData["CustomerId"] = customerId;
            ViewData["Customers"] = await _context.Customers.ToListAsync();

            if (customerId == null)
            {
                return View(new List<dynamic>());
            }

            var purchases = await _context.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Order)
                    .ThenInclude(o => o.Customer)
                .Where(od => od.Order.CustomerId == customerId)
                .GroupBy(od => new { od.ProductId, od.Product.Name, od.Product.Category })
                .Select(g => new
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    Category = g.Key.Category,
                    TotalQuantity = g.Sum(od => od.Quantity),
                    TotalSpent = g.Sum(od => od.Subtotal),
                    OrderCount = g.Select(od => od.OrderId).Distinct().Count(),
                    LastPurchaseDate = g.Max(od => od.Order.OrderDate)
                })
                .OrderByDescending(x => x.TotalSpent)
                .ToListAsync();

            var customer = await _context.Customers.FindAsync(customerId);
            ViewData["CustomerName"] = customer?.Name;

            return View(purchases);
        }

        // All customers and their total purchases
        public async Task<IActionResult> CustomerReport()
        {
            var customerReport = await _context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderDetails)
                .Select(c => new
                {
                    CustomerId = c.Id,
                    CustomerName = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    TotalOrders = c.Orders.Count,
                    TotalSpent = c.Orders.Sum(o => o.TotalAmount),
                    LastOrderDate = c.Orders.OrderByDescending(o => o.OrderDate).Select(o => o.OrderDate).FirstOrDefault()
                })
                .OrderByDescending(x => x.TotalSpent)
                .ToListAsync();

            return View(customerReport);
        }

        // Products purchased by multiple customers
        public async Task<IActionResult> ProductCustomers(int? productId)
        {
            ViewData["ProductId"] = productId;
            ViewData["Products"] = await _context.Products.ToListAsync();

            if (productId == null)
            {
                return View(new List<dynamic>());
            }

            var productCustomers = await _context.OrderDetails
                .Include(od => od.Order)
                    .ThenInclude(o => o.Customer)
                .Include(od => od.Product)
                .Where(od => od.ProductId == productId)
                .GroupBy(od => new { od.Order.CustomerId, od.Order.Customer.Name, od.Order.Customer.Email })
                .Select(g => new
                {
                    CustomerId = g.Key.CustomerId,
                    CustomerName = g.Key.Name,
                    Email = g.Key.Email,
                    TotalQuantity = g.Sum(od => od.Quantity),
                    TotalSpent = g.Sum(od => od.Subtotal),
                    OrderCount = g.Select(od => od.OrderId).Distinct().Count(),
                    LastPurchaseDate = g.Max(od => od.Order.OrderDate)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .ToListAsync();

            var product = await _context.Products.FindAsync(productId);
            ViewData["ProductName"] = product?.Name;

            return View(productCustomers);
        }
    }
}
