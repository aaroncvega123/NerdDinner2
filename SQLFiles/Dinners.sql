CREATE TABLE [dbo].[Table]
(
	[DinnerID] INT NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [EventDate] DATETIME NOT NULL, 
    [Description] NVARCHAR(256) NOT NULL, 
    [HostedBy] NVARCHAR(20) NOT NULL, 
    [ContactPhone] NVARCHAR(20) NOT NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [Country] NVARCHAR(30) NOT NULL, 
    [Latitude] FLOAT NOT NULL, 
    [Longitude] FLOAT NOT NULL
)
