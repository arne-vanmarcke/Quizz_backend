namespace BackendApp.Models
{
    public class HighscoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string HighscoreCollectionName { get; set; } = null!;
    }
}
