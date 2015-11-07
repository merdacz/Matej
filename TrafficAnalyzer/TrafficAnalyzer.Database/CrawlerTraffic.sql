CREATE TABLE [dbo].[CrawlerTraffic] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[CrawlerName] NVARCHAR(50) NOT NULL,
	[AccessAttempts] INT NOT NULL ,
	[TransferedBytes] INT NOT NULL 
	)