CREATE PROCEDURE [football].[RetrieveCompetitionSeasonsByTeam]
	@teamId int = 0
AS
	SELECT DISTINCT
		 c.[CompetitionId]
		,c.[CompetitionName]
		,s.[SeasonPeriod]
		,s.[SeasonId]
		,COUNT(m.[MatchId]) AS [MatchCount]
	FROM [football].[Match] AS m
	INNER JOIN [football].[Season] AS s ON s.[SeasonId] = m.[SeasonId]
	INNER JOIN [football].[Competition] AS c ON c.[CompetitionId] = m.[CompetitionId]
	
	WHERE m.[HomeTeamId] = @teamId OR m.[AwayTeamId] = @teamId

	GROUP BY c.[CompetitionId], c.[CompetitionName], s.[SeasonPeriod], s.[SeasonId]
	ORDER BY s.[SeasonPeriod] DESC, c.[CompetitionName] ASC