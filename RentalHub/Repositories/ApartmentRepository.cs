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
        public List<ApartmentModel> GetApartments(string partialName)
        {
            string query = "SELECT * FROM APARTMENTS WHERE LOWER(NAME) LIKE LOWER(:PARTIALNAME)";

            var parameters = new OracleParameter[]
            {
                new OracleParameter("PARTIALNAME", "%" + partialName + "%")
            };

            var results = ExecuteQuery(query, reader => new ApartmentModel
            {
                ApartmentID = reader["APARTMENTID"].ToString(),
                HostID = reader["HOSTID"].ToString(),
                Name = reader["NAME"].ToString(),
                Description = reader["DESCRIPTION"].ToString(),
                AddresLine1 = reader["ADDRESSLINE1"].ToString(),
                AddresLine2 = reader["ADDRESSLINE2"].ToString(),
                CityID = reader["CITYID"].ToString(),
                StateID = reader["STATEID"].ToString(),
                CountryID = reader["COUNTRYID"].ToString(),
                ZipCodeID = reader["ZIPCODEID"].ToString(),
                Latitude = reader["LATITUDE"].ToString(),
                Longitude = reader["LONGITUDE"].ToString(),
                PricePerNight = reader["PRICEPERNIGHT"].ToString(),
                MaxGuests = reader["MAXGUESTS"].ToString(),
                ImageUrl = "https://images.pexels.com/photos/276592/pexels-photo-276592.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"
            }, parameters);

            return results.ToList();
        }
    }
}
