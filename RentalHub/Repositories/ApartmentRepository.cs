using Oracle.ManagedDataAccess.Client;

using RentalHub.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHub.Repositories
{
    public class ApartmentRepository : RepositoryBase
    {
        public List<ApartmentModel> GetApartments(string partialCityName)
        {
            string query = "SELECT * FROM APARTMENTS WHERE LOWER(NAME) LIKE LOWER(:PARTIALCITYNAME)";

            var parameters = new OracleParameter[]
            {
                new OracleParameter("PARTIALNAME", "%" + partialCityName + "%")
            };

            var results = ExecuteQuery(query, reader => new ApartmentModel
            {
                ApartmentID = reader["APARTMENTID"].ToString(),
                HostID = reader["HOSTID"].ToString(),
                Name = reader["NAME"].ToString(),
                Description = reader["DESCRIPTION"].ToString(),
                AddresLine = reader["ADDRESSLINE"].ToString(),
                CountryID = reader["COUNTRYID"].ToString(),
                ZipCode = reader["ZIPCODE"].ToString(),
                PricePerNight = reader["PRICEPERNIGHT"].ToString()
                }, parameters);

            return results.ToList();
        }
    }
}
