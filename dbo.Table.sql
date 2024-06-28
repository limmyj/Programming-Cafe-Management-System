CREATE TABLE [dbo].[Trainers]
(
	[TPNum] nvarchar(20) NOT NULL PRIMARY KEY,
	[Name] nvarchar(20) NOT NULL,
	[Email] nvarchar(50) NOT NULL,
	[ContactNumber] nvarchar(20) NULL,
	[Address] nvarchar(50) NULL,
	[Biodata] nvarchar(50) NULL
)
