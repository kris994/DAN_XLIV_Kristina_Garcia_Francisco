-- Dropping the tables before recreating the database in the order depending how the foreign keys are placed.
IF OBJECT_ID('tblShoppingCart', 'U') IS NOT NULL DROP TABLE tblShoppingCart;
IF OBJECT_ID('tblOrder', 'U') IS NOT NULL DROP TABLE tblOrder;
IF OBJECT_ID('tblUser', 'U') IS NOT NULL DROP TABLE tblUser;
IF OBJECT_ID('tblItem', 'U') IS NOT NULL DROP TABLE tblItem;

-- Checks if the database already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'OrderDB')
CREATE DATABASE OrderDB;
GO

USE OrderDB
CREATE TABLE tblUser(
	UserID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	JMBG VARCHAR (13) UNIQUE				NOT NULL,
);

USE OrderDB
CREATE TABLE tblItem(
	ItemID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	ItemName VARCHAR (40) UNIQUE		    NOT NULL,
);

USE OrderDB
CREATE TABLE tblShoppingCart(
	ShoppingCartID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	Amount INT, 
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID),
	ItemID INT FOREIGN KEY REFERENCES tblItem(ItemID),
);

USE OrderDB
CREATE TABLE tblOrder(
	ShoppingCartID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	TotalPrice INT									NOT NULL,
	OrderStatus  VARCHAR (20)						NOT NULL,
	OrderCreated DATE								NOT NULL,
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID),
);

INSERT INTO tblUser(JMBG) VALUES ('1111111111111')
INSERT INTO tblUser(JMBG) VALUES ('2222222222222')
INSERT INTO tblUser(JMBG) VALUES ('3333333333333')
INSERT INTO tblUser(JMBG) VALUES ('4444444444444')

INSERT INTO tblItem(ItemName) VALUES ('Soup')
INSERT INTO tblItem(ItemName) VALUES ('Salad')
INSERT INTO tblItem(ItemName) VALUES ('Meat')
INSERT INTO tblItem(ItemName) VALUES ('Hamburger')
INSERT INTO tblItem(ItemName) VALUES ('Pizza')