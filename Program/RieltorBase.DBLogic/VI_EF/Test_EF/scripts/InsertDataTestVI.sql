USE [VolgaInfoDB]
GO
DELETE Firm;
DELETE Agent;
DELETE Action;
DELETE Changelog;
DELETE ChangelogAgent;
DELETE Photo;
DELETE PropertyType;
DELETE PropertyValue;
DELETE RealtyObject;
DELETE RealtyObjectType;
DELETE RealtyObjectType_PropertyType;
UPDATE STATISTICS Agent;
GO
DBCC CHECKIDENT (Action, RESEED, 0);
DBCC CHECKIDENT (Agent, RESEED, 0);
--DBCC CHECKIDENT (Changelog, RESEED, 0) --поставить автоматический индекс
DBCC CHECKIDENT (ChangelogAgent, RESEED, 0);
DBCC CHECKIDENT (Firm, RESEED, 0);
DBCC CHECKIDENT (Photo, RESEED, 0);
DBCC CHECKIDENT (PropertyType, RESEED, 0); -- -&gt; не должно быть в XML после парсина (см. Дачи)
DBCC CHECKIDENT (RealtyObject, RESEED, 0); --AgetId не должно быть null
DBCC CHECKIDENT (RealtyObjectType, RESEED, 0);
GO
INSERT INTO Firm
VALUES ( 'Лира' ), ( 'Aфина' ), ( 'Корнов' ), ('Седакова');
GO
INSERT INTO Agent
VALUES ( 'Иван', 'Иванович', 'Зеленова 3а', '33-22-11', 2, 0 ),
( 'Сергей', 'Петров', 'Свердлова 4-33', '21-23-41', 2, 0 ),
( 'Петр', 'Ларин', 'Космонавтов 4-21', '13-32-14', 2, 1 ),
( 'Ольга', 'Степановна', 'Яровая 4', '43-52-61', 1, 0 ),
( 'Андрей', 'Зорин', 'Филипинова 2-121', '38-52-13', 1, 0 ),
( 'Олег', 'Пачкордов', 'Фиалкова 16-2', '55-26-71', 1, 1 );
GO
INSERT INTO RealtyObjectType
VALUES ( 'Квартира' ), ( 'Долевое' ), ( 'Дача' ), ('Котедж');
GO
INSERT INTO PropertyType
VALUES ( 'IdApartment', 'String' ), ( 'Date', 'Date' ), ( 'District', 'String' ), ( 'Quarter', 'Integer' ), ( 'QuantityRoom', 'Integer' ), ( 'Street', 'String' ), ( 'Home', 'string' ),
('Readiness', 'String'), ('Address', 'String'), ('Floors', 'Integer');
GO
INSERT INTO RealtyObjectType_PropertyType
VALUES ( 1, 1 ), ( 1, 2 ), ( 1, 3 ), ( 1, 4 ), ( 1, 5 ), ( 1, 6 ), ( 1, 7 ),
( 3, 1 ), ( 3, 2 ), ( 3, 8 ), ( 3, 3 ), ( 3, 9 ), ( 3, 10 );
GO
INSERT INTO RealtyObject
VALUES ( 1, 5 ), ( 1, 2 ), (1, 2 ), ( 1, 2 ), ( 1, 2 ), ( 3, 1 ), ( 3, 1 ), ( 1, 5 ), ( 1, 5 ), ( 1, 6 ), ( 1, 6 ), ( 1, 3 );
GO
INSERT INTO PropertyValue
VALUES ( 1, 1, 'TLT0256_2585' ), ( 1, 2, '2015-12-22 00:00:00' ), ( 1, 3, 'Автозаводской' ), ( 1, 4, '1' ), ( 1, 5, '1' ), ( 1, 6, 'Свердлова' ), ( 1, 7, '43' ),
( 2, 1, 'TLT0134_1704' ), ( 2, 2, '2015-12-28 00:00:00' ), ( 2, 3, 'Автозаводской' ), ( 2, 4, '1' ), ( 2, 5, '1' ), ( 2, 6, 'Революционная' ), ( 2, 7, '50' ),
( 6, 1, 'TLT0549_6' ), ( 6, 2, '2015-10-15 00:00:00' ), ( 6, 8, '0%' ), ( 6, 3, 'Автозаводской' ), ( 6, 9, '2 кв.\Нектар' ), ( 6, 10, '2' );
GO
SELECT rot.TypeName,
	   pt.PropertyName,
	   pv.StringValue,
	   pt.PropertyValueType,
	   a.LastName,
	   f.Name		 
FROM PropertyValue AS pv
INNER JOIN PropertyType AS pt ON pt.PropertyTypeId = pv.PropertyTypeId
INNER JOIN RealtyObject AS rl ON rl.RealtyObjectId = pv.RealtyObjectId
INNER JOIN RealtyObjectType AS rot ON rot.RealtyObjectTypeId = rl.RealtyObjectTypeId
INNER JOIN Agent AS a ON a.Id_agent = rl.AgentId
INNER JOIN Firm AS f ON f.FirmId = a.Id_firm
ORDER BY f.Name