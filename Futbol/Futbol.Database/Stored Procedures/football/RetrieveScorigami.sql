CREATE PROCEDURE [football].[RetrieveScorigami]
	@CompetitionId int = 0,
	@SeasonId int = 0
AS
with matches AS (
SELECT
		 IIF(md.[FTResult] = 'H' OR md.[FTResult] = 'D', md.[FTHomeGoals], md.[FTAwayGoals]) AS [Score_1]
		,IIF(md.[FTResult] = 'H' OR md.[FTResult] = 'D', md.[FTAwayGoals], md.[FTHomeGoals]) AS [Score_2]
		,md.[MatchId]
	FROM [football].[MatchData] AS md
	INNER JOIN [football].[Match] AS m ON m.[MatchId] = md.[MatchId]

	WHERE ((@CompetitionId = 0 AND m.[CompetitionId] = m.[CompetitionId])
			OR (@CompetitionId != 0 AND m.[CompetitionId] = @CompetitionId))
	  AND ((@SeasonId = 0 AND m.[SeasonId] = m.[SeasonId])
			OR (@SeasonId != 0 AND m.[SeasonId] = @SeasonId))
)

SELECT
		 m.[Score_1]
		,m.[Score_2]
		,COUNT(m.[MatchId]) AS [Count]
	FROM matches AS m
	WHERE m.[Score_1] IS NOT NULL
	GROUP BY m.[Score_1], m.[Score_2]
	ORDER BY m.[Score_1], m.[Score_2]