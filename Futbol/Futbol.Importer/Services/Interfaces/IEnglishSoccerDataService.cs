namespace Futbol.Importer.Services.Interfaces
{
    public interface IEnglishSoccerDataService : IService
    {
        /// <summary>
        /// Imports the data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void ImportData(string fileName);
    }
}
