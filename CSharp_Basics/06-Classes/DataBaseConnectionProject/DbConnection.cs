namespace DataBaseConnectionProject
{
    internal abstract class DbConnection
    {
        public DbConnection(string? connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string was null or empty");
            }

            ConnectionString = connectionString.Trim();
        }
        public string ConnectionString { get; set; }
        public TimeSpan Timeout { get; set; }

        public abstract void OpenConnection();
        public abstract void CloseConnection();
    }
}
