using System;
using System.Collections.Generic;
using System.Linq;
using Futbol.Common.Infrastructure;
using Futbol.Common.Models.Football;
using Futbol.Importer.Helpers;
using Futbol.Importer.Infrastructure;
using Futbol.Importer.Repositories.Interfaces;

namespace Futbol.Importer.Repositories
{
    public class FutbolRepository : IFutbolRepository
    {
        private FutbolContext context;

        public FutbolRepository(FutbolContext context)
        {
            this.context = context;
        }

        public Team RetrieveTeamByName(string name)
        {
            return this.context.Team.FirstOrDefault(w => w.TeamName == name);
        }

        public Season RetrieveSeasonByStartYear(int seasonStartYear)
        {
            return this.context.Season.FirstOrDefault(w => w.SeasonPeriod.StartsWith(seasonStartYear.ToString()));
        }

        public Competition RetrieveCompetitionByName(string name)
        {
            return this.context.Competition.FirstOrDefault(w => name.StartsWith(w.CompetitionName));
        }

        public void Add<T>(T record) where T : class
        {
            this.context.Add(record);
            this.context.SaveChanges();
        }

        public void InsertMatches(List<Match> records)
        {
            foreach (var record in records)
            {
                try
                {
                    var matchData = record.MatchData;
                    record.MatchData = null;

                    var match = this.context.Match.Add(record);

                    matchData.MatchId = match.Entity.MatchId;

                    this.context.MatchData.Add(matchData);
                    this.context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ConsoleLog.Error($"Failed to insert match", $"{record.HomeTeamId} V {record.AwayTeamId} in Competition: {record.CompetitionId}, Season: {record.SeasonId}");
                }
            }
        }
    }
}
