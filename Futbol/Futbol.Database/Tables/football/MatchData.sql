CREATE TABLE [football].[MatchData]
(
	[MatchDataId] INT NOT NULL IDENTITY(1,1),
	[MatchId] INT NOT NULL,
	[FTHomeGoals] INT NULL,
	[FTAwayGoals] INT NULL,
	[FTResult] VARCHAR(1) NOT NULL,
	[HTHomeGoals] INT NULL,
	[HTAwayGoals] INT NULL,
	[HTResult] VARCHAR(1) NULL,
	[HomeShots] INT NULL,
	[AwayShots] INT NULL,
	[HomeShotsOnTarget] INT NULL,
	[AwayShotsOnTarget] INT NULL,

	CONSTRAINT [PK_MatchDataId] PRIMARY KEY ([MatchDataId]),
	CONSTRAINT [FK_Match_MatchId] FOREIGN KEY ([MatchId]) REFERENCES [football].[Match]([MatchId])
)
