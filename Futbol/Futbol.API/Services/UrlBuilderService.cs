using System;
using Futbol.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Futbol.API.Helpers
{
    public class UrlBuilderService : IUrlBuilderService
    {
        private readonly string FBUrl;

        private readonly string StatsUrl;

        private readonly string ApiUrl;

        public UrlBuilderService(IConfigurationRoot config)
        {
            this.ApiUrl = config.GetValue<string>("AppSettings:ApiBaseUrl");
            this.FBUrl = $"{this.ApiUrl}/{config.GetValue<string>("AppSettings:FootballApiResource")}";
            this.StatsUrl = $"{this.ApiUrl}/{config.GetValue<string>("AppSettings:StatsApiResource")}";
        }

        public Uri AllMatchesForScoreline(int firstBoxScore, int secondBoxScore, int? competitionId, int? seasonId)
            => new Uri($"{this.FBUrl}/Matches?boxScoreFirst={firstBoxScore}&boxScoreSecond={secondBoxScore}&competitionId={competitionId}&seasonId={seasonId}");

        public Uri AllMatchesForTeamScoreline(int firstBoxScore, int secondBoxScore, int teamId, int? competitionId, int? seasonId)
            => new Uri($"{this.FBUrl}/Matches?boxScoreFirst={firstBoxScore}&boxScoreSecond={secondBoxScore}&teamId={teamId}&competitionId={competitionId}&seasonId={seasonId}");

        public Uri AllMatchesTeams(int firstTeamId, int secondTeamId, int? competitionId, int? seasonId)
            => new Uri($"{this.FBUrl}/Matches?teamId={firstTeamId}&teamId_2={secondTeamId}&competitionId={competitionId}&seasonId={seasonId}");

        public Uri AllMatchesTeam(int teamId, int? competitionId = null, int? seasonId = null)
            => new Uri($"{this.FBUrl}/Matches?teamId={teamId}&competitionId={competitionId}&seasonId={seasonId}");

        public Uri AllMatchesCompetition(int competitionId)
            => new Uri($"{this.FBUrl}/Matches?competitionId={competitionId}");

        public Uri AllMatchesSeason(int seasonId)
            => new Uri($"{this.FBUrl}/Matches?seasonId={seasonId}");

        public Uri AllMatchesCompetitionSeason(int competitionId, int seasonId)
            => new Uri($"{this.FBUrl}/Matches?competitionId={competitionId}&seasonId={seasonId}");
    
        public Uri ScoreStats(int? firstBoxScore, int? secondBoxScore, int? competitionId, int? seasonId, bool fullTime)
            => new Uri($"{this.StatsUrl}/Scores/{firstBoxScore}/{secondBoxScore}?competitionId={competitionId}&seasonId={seasonId}&fullTime={fullTime}");

        public Uri FixtureStats(int homeTeamId, int awayTeamId, int? competitionId, int? seasonId)
            => new Uri($"{this.StatsUrl}/Fixtures/{homeTeamId}/{awayTeamId}?competitionId={competitionId}&seasonId={seasonId}");

        public Uri TeamStats(int teamId, int? competitionId = null, int? seasonId = null)
            => new Uri($"{this.StatsUrl}/Teams/{teamId}?competitionId={competitionId}&seasonId={seasonId}");

        public Uri CompetitionSeasonStats(int competitionId, int seasonId)
            => new Uri($"{this.StatsUrl}/Competitions/{competitionId}/Seasons/{seasonId}");

        public Uri CompetitionReference(int competitionId)
            => new Uri($"{this.FBUrl}/Competitions/{competitionId}");

        public Uri SeasonReference(int seasonId)
            => new Uri($"{this.FBUrl}/Seasons/{seasonId}");

        public Uri TeamReference(int teamId)
            => new Uri($"{this.FBUrl}/Teams/{teamId}");

        public Uri MatchReference(int matchId)
            => new Uri($"{this.FBUrl}/Matches?matchId={matchId}");
    }
}
