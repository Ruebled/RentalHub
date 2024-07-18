using Oracle.ManagedDataAccess.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHub.Repositories
{
    public class LocationRepository : RepositoryBase
    {
        public static LocationRepository Instance = new LocationRepository();

        public List<string> GetCitiesByPartial(string partial)
        {
            string query = @"SELECT NAME FROM CITIES WHERE LOWER(NAME) LIKE LOWER(:PARTIALNAME)";

            var parameters = new OracleParameter[]
            {
                new OracleParameter("PARTIALNAME", partial+"%")
            };

            var results = ExecuteQuery(query, reader => reader["NAME"].ToString(), parameters);

            return results.ToList();
        }
    }
}
