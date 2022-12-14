INSERT INTO Company VALUES 
(1,'Sony Computer Entertainment'),
(2,'Microsoft Studios'),
(3,'Nintendo'),
(4,'Sega'),
(5,'Electronic Arts');
INSERT INTO Package VALUES
(1,'Хиты этого года',1),
(2,'Лидеры месяца',5),
(3,'По низким ценам',2),
(4,'Новинки',3),
(5,'Лучшее от Sega',4);
INSERT INTO Developer VALUES
(1,'Гейб Ньюэлл',3),
(2,'Джон Кармак',4),
(3,'Сид Мейер',2),
(4,'Крис Метзен',5),
(5,'Дрю Карпишин',1);
INSERT INTO Users VALUES
(1,'Иван Иванов'),
(2,'Пётр Петров'),
(3,'Наталья Игнатенко'),
(4,'Анна Василевич'),
(5,'Никита Бойко');
INSERT INTO Game VALUES
(1,'StarWars',50,'Фантастика','Английский','США',1,1),
(2,'Cyberpunk',75,'Экшен','Русский','Польша',2,2),
(3,'Last of us',55,'Стелс экшн','Английский','США',3,3),
(4,'God of War',80,'Фантастика','Английский','США',4,4),
(5,'Uncharted',45,'Приключения','Русский','США',5,5);
INSERT INTO Status VALUES
(1,'Скоро выход',3),
(2,'В разработке',4),
(3,'Релиз',2),
(4,'Обновляется',5),
(5,'Временно недоступна',1);
INSERT INTO Cart VALUES
(1,2),
(2,5),
(3,1),
(4,4),
(5,3);
INSERT INTO Orders VALUES
(1,50,'Готов',4,1),
(2,100,'Принят',3,2),
(3,65,'Обрабатывается',2,3),
(4,45,'Готов',5,4),
(5,55,'Обрабатывается',1,5);
INSERT INTO Transactions VALUES
(1,'Прошла успешна',4,1),
(2,'Ошибка',3,2),
(3,'Ожидайте',2,3),
(4,'Прошла успешна',5,4),
(5,'Ожидайте',1,5);