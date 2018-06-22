CREATE TABLE [football].[Match]
(
	[MatchId] INT NOT NULL IDENTITY(1,1),
	[CompetitionId] INT NOT NULL,
	[SeasonId] INT NOT NULL,
	[MatchDate] DATETIME NOT NULL,
	[HomeTeamId] INT NOT NULL,
	[AwayTeamId] INT NOT NULL,
	[MatchUid] NVARCHAR(100) NULL,

    CONSTRAINT [PK_MatchId] PRIMARY KEY ([MatchId]),
	CONSTRAINT [FK_Competition_CompetitionId] FOREIGN KEY ([CompetitionId]) REFERENCES [football].[Competition]([CompetitionId]),
	CONSTRAINT [FK_Season_SeasonId] FOREIGN KEY ([SeasonId]) REFERENCES [football].[Season]([SeasonId]),
	CONSTRAINT [FK_Team_HomeTeamId] FOREIGN KEY ([HomeTeamId]) REFERENCES [football].[Team]([TeamId]),
	CONSTRAINT [FK_Team_AwayTeamId] FOREIGN KEY ([AwayTeamId]) REFERENCES [football].[Team]([TeamId]),
	CONSTRAINT [UC_MatchUid] UNIQUE ([MatchUid])
)

GO

CREATE NONCLUSTERED INDEX [IX_Match_CompetitionId] ON [football].[Match] ([CompetitionId]) INCLUDE ([SeasonId],[MatchDate],[HomeTeamId],[AwayTeamId],[MatchUid])
GO
CREATE NONCLUSTERED INDEX [IX_Match_SeasonId] ON [football].[Match] ([SeasonId]) INCLUDE ([CompetitionId],[MatchDate],[HomeTeamId],[AwayTeamId],[MatchUid])
GO
CREATE NONCLUSTERED INDEX [IX_Match_AwayTeamId] ON [football].[Match] ([AwayTeamId]) INCLUDE ([CompetitionId],[SeasonId],[MatchDate],[HomeTeamId],[MatchUid])
GO
CREATE NONCLUSTERED INDEX [IX_Match_HomeTeamId] ON [football].[Match] ([HomeTeamId]) INCLUDE ([CompetitionId],[SeasonId],[MatchDate],[AwayTeamId],[MatchUid])
GO