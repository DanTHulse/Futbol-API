CREATE TABLE [football].[MatchData]
(
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

	CONSTRAINT [FK_Match_MatchId] FOREIGN KEY ([MatchId]) REFERENCES [football].[Match]([MatchId])
)
