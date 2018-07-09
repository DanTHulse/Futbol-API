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
            this.ApiUrl = config.GetValue<string>("AppSettings:ApiUrl");
            this.FBUrl = $"{this.ApiUrl}/{config.GetValue<string>("AppSettings:FootballApiResource")}";
            this.StatsUrl = $"{this.ApiUrl}/{config.GetValue<string>("AppSettings:StatsApiResource")}";
        }

        public Uri AllMatchesForScoreline(int firstBoxScore, int secondBoxScore, int? competitionId, int? seasonId)
            => new Uri($"{this.FBUrl}?boxScoreFirst={firstBoxScore}&boxScoreSecond={secondBoxScore}&competitionId={competitionId}&seasonId={seasonId}");

        public Uri MatchData(int matchId)
            => new Uri($"{this.FBUrl}?matchId={matchId}");

        public Uri ScoreStats(int? firstBoxScore, int? secondBoxScore, int? competitionId, int? seasonId, bool fullTime)
            => new Uri($"{this.StatsUrl}/Scores/{firstBoxScore}/{secondBoxScore}?competitionId={competitionId}&seasonId={seasonId}&fullTime={fullTime}");

        public Uri AllMatchesForTeamScoreline(int firstBoxScore, int secondBoxScore, int teamId, int? competitionId, int? seasonId)
            => new Uri($"{this.FBUrl}?boxScoreFirst={firstBoxScore}&boxScoreSecond={secondBoxScore}&teamId={teamId}&competitionId={competitionId}&seasonId={seasonId}");

        public Uri AllMatchesTeams(int firstTeamId, int secondTeamId, int? competitionId, int? seasonId)
            => new Uri($"{this.FBUrl}?teamId={firstTeamId}&teamId_2={secondTeamId}&competitionId={competitionId}&seasonId={seasonId}");

        public Uri FixtureStats(int homeTeamId, int awayTeamId, int? competitionId, int? seasonId)
            => new Uri($"{this.StatsUrl}/{homeTeamId}/{awayTeamId}?competitionId={competitionId}&seasonId={seasonId}");
    }
}
