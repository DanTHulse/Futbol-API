CREATE TABLE [football].[Competition]
(
	[CompetitionId] INT NOT NULL IDENTITY(1,1),
	[CompetitionName] NVARCHAR(MAX) NOT NULL,
	[Country] NVARCHAR(100) NULL,

	CONSTRAINT [PK_CompetitionId] PRIMARY KEY ([CompetitionId])
)
