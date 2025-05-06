select * from titles

-- 1) Print all the titles names
select title from titles

-- 2) Print all the titles that have been published by 1389
select title, pubdate from titles where pub_id = 1389

-- 3) Print the books that have price in range of 10 to 15
select title from titles where price between 10 and 15

-- 4) Print those books that have no price
select title from titles where price is null

-- 5) Print the book names that starts with 'The'
select title from titles where title like 'The%'

-- 6) Print the book names that do not have 'v' in their name
select title from titles where title not like '%v%'

-- 7) print the books sorted by the royalty
select title,royalty from titles order by royalty

-- 8) print the books sorted by publisher in descending then by types in ascending then by price in descending

select pub_id, title Book_name, type, price from titles  
order by pub_id DESC,
type asc,
price desc;


-- 9) Print the average price of books in every type

select AVG(price) AveragePrice, type BookType from titles
group by type

-- 10) print all the types in unique
select type UniqueTypes from titles
group by type

select DISTINCT type UniqueTypes from titles

-- 11) Print the first 2 costliest books
select top 2 title Book_name, price from titles
order by price Desc

-- 12) Print books that are of type business and have price less than 20 which also have advance greater than 7000
select  title Book_name, price, type, advance from titles
where type = 'business' and price < 20 and advance > 7000

-- 13) Select those publisher id and number of books which have price between 15 to 25 and have 'It' in its name. Print only those which have count greater than 2. Also sort the result in ascending order of count

SELECT pub_id, COUNT(pub_id) AS 'number of books'
FROM titles
WHERE price BETWEEN 15 AND 25 AND title LIKE '%It%'
GROUP BY pub_id
HAVING COUNT(pub_id) > 2
ORDER BY COUNT(pub_id);

-- 14) Print the Authors who are from 'CA'
select * from authors
where state = 'CA'

-- 15) Print the count of authors from every state
select state, COUNT(au_id) 'Number of Authors' from authors
group by state


-- Working on DDL Commands 

-- Before Explanation from Gayathri Mahadevan

/*
Design the database for a shop which sells products
Points for consideration
  1) One product can be supplied by many suppliers
  2) One supplier can supply many products
  3) All customers details have to present
  4) A customer can buy more than one product in every purchase
  5) Bill for every purchase has to be stored
  6) These are just details of one shop
*/

create table Products (
	ProductId INT PRIMARY KEY,
	Name VARCHAR(100),
	Price DECIMAL(10,2),
	Quantity INT,
	Description TEXT
);

CREATE TABLE Suppliers (
	SupplierId INT PRIMARY KEY,
	Name VARCHAR(100),
	Organiztion VARCHAR(100),
	Phone VARCHAR(50)
);

CREATE TABLE ProductSupplierRelation(
	ProductId INT,
	SupplierId INT,
	Foreign Key (ProductId) References Products(ProductId),
	Foreign Key (SupplierId) References Suppliers(SupplierId),
	Primary Key (ProductId,SupplierId)
);

CREATE TABLE Customers(
	CustomerId INT PRIMARY KEY,
	Name VARCHAR(100),
	Phone VARCHAR(50)
);

CREATE TABLE Purchase(
	PurchaseID INT PRIMARY KEY,
	CustomerId INT,
	ProductId INT,
	Foreign Key (CustomerId) References Customers(CustomerId),
	Foreign Key (ProductId) References Products(ProductId)
);


-- After Explantion 

/*

Design the database for a shop which sells products
Points for consideration
  1) One product can be supplied by many suppliers
  2) One supplier can supply many products
  3) All customers details have to present
  4) A customer can buy more than one product in every purchase
  5) Bill for every purchase has to be stored
  6) These are just details of one shop
 
categories
id, name, status
 
country
id, name
 
state
id, name, country_id
 
City
id, name, state_id
 
area
zipcode, name, city_id
 
address
id, door_number, addressline1, zipcode
 
supplier
id, name, contact_person, phone, email, address_id, status
 
product
id, Name, unit_price, quantity, description, image
 
product_supplier
transaction_id, product_id, supplier_id, date_of_supply, quantity,
 
Customer
id, Name, Phone, age, address_id
 
order
  order_number, customer_id, Date_of_order, amount, order_status
 
order_details
  id, order_number, product_id, quantity, unit_price

*/

CREATE TABLE Categories(
	id int primary key,
	Name VARCHAR(100),
	Status VARCHAR(100)
);

CREATE TABLE Country(
	id int primary key,
	Name VARCHAR(100)
);

CREATE TABLE State(
	id int primary key,
	Name VARCHAR(100),
	CountryId INT,
	Foreign Key (CountryId) References Country(id) ON DELETE CASCADE
);

CREATE TABLE City(
	id int primary key,
	Name VARCHAR(100),
	StateId int,
	Foreign Key (StateId) References State(id) ON DELETE CASCADE
);

CREATE TABLE Area(
	zipcode int primary key,
	Name varchar(100),
	CityId int,
	Foreign Key (CityId) References City(id) ON DELETE CASCADE
);	


CREATE TABLE Address(
	id int primary key,
	DoorNumber varchar(100),
	AddressLine1 varchar(100),
	AddressLine2 varchar(100),
	zipcode int,
	Foreign Key (zipcode) References Area(zipcode) ON DELETE CASCADE
);	


CREATE TABLE Supplier(
	id int primary key,
	Name varchar(100),
	Phone varchar(100),
	Email varchar(100),
	AddressId int,
	Foreign Key (AddressId) References Address(id) ON DELETE CASCADE
);


CREATE TABLE Product(
	id int primary key,
	Name varchar(100),
	UnitPrice decimal(10,2),
	Quantity int,
	Description Text,
);


CREATE TABLE ProductSupplier(
	id int primary key,
	ProductId int,
	SupplierId int,
	DateOfSupply DateTime,
	Quantity int,
	Foreign key(ProductId) References Product(id) ON DELETE CASCADE,
	Foreign key(SupplierId) References Supplier(id) ON DELETE CASCADE,
);


CREATE TABLE Customer(
	id int primary key,
	Name varchar(100),
	Phone varchar(100),
	Age int,
	AddressId int,
	Foreign Key (AddressId) References Address(id) ON DELETE CASCADE
);


CREATE TABLE Orders(
	OrderNumber int primary key,
	CustomerId int,
	DateOfOrder Datetime,
	Amount Decimal(10,2),
	OrderStatus VARCHAR(100),
);


CREATE TABLE OrderDetails(
	id int primary key,
	OrderNumber int,
	ProductId int,
	Quantity int,
	UnitPrice int,
	Foreign key (OrderNumber) References Orders(OrderNumber) ON DELETE CASCADE,
	Foreign key (ProductId) References Product(id) ON DELETE CASCADE,
);

Create table Bills(
	OrderdetailsID int
	Foreign key (OrderdetailsID) References OrderDetails(id)
);

DROP table Bills;
