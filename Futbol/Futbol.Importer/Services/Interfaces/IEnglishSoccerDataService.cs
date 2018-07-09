namespace Futbol.Importer.Services.Interfaces
{
    public interface IEnglishSoccerDataService : IService
    {
        void ImportData(string fileName);
    }
}
