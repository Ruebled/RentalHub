using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace RentalHub.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;

        protected RepositoryBase()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        protected OracleConnection GetConnection()
        {
            return new OracleConnection(_connectionString);
        }

        protected IEnumerable<T> ExecuteQuery<T>(string query, Func<OracleDataReader, T> map, params OracleParameter[] parameters)
        {
            var results = new List<T>();

            using (var connection = GetConnection())
            using (var command = new OracleCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(map(reader));
                    }
                }
            }

            return results;
        }


        protected int ExecuteNonQuery(string query, params OracleParameter[] parameters)
        {
            using (var connection = GetConnection())
            using (var command = new OracleCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        protected object ExecuteScalar(string query, params OracleParameter[] parameters)
        {
            using (var connection = GetConnection())
            using (var command = new OracleCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                connection.Open();
                return command.ExecuteScalar();
            }
        }
    }
}

