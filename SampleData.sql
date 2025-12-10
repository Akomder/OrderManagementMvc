-- Sample Data for Quick Testing
-- Run this after database is created to populate with test data

USE OrderManagementDb;
GO

-- Insert Sample User (Password: admin123 - hashed with SHA256)
INSERT INTO Users (Username, Password, FullName, Email, CreatedDate)
VALUES ('admin', 'efb9e50d81e3cd8ea37fac1ef63d2c39a81b9fa4b5f1f109c85c68f5c9d83ec4', 'Admin User', 'admin@ordersystem.com', GETDATE());

-- Insert Sample Customers
INSERT INTO Customers (Name, Email, Phone, Address, CreatedDate)
VALUES 
('John Smith', 'john.smith@email.com', '(555) 123-4567', '123 Main Street, New York, NY 10001', GETDATE()),
('Sarah Johnson', 'sarah.j@email.com', '(555) 234-5678', '456 Oak Avenue, Los Angeles, CA 90001', GETDATE()),
('Michael Brown', 'mbrown@email.com', '(555) 345-6789', '789 Pine Road, Chicago, IL 60601', GETDATE()),
('Emily Davis', 'emily.davis@email.com', '(555) 456-7890', '321 Elm Street, Houston, TX 77001', GETDATE()),
('David Wilson', 'dwilson@email.com', '(555) 567-8901', '654 Maple Drive, Phoenix, AZ 85001', GETDATE());

-- Insert Sample Agents
INSERT INTO Agents (Name, Email, Phone, Address, Company, CreatedDate)
VALUES 
('Jessica Martinez', 'jessica@techsales.com', '(555) 111-2222', '100 Business Blvd, New York, NY', 'TechSales Inc', GETDATE()),
('Robert Taylor', 'robert@prodeals.com', '(555) 222-3333', '200 Commerce St, Los Angeles, CA', 'ProDeals Corp', GETDATE()),
('Amanda White', 'amanda@salesplus.com', '(555) 333-4444', '300 Trade Ave, Chicago, IL', 'SalesPlus LLC', GETDATE());

-- Insert Sample Products
INSERT INTO Products (Name, Description, Price, StockQuantity, Category, CreatedDate)
VALUES 
('Dell Laptop XPS 15', 'High-performance laptop with Intel i7, 16GB RAM, 512GB SSD', 1299.99, 45, 'Electronics', GETDATE()),
('Apple MacBook Pro', 'MacBook Pro 14" with M3 chip, 16GB RAM, 512GB SSD', 1999.99, 30, 'Electronics', GETDATE()),
('Logitech MX Master 3', 'Wireless productivity mouse with precision tracking', 99.99, 150, 'Accessories', GETDATE()),
('Samsung 27" Monitor', '27-inch 4K UHD monitor with IPS display', 349.99, 60, 'Electronics', GETDATE()),
('Mechanical Keyboard', 'RGB mechanical keyboard with Cherry MX switches', 129.99, 80, 'Accessories', GETDATE()),
('USB-C Hub', '7-in-1 USB-C hub with HDMI, USB 3.0, and SD card reader', 39.99, 200, 'Accessories', GETDATE()),
('Wireless Headphones', 'Noise-cancelling wireless headphones with 30hr battery', 199.99, 75, 'Audio', GETDATE()),
('Webcam 1080p', 'Full HD webcam with auto-focus and built-in microphone', 79.99, 120, 'Electronics', GETDATE()),
('External SSD 1TB', 'Portable external SSD with USB 3.2 Gen 2', 149.99, 90, 'Storage', GETDATE()),
('Desk Lamp LED', 'Adjustable LED desk lamp with touch control', 49.99, 110, 'Office', GETDATE());

-- Insert Sample Orders
-- Order 1: John Smith
DECLARE @Order1Id INT;
INSERT INTO Orders (OrderNumber, CustomerId, AgentId, OrderDate, TotalAmount, Status, Notes)
VALUES ('ORD-20251210001', 1, 1, DATEADD(day, -5, GETDATE()), 1529.97, 'Completed', 'Bulk order for office setup');
SET @Order1Id = SCOPE_IDENTITY();

INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, Subtotal)
VALUES 
(@Order1Id, 1, 1, 1299.99, 1299.99),
(@Order1Id, 3, 2, 99.99, 199.99),
(@Order1Id, 6, 3, 39.99, 119.99);

