namespace Futbol.Importer.Services.Interfaces
{
    public interface IFootballDataService : IService
    {
        void RetrieveCompetitionsBySeason(int seasonStartYear);
    }
}
