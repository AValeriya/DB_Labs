SELECT * FROM Game
WHERE Prices IN (50, 55, 80);

SELECT * FROM Game
WHERE Prices NOT IN (50, 55, 80);

SELECT * FROM Game
WHERE Prices BETWEEN 50 AND 80;

SELECT * FROM Game
WHERE GameName LIKE 'G%';

SELECT AVG(Prices) FROM Game
WHERE Country='USA';

SELECT AVG (Prices) FROM Game;

SELECT COUNT(*) FROM Game;

SELECT COUNT(DISTINCT Prices) FROM Game;

SELECT MIN(Prices) FROM Game;

SELECT MAX(Prices) FROM Game;

SELECT SUM(Prices) FROM Game;

SELECT STRING_AGG(GameName, ', ') FROM Game;

SELECT Country, COUNT(*) AS CountryCount
FROM Game
GROUP BY Country;

SELECT Country, COUNT(*) AS CountryCount
FROM Game
WHERE Prices > 55
GROUP BY Country
ORDER BY CountryCount DESC;

SELECT Country, COUNT(*) AS CountryCount
FROM Game
GROUP BY Country
HAVING COUNT(*) > 1;

SELECT Country, COUNT(*) AS Country, Prices
FROM Game
GROUP BY GROUPING SETS(Country, Prices);

SELECT Country, COUNT(*) AS Country, SUM(Prices) AS Units
FROM Game
GROUP BY ROLLUP(Country);

SELECT Country, COUNT(*) AS Country, SUM(Prices) AS Units
FROM Game
GROUP BY CUBE(Country, Prices);

SELECT * FROM Game
WHERE Prices = (SELECT MIN(Prices) FROM Game);

SELECT * FROM Game
WHERE Prices > (SELECT AVG(Prices) FROM Game);

SELECT  StateOrder , 
        Prices, 
        (SELECT GameName FROM Game 
        WHERE Game.Id = Orders.Id) AS Game
FROM Orders;

SELECT GameName,
       Country,
       Prices, 
        (SELECT AVG(Prices) FROM Game AS GP 
         WHERE GP.Country=Ga.Country)  AS AvgPrices
FROM Game AS Ga
WHERE Prices > 
    (SELECT AVG(Prices) FROM Game AS GP 
     WHERE GP.Country=Ga.Country)

SELECT * FROM Orders, Transactions;

SELECT * FROM Orders, Transactions
WHERE Orders.Id = Transactions.Id;

SELECT Company.CompanyName, Game.GameName, Package.PackageName 
FROM Company, Game, Package
WHERE Company.Id = Package.Id AND Package.Id =Game.Id;

SELECT Orders.Prices, Orders.StateOrder, Game.GameName 
FROM Orders
JOIN Game ON Game.Id = Orders.Id;

SELECT Orders.Prices, Users.UserName, Game.GameName 
FROM Orders
JOIN Users ON Users.Id = Orders.Id
JOIN Game ON Game.Id = Orders.Id;

SELECT Orders.Prices, Users.UserName, Game.GameName 
FROM Orders
JOIN Users ON Users.Id = Orders.Id
JOIN Game ON Game.Id = Orders.Id
WHERE Orders.Prices > 55
ORDER BY Game.GameName;

SELECT Orders.Prices, Users.UserName, Game.GameName 
FROM Orders
JOIN Game ON Game.Id = Orders.Id AND Game.GameName ='Last of us'
JOIN Users ON Users.Id=Orders.Id
ORDER BY Game.GameName;

SELECT UserName, Prices, StateOrder, UsersId 
FROM Orders LEFT JOIN Users 
ON Orders.Id = Users.Id;

SELECT UserName, Prices, StateOrder, UsersId 
FROM Orders FULL JOIN Users 
ON Orders.Id = Users.Id;

SELECT * FROM Orders CROSS JOIN Users;

SELECT UserName, COUNT(Orders.Id)
FROM Users JOIN Orders 
ON Orders.Id = Users.Id
GROUP BY Users.Id, Users.UserName;

SELECT Prices
FROM Game
UNION SELECT Prices FROM Orders;
