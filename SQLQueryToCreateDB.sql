CREATE TABLE CustomerDB (
	[Id] INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[Address] NVARCHAR(100) NOT NULL,
	[PhoneNum] NVARCHAR(12) NOT NULL
);

CREATE TABLE OrderDB (
	[Id] INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
	[CustomerId] INT FOREIGN KEY REFERENCES CustomerDb (Id),
	[Number] NVARCHAR(12) NOT NULL,
	[Amount] INT NOT NULL DEFAULT 0,
	[DueTime] DATETIME NOT NULL DEFAULT (DATEADD(day, 3, GETDATE())),
	[ProcessedTime] DATETIME NOT NULL DEFAULT (DATEADD(day, 1, GETDATE())),
	[Description] TEXT	
);

SELECT DATEADD(day, 3, GETDATE())

SELECT * FROM CustomerDB

SELECT * FROM OrderDB

INSERT INTO CustomerDB (Name, Address, PhoneNum) VALUES 
('Иванов Иван Иванович', 'г. Самара, ул. Кирова, д.15', '+79277776655'),
('Сергеев Сергей Сергеевич', 'г. Москва, ул. Победы, д.112', '+79176665544'),
('Петров Петр Петрович', 'г. Санкт-Петербург, ул. Льва Толстого, д. 5', '+79178887766')

INSERT INTO OrderDB (CustomerId, Number, Amount, DueTime, ProcessedTime, Description) VALUES
((SELECT Id from CustomerDB WHERE Id = 1), '1111', 10, DATEADD(MONTH, 1, GETDATE()), DATEADD(DAY, 10, GETDATE()), 'Тестовый заказ №1')

INSERT INTO OrderDB (CustomerId, Number, Amount, DueTime, Description) VALUES
((SELECT Id from CustomerDB WHERE Id = 2), '1111', 10, DATEADD(MONTH, 1, GETDATE()), 'Тестовый заказ №2')

INSERT INTO OrderDB (CustomerId, Number, Amount, Description) VALUES
((SELECT Id from CustomerDB WHERE Id = 3), '3333', 10,  'Тестовый заказ №3')