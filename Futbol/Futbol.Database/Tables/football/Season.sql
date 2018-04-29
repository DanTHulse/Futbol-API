CREATE TABLE [football].[Season]
(
	[SeasonId] INT NOT NULL IDENTITY(1,1),
	[SeasonPeriod] NVARCHAR(10) NOT NULL,

	CONSTRAINT [PK_SeasonId] PRIMARY KEY ([SeasonId])
)
