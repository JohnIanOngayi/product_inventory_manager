START TRANSACTION;
    CREATE DATABASE IF NOT EXISTS csm_7th_november;
    USE csm_7th_november;

    -- Create Products table
    CREATE TABLE IF NOT EXISTS Products (
        Id          INT AUTO_INCREMENT,
        Name        VARCHAR(255),
        Rate        DECIMAL(6, 2),
        Quantity    INT,
        PRIMARY KEY(Id)
    );
    
    -- Insert sample products
    INSERT INTO Products (Name, Rate, Quantity) VALUES
    ('Laptop', 899.99, 50),
    ('Mouse', 25.50, 100),
    ('Keyboard', 75.00, 80),
    ('Monitor', 299.99, 30),
    ('Headphones', 149.99, 60);

    -- Create Orders table
    CREATE TABLE IF NOT EXISTS Orders (
        Id              INT AUTO_INCREMENT,
        Date            DATE,
        Product_Id      INT,
        Product_Quantity INT,
        Value           DECIMAL(9, 2),
        PRIMARY KEY(Id),
        FOREIGN KEY(Product_Id) REFERENCES Products(Id)
    );

    DELIMITER //
    
    -- Create stored procedure for PRODUCT operations
    CREATE PROCEDURE sp_prod_ops (
        IN prodId           INT, 
        IN prodName         VARCHAR(255), 
        IN prodRate         DECIMAL(6, 2),
        IN prodQuantity     INT,
        IN orderId          INT,
        IN orderDate        DATE,
        IN orderProductId   INT,
        IN orderQuantity    INT,
        IN orderValue       DECIMAL(9, 2),
        IN action           CHAR(3)
    )
    BEGIN
        -- INSERT Product operation
        IF action = 'PI' THEN
            INSERT INTO Products (Name, Rate, Quantity)
            VALUES (prodName, prodRate, prodQuantity);
        
        -- UPDATE Product operation  
        ELSEIF action = 'PU' THEN
            UPDATE Products 
            SET Name = prodName, 
                Rate = prodRate,
                Quantity = prodQuantity
            WHERE Id = prodId;
        
        -- DELETE Product operation
        ELSEIF action = 'PD' THEN
            UPDATE Orders SET Product_Id = NULL WHERE Product_Id = prodId;
            DELETE FROM Products WHERE Id = prodId;
        
        -- SELECT ALL Products operation
        ELSEIF action = 'PA' THEN
            SELECT Id, Name, Rate, Quantity FROM Products;
        
        -- SELECT SINGLE Product operation
        ELSEIF action = 'PS' THEN
            SELECT Id, Name, Rate, Quantity FROM Products WHERE Id = prodId;
        
        -- INSERT Order operation
        ELSEIF action = 'OI' THEN
            INSERT INTO Orders (Date, Product_Id, Product_Quantity, Value)
            VALUES (orderDate, orderProductId, orderQuantity, orderValue);
        
        -- SELECT ALL Orders with Product details
        ELSEIF action = 'OA' THEN
            SELECT o.Id, o.Date, o.Product_Quantity, o.Value,
                   p.Id AS Product_Id, p.Name AS Product_Name, p.Rate AS Product_Rate
            FROM Orders o 
            INNER JOIN Products p ON o.Product_Id = p.Id;
        
		-- SELECT Specific Order
        ELSEIF action = 'OS' THEN
        	SELECT o.Id, o.Date, o.Product_Quantity, o.Value,
                   p.Id AS Product_Id, p.Name AS Product_Name, p.Rate AS Product_Rate
            FROM Orders o 
            INNER JOIN Products p ON o.Product_Id = p.Id 
            WHERE o.Id = orderId;
        
        -- SELECT Orders for specific product
        ELSEIF action = 'OPS' THEN
            SELECT o.Id, o.Date, o.Product_Quantity, o.Value,
                   p.Id AS Product_Id, p.Name AS Product_Name, p.Rate AS Product_Rate
            FROM Orders o 
            INNER JOIN Products p ON o.Product_Id = p.Id 
            WHERE o.Product_Id = prodId;
        
        END IF;
        
    END//
    
    DELIMITER ;

COMMIT;