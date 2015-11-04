USE [Chinook]
GO

-- add some entries so we get entries with the same last name
INSERT INTO [dbo].[Customer]
           ([CustomerId]
           ,[FirstName]
           ,[LastName]
           ,[Company]
           ,[Address]
           ,[City]
           ,[State]
           ,[Country]
           ,[PostalCode]
           ,[Phone]
           ,[Fax]
           ,[Email]
           ,[SupportRepId])
     VALUES
           (60
           ,'Puja'
           ,'Srivastava'
           ,null
           ,'Slowackiego'
           ,'Torun'
           ,null
           ,'Poland'
           ,'87100'
           ,'+48 661 519 511'
           ,null
           ,'merdacz@gmail.com'
           ,3);


INSERT INTO [dbo].[Customer]
           ([CustomerId]
           ,[FirstName]
           ,[LastName]
           ,[Company]
           ,[Address]
           ,[City]
           ,[State]
           ,[Country]
           ,[PostalCode]
           ,[Phone]
           ,[Fax]
           ,[Email]
           ,[SupportRepId])
     VALUES
           (61
           ,'Kowalski'
           ,'Pareek'
           ,null
           ,'Slowackiego'
           ,'Torun'
           ,null
           ,'Poland'
           ,'87100'
           ,'+48 661 519 511'
           ,null
           ,'merdacz@gmail.com'
           ,3);


-- actual query 
SELECT c1.[CustomerId], c2.[CustomerId]
FROM [Customer] c1 JOIN [Customer] c2 on (c1.FirstName = c2.FirstName or c1.LastName = c2.LastName) 
WHERE c1.CustomerId < c2.CustomerId

