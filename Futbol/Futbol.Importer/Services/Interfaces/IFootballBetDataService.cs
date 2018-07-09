namespace Futbol.Importer.Services.Interfaces
{
    public interface IFootballBetDataService : IService
    {
        void ImportBetData(string fileName, string competitionName, int seasonStart);
    }
}
