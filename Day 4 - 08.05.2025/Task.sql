
-- 1) List all orders with the customer name and the employee who handled the order.

select o.orderid as OrderId, c.companyname as CustomerName, CONCAT(e.firstname,' ',e.lastname) as EmployeeName
from orders o
join employees e on e.employeeid = o.employeeid
join customers c on c.customerid = o.customerid
-- join orderdetails od on od.orderid = o.orderid
-- join products p on p.productid = od.productid

-- 2) Get a list of products along with their category and supplier name. (Join Products, Categories, and Suppliers)

select p.productname as ProductName, c.categoryname as CategoryName, s.companyname as SupplierCompanyName
from products p
join Categories c on c.categoryid = p.categoryid
join Suppliers s on s.supplierid = p.supplierid

-- 3) Show all orders and the products included in each order with quantity and unit price. (Join Orders, Order Details, Products)

select o.OrderID,p.ProductName,od.Quantity,od.UnitPrice 
from Orders o
join [Order Details] od on od.OrderID = o.OrderID
join Products p on p.ProductID = od.ProductID

-- 4) List employees who report to other employees (manager-subordinate relationship). (Self join on Employees)


SELECT e.EmployeeID, CONCAT(e.FirstName,'',e.LastName) AS EmployeeName, CONCAT(m.FirstName,'',m.LastName) AS ManagerName,e.ReportsTo
FROM Employees e
JOIN Employees m ON e.ReportsTo = m.EmployeeID;

-- 5) Display each customer and their total order count. (Join Customers and Orders, then GROUP BY)

select c.CompanyName ,COUNT(o.OrderID) 'No of Orders'
from Customers c
join Orders o on o.CustomerID = c.CustomerID
group by c.CompanyName

-- 6) Find the average unit price of products per category. (Use AVG() with GROUP BY)

select c.CategoryName ,AVG(p.UnitPrice) 'Average Price'
from Products p
join Categories c on c.CategoryID = p.CategoryID
group by c.CategoryName

-- 7) List customers where the contact title starts with 'Owner'. Use LIKE or LEFT(ContactTitle, 5)



select CompanyName, ContactTitle
from Customers 
where ContactTitle like 'Owner%'

-- or

select CompanyName, ContactTitle
from Customers 
where LEFT(ContactTitle, 5) = 'Owner'

-- 8) Show the top 5 most expensive products. Use ORDER BY UnitPrice DESC and TOP 5

select TOP 5 ProductName, UnitPrice
from Products
order by UnitPrice DESC

-- 9) Return the total sales amount (quantity × unit price) per order. Use SUM(OrderDetails.Quantity * OrderDetails.UnitPrice) and GROUP BY

select OrderID ,SUM(Quantity * UnitPrice) as TotalSales
from [Order Details] 
group by orderId

-- 10) Create a stored procedure that returns all orders for a given customer ID. Input: @CustomerID

create or alter proc proc_OrderByCustomerId(@CustomerID nchar(5))
as
begin
	select o.CustomerID, ProductName, od.UnitPrice, od.Quantity
	from Orders o
	join [Order Details] od on od.OrderID = o.OrderID
	join Products p on p.ProductID = od.ProductID
	where o.CustomerID = @CustomerID
end
go
proc_OrderByCustomerId 'TOMSP'

-- 11) Write a stored procedure that inserts a new product. Inputs: ProductName, SupplierID, CategoryID, UnitPrice, etc.

CREATE OR ALTER PROCEDURE proc_InsertNewProduct
    @ProductName NVARCHAR(40),
    @SupplierID INT,
    @CategoryID INT,
    @Quantity NVARCHAR(20),
    @UnitPrice MONEY,
    @Stock SMALLINT,
    @Order SMALLINT,
    @Reorder SMALLINT,
    @Discontinued BIT
AS
BEGIN
    INSERT INTO Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
    VALUES (@ProductName, @SupplierID, @CategoryID, @Quantity, @UnitPrice, @Stock, @Order, @Reorder, @Discontinued)
END


EXEC proc_InsertNewProduct 'Dell Laptop', 13, 4, '4 pieces', 40000, 12, 12, 10, 0

select * from Products

-- 12) Create a stored procedure that returns total sales per employee. Join Orders, Order Details, and Employees

CREATE OR ALTER PROCEDURE proc_TotalSalesOfEmployee
as 
begin
	select e.EmployeeID,CONCAT(e.FirstName,' ', e.LastName) EmployeeName, SUM(od.UnitPrice) as TotalSales
	from Orders o
	join [Order Details] od on od.OrderID = o.OrderID
	join Employees e on e.EmployeeID = o.EmployeeID
	group by e.EmployeeID, e.FirstName, e.LastName
	order by e.EmployeeID
end
go
exec proc_TotalSalesOfEmployee

-- 13) Use a CTE to rank products by unit price within each category. Use ROW_NUMBER() or RANK() with PARTITION BY CategoryID

with RankProducts
as (
	select p.ProductName, p.UnitPrice, Dense_RANK() over (PARTITION BY c.CategoryID ORDER BY p.UnitPrice) as rnk
	from Products p
	join Categories c on c.CategoryID = p.CategoryID
)
select * from RankProducts

-- 14) Create a CTE to calculate total revenue per product and filter products with revenue > 10,000.

with RevenueOfProd
as
(
	select od.ProductID, sum(od.UnitPrice) as TotalAmount
	from Products p
	join [Order Details] od on od.ProductID = p.ProductID
	group by od.ProductID
)
select * from RevenueOfProd 
where TotalAmount > 10000
order by ProductID

-- 15) Use a CTE with recursion to display employee hierarchy. Start from top-level employee (ReportsTo IS NULL) and drill down

WITH EmployeeHierarchy AS (
    SELECT EmployeeID,LastName,FirstName,Title,ReportsTo,0 AS Level
    FROM Employees
    WHERE ReportsTo IS NULL

    UNION ALL

    SELECT e.EmployeeID,e.LastName, e.FirstName,e.Title,e.ReportsTo, eh.Level + 1 AS Level
    FROM Employees e
	INNER JOIN EmployeeHierarchy eh ON e.ReportsTo = eh.EmployeeID
)

SELECT EmployeeID,LastName, FirstName,Title,ReportsTo,Level
FROM EmployeeHierarchy
ORDER BY Level, ReportsTo, EmployeeID;
