using Futbol.Common.Models.Football;
using Microsoft.EntityFrameworkCore;

namespace Futbol.Common.Infrastructure
{
    public class FutbolContext : DbContext
    {
        public virtual DbSet<Competition> Competition { get; set; }

        public virtual DbSet<Match> Match { get; set; }

        public virtual DbSet<MatchData> MatchData { get; set; }

        public virtual DbSet<Season> Season { get; set; }

        public virtual DbSet<Team> Team { get; set; }

        public FutbolContext(DbContextOptions<FutbolContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Competition>(entity =>
            {
                entity.ToTable("Competition", schema: "football");

                entity.HasKey(k => k.CompetitionId);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("Match", schema: "football");

                entity.HasKey(k => k.MatchId);
                entity.HasOne(e => e.Competition)
                    .WithMany(e => e.Matches)
                    .HasForeignKey(f => f.CompetitionId).HasConstraintName("FK_Competition_CompetitionId");
                entity.HasOne(e => e.Season)
                    .WithMany(e => e.Matches)
                    .HasForeignKey(f => f.SeasonId)
                    .HasConstraintName("FK_Season_SeasonId");
                entity.HasOne(e => e.HomeTeam)
                    .WithMany(e => e.HomeMatches)
                    .HasForeignKey(f => f.HomeTeamId)
                    .HasConstraintName("FK_Team_HomeTeamId");
                entity.HasOne(e => e.AwayTeam)
                    .WithMany(e => e.AwayMatches)
                    .HasForeignKey(f => f.AwayTeamId)
                    .HasConstraintName("FK_Team_AwayTeamId");
                entity.HasOne(e => e.MatchData)
                    .WithOne(e => e.Match)
                    .HasForeignKey<MatchData>();
            });

            modelBuilder.Entity<MatchData>(entity =>
            {
                entity.ToTable("MatchData", schema: "football");

                entity.HasKey(k => k.MatchDataId);
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.ToTable("Season", schema: "football");

                entity.HasKey(k => k.SeasonId);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Team", schema: "football");

                entity.HasKey(k => k.TeamId);
            });
        }
    }
}
