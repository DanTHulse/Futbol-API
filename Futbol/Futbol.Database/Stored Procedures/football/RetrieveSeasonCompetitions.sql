CREATE PROCEDURE [football].[RetrieveSeasonCompetitions]
	@seasonId int
AS
	SELECT DISTINCT
		 c.[CompetitionId]
		,c.[CompetitionName]
		,c.[Country]
		,c.[Tier]
		,COUNT(m.[MatchId]) AS [MatchCount]
	FROM [football].[Match] AS m
	INNER JOIN [football].[Competition] AS c ON c.[CompetitionId] = m.[CompetitionId]
	
	WHERE (@seasonId != 0 AND m.[SeasonId] = @seasonId)
	   OR (@seasonId = 0 AND m.[SeasonId] = m.[SeasonId])

	GROUP BY c.[CompetitionId] ,c.[CompetitionName] ,c.[Country] ,c.[Tier]
	ORDER BY c.[Country] ASC, c.[Tier] ASC