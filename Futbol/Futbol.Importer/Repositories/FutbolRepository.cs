using System;
using System.Collections.Generic;
using System.Linq;
using Futbol.Common.Infrastructure;
using Futbol.Common.Models.Football;
using Futbol.Importer.Helpers;
using Futbol.Importer.Repositories.Interfaces;

namespace Futbol.Importer.Repositories
{
    public class FutbolRepository : IFutbolRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private FutbolContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FutbolRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public FutbolRepository(FutbolContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Retrieves the name of the team by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Team by name</returns>
        public Team RetrieveTeamByName(string name)
        {
            return this.context.Team.FirstOrDefault(w => w.TeamName == name || w.AlternateTeamName == name);
        }

        /// <summary>
        /// Retrieves the team by identifier.
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        /// <returns></returns>
        public Team RetrieveTeamById(int teamId)
        {
            return this.context.Team.FirstOrDefault(w => w.TeamId == teamId);
        }


        public void UpdateTeam(Team team)
        {
            this.context.Team.Update(team);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Retrieves the season by start year.
        /// </summary>
        /// <param name="seasonStartYear">The season start year.</param>
        /// <returns>Season by year</returns>
        public Season RetrieveSeasonByStartYear(int seasonStartYear)
        {
            var seasonCode = (seasonStartYear % 100).ToString().PadLeft(2, '0');

            return this.context.Season.FirstOrDefault(w => w.SeasonPeriod.StartsWith(seasonCode));
        }

        /// <summary>
        /// Retrieves the name of the competition by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Competition by name</returns>
        public Competition RetrieveCompetitionByName(string name)
        {
            return this.context.Competition.FirstOrDefault(w => name.StartsWith(w.CompetitionName));
        }

        /// <summary>
        /// Adds the specified record.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record">The record.</param>
        public void Add<T>(T record) where T : class
        {
            this.context.Add(record);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Inserts the matches.
        /// </summary>
        /// <param name="records">The records.</param>
        public void InsertMatches(List<Match> records)
        {
            foreach (var record in records)
            {
                try
                {
                    if (!this.context.Match.Where(w => w.MatchUid == record.MatchUid).Any())
                    {
                        var matchData = record.MatchData;
                        record.MatchData = null;

                        var match = this.context.Match.Add(record);

                        matchData.MatchId = match.Entity.MatchId;

                        this.context.MatchData.Add(matchData);
                        this.context.SaveChanges();

                        ConsoleLog.Information($"Inserted Match:", $"{record.HomeTeamId} V {record.AwayTeamId}");
                    }
                }
                catch (Exception ex)
                {
                    ConsoleLog.Error($"Failed to insert match", $"{record.HomeTeamId} V {record.AwayTeamId} in Competition: {record.CompetitionId}, Season: {record.SeasonId}");
                }
            }
        }
    }
}
