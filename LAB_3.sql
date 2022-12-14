SELECT * FROM Game;

SELECT Prices FROM Game;

SELECT * FROM Game
WHERE Country  = 'США';

SELECT * FROM Game
WHERE Prices < 55;

SELECT * FROM Game
WHERE Prices > 55;

SELECT * FROM Game
WHERE Country = 'США' AND Prices > 55;

SELECT * FROM Game
WHERE Country = 'США' OR Prices > 55;

SELECT * FROM Game
WHERE NOT Country = 'США';

SELECT * FROM Game
WHERE Country = 'США' OR Prices > 55 AND Languages = 'Английский';

SELECT * FROM Game
WHERE (Country = 'США' OR Prices > 55) AND Languages = 'Английский';

SELECT * FROM Game
WHERE Prices IS NULL;

UPDATE Game
SET Prices = Prices + 1000;

UPDATE Game
SET Languages = 'English'
WHERE Languages = 'Английский';

UPDATE Game
SET Languages = 'English'
WHERE Languages = 'Английский';

UPDATE Game
SET Country = 'USA',
    Prices = Prices - 1000
WHERE Country = 'США';

DELETE FROM Game
WHERE Country = 'Польша';
	
DELETE FROM Game
WHERE Country = 'Польша' AND Prices < 60;
	
DELETE FROM Game;

SELECT DISTINCT Languages FROM Game;

SELECT * FROM Game
ORDER BY Prices;

SELECT Prices FROM Game
ORDER BY Prices DESC;

SELECT Prices FROM Game
ORDER BY Prices ASC;

SELECT * FROM Game
ORDER BY Prices
LIMIT 4;

SELECT * FROM Game
ORDER BY Prices
LIMIT 3 OFFSET 2;
