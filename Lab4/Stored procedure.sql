CREATE PROCEDURE [dbo].[ReadOne]
@SalesOrderId INT=1,
@CustomerId INT OUTPUT,
@TerritoryId INT OUTPUT,
@CreditCardId INT OUTPUT,
@ShipToAddressId INT OUTPUT,
@AccountNumber VARCHAR(10) OUTPUT,
@Name VARCHAR(50) OUTPUT,
@CardType VARCHAR(50) OUTPUT,
@City VARCHAR(30) OUTPUT
AS
BEGIN
SELECT @CustomerId=CustomerID FROM [AdventureWorks2019].[Sales].[SalesOrderHeader]
WHERE SalesOrderID=@SalesOrderId
SELECT @TerritoryId=TerritoryID FROM [AdventureWorks2019].[Sales].[SalesOrderHeader]
WHERE SalesOrderID=@SalesOrderId
SELECT @CreditCardId=CreditCardID FROM [AdventureWorks2019].[Sales].[SalesOrderHeader]
WHERE SalesOrderID=@SalesOrderId
SELECT @ShipToAddressId=ShipToAddressID FROM [AdventureWorks2019].[Sales].[SalesOrderHeader]
WHERE SalesOrderID=@SalesOrderId
SELECT @AccountNumber=AccountNumber FROM [AdventureWorks2019].[Sales].[Customer]
WHERE CustomerID=@CustomerId
SELECT @Name=Name FROM [AdventureWorks2019].[Sales].[SalesTerritory]
WHERE TerritoryID=@TerritoryId
SELECT @CardType=CardType FROM [AdventureWorks2019].[Sales].[CreditCard]
WHERE CreditCardID=@CreditCardId
SELECT @City=City FROM [AdventureWorks2019].[Person].[Address]
WHERE AddressID=@ShipToAddressId
END;