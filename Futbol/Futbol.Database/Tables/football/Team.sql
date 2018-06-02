CREATE TABLE [football].[Team]
(
	[TeamId] INT NOT NULL IDENTITY(1,1),
	[TeamName] NVARCHAR(MAX) NOT NULL,
	[AlternateTeamName] NVARCHAR(MAX) NULL,

	CONSTRAINT [PK_TeamId] PRIMARY KEY ([TeamId])
)