-- Order 2: Sarah Johnson
DECLARE @Order2Id INT;
INSERT INTO Orders (OrderNumber, CustomerId, AgentId, OrderDate, TotalAmount, Status, Notes)
VALUES ('ORD-20251210002', 2, 2, DATEADD(day, -4, GETDATE()), 2349.98, 'Completed', 'Premium setup for design work');
SET @Order2Id = SCOPE_IDENTITY();

INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, Subtotal)
VALUES 
(@Order2Id, 2, 1, 1999.99, 1999.99),
(@Order2Id, 4, 1, 349.99, 349.99);

-- Order 3: Michael Brown
DECLARE @Order3Id INT;
INSERT INTO Orders (OrderNumber, CustomerId, AgentId, OrderDate, TotalAmount, Status, Notes)
VALUES ('ORD-20251210003', 3, 1, DATEADD(day, -3, GETDATE()), 909.96, 'Processing', 'Gaming setup accessories');
SET @Order3Id = SCOPE_IDENTITY();

INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, Subtotal)
VALUES 
(@Order3Id, 4, 1, 349.99, 349.99),
(@Order3Id, 5, 1, 129.99, 129.99),
(@Order3Id, 7, 1, 199.99, 199.99),
(@Order3Id, 8, 1, 79.99, 79.99),
(@Order3Id, 9, 1, 149.99, 149.99);

-- Order 4: Emily Davis
DECLARE @Order4Id INT;
INSERT INTO Orders (OrderNumber, CustomerId, AgentId, OrderDate, TotalAmount, Status, Notes)
VALUES ('ORD-20251210004', 4, 3, DATEADD(day, -2, GETDATE()), 429.95, 'Completed', 'Home office upgrade');
SET @Order4Id = SCOPE_IDENTITY();

INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, Subtotal)
VALUES 
(@Order4Id, 3, 1, 99.99, 99.99),
(@Order4Id, 5, 1, 129.99, 129.99),
(@Order4Id, 7, 1, 199.99, 199.99);

-- Order 5: David Wilson
DECLARE @Order5Id INT;
INSERT INTO Orders (OrderNumber, CustomerId, AgentId, OrderDate, TotalAmount, Status, Notes)
VALUES ('ORD-20251210005', 5, NULL, DATEADD(day, -1, GETDATE()), 1679.96, 'Pending', 'New laptop and peripherals');
SET @Order5Id = SCOPE_IDENTITY();

INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, Subtotal)
VALUES 
(@Order5Id, 1, 1, 1299.99, 1299.99),
(@Order5Id, 6, 2, 39.99, 79.99),
(@Order5Id, 8, 1, 79.99, 79.99),
(@Order5Id, 10, 2, 49.99, 99.99),
(@Order5Id, 9, 1, 149.99, 149.99);

-- Order 6: John Smith (second order)
DECLARE @Order6Id INT;
INSERT INTO Orders (OrderNumber, CustomerId, AgentId, OrderDate, TotalAmount, Status, Notes)
VALUES ('ORD-20251210006', 1, 1, GETDATE(), 549.96, 'Pending', 'Additional accessories');
SET @Order6Id = SCOPE_IDENTITY();

INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, Subtotal)
VALUES 
(@Order6Id, 5, 1, 129.99, 129.99),
(@Order6Id, 7, 1, 199.99, 199.99),
(@Order6Id, 10, 2, 49.99, 99.99),
(@Order6Id, 6, 3, 39.99, 119.99);

-- Order 7: Sarah Johnson (second order)
DECLARE @Order7Id INT;
INSERT INTO Orders (OrderNumber, CustomerId, AgentId, OrderDate, TotalAmount, Status, Notes)
VALUES ('ORD-20251210007', 2, 2, GETDATE(), 779.94, 'Pending', 'Additional monitors and accessories');
SET @Order7Id = SCOPE_IDENTITY();

INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, Subtotal)
VALUES 
(@Order7Id, 4, 2, 349.99, 699.98),
(@Order7Id, 8, 1, 79.99, 79.99);

PRINT 'Sample data inserted successfully!';
PRINT '';
PRINT 'Login Credentials:';
PRINT 'Username: admin';
PRINT 'Password: admin123';
PRINT '';
PRINT 'Database now contains:';
PRINT '- 1 User';
PRINT '- 5 Customers';
PRINT '- 3 Agents';
PRINT '- 10 Products';
PRINT '- 7 Orders with 29 Order Details';
GO
