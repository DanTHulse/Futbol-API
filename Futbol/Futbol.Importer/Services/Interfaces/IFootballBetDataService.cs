namespace Futbol.Importer.Services.Interfaces
{
    public interface IFootballBetDataService : IService
    {
        /// <summary>
        /// Imports the bet data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void ImportBetData(string fileName, string competitionName);
    }
}
