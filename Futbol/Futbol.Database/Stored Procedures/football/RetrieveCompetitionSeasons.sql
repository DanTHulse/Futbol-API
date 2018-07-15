CREATE PROCEDURE [football].[RetrieveCompetitionSeasons]
	@competitionId int
AS
	SELECT DISTINCT
		 s.[SeasonPeriod]
		,s.[SeasonId]
		,COUNT(m.[MatchId]) AS [MatchCount]
	FROM [football].[Match] AS m
	INNER JOIN [football].[Season] AS s ON s.[SeasonId] = m.[SeasonId]
	
	WHERE (@competitionId != 0 AND m.[CompetitionId] = @competitionId)
	   OR (@competitionId = 0 AND m.[CompetitionId] = m.[CompetitionId])

	GROUP BY s.[SeasonPeriod], s.[SeasonId]
	ORDER BY s.[SeasonPeriod] DESC