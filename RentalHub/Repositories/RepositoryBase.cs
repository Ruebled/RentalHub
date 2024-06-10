using Oracle.ManagedDataAccess.Client;

namespace RentalHub.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            _connectionString = "Data Source=localhost/orcl;User Id=system;Password=Oracle12#;";
        }

        protected OracleConnection GetConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }

}
