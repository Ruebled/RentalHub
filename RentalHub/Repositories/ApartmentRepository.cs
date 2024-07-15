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
        public List<ApartmentModel> GetApartmentsByCity(string partialCityName)
        {
            string query = 
                @"SELECT 
                    A.APARTMENTID,
                    A.HOSTID,
                    A.NAME,
                    A.DESCRIPTION,
                    A.ADDRESSLINE,
                    A.CITYID,
                    C.NAME AS CITYNAME,
                    A.ZIPCODE,
                    A.PRICEPERNIGHT,
                    A.SIZEINSQUAREFEET,
                    A.MAINPHOTOURL,
                    A.NUMBEROFROOMS,
                    A.AVERAGERATING,
                    A.NUMBEROFREVIEWS,
                    A.HOUSERULES,
                    A.CANCELLATIONPOLICY,
                    A.CREATEDAT
                  FROM APARTMENTS A, CITIES C
                  WHERE 
                    C.CITYID = A.CITYID
                  AND 
                    LOWER(C.NAME) LIKE LOWER(:PARTIALNAME)";

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
                AddressLine = reader["ADDRESSLINE"].ToString(),
                CityID = reader["CITYID"].ToString(),
                CityName = reader["CITYNAME"].ToString(),
                ZipCode = reader["ZIPCODE"].ToString(),
                PricePerNight = reader["PRICEPERNIGHT"].ToString(),
                MainPhotoURL = reader["MAINPHOTOURL"].ToString(),
                SizeInSquareFeet = reader["SIZEINSQUAREFEET"].ToString(),
                NumberOfRooms = reader["NUMBEROFROOMS"].ToString(),
                AverageRating = reader["AVERAGERATING"].ToString(),
                NumberOfReviews = reader["NUMBEROFREVIEWS"].ToString(),
                HouseRules = reader["HOUSERULES"].ToString(),
                CancellationPolicy = reader["CANCELLATIONPOLICY"].ToString()
            }, parameters);

            return results.ToList();
        }

        public List<ApartmentModel> GetApartmentsByCityAndDays(string partialCityName, DateTime checkInDate, DateTime checkOutDate)
        {
            string query =
                @"SELECT 
                    A.APARTMENTID,
                    A.HOSTID,
                    A.NAME,
                    A.DESCRIPTION,
                    A.ADDRESSLINE,
                    A.CITYID,
                    C.NAME AS CITYNAME,
                    A.ZIPCODE,
                    A.PRICEPERNIGHT,
                    A.MAINPHOTOURL,
                    A.SIZEINSQUAREFEET,
                    A.NUMBEROFROOMS,
                    A.AVERAGERATING,
                    A.NUMBEROFREVIEWS,
                    A.HOUSERULES,
                    A.CANCELLATIONPOLICY,
                    A.CREATEDAT
                FROM APARTMENTS A, CITIES C, BOOKINGS B
                WHERE 
                    C.CITYID = A.CITYID
                AND 
                    LOWER(C.NAME) LIKE LOWER(:PARTIALNAME)
                AND 
                    B.APARTMENTID = A.APARTMENTID
                AND
                (
                    (
                        (
                            B.CHECKINDATE >= TO_DATE(:CHECKINDATE, 'MM/DD/YYYY')
                        AND
                            B.CHECKINDATE >= TO_DATE(:CHECKOUTDATE, 'MM/DD/YYYY')
                        )OR(
                            B.CHECKOUTDATE <= TO_DATE(:CHECKINDATE, 'MM/DD/YYYY')
                        AND
                            B.CHECKOUTDATE <= TO_DATE(:CHECKOUTDATE, 'MM/DD/YYYY')
                        )
                    )
                    OR
                    B.STATUS LIKE '%Cancelled%'
                )";
            var parameters = new OracleParameter[]
            {
                new OracleParameter("PARTIALNAME", "%" + partialCityName + "%"),
                new OracleParameter("CHECKINDATE", checkInDate.Date.ToString().Split(" ")[0]),
                new OracleParameter("CHECKOUTDATE", checkOutDate.Date.ToString().Split(" ")[0])
            };

            var results = ExecuteQuery(query, reader => new ApartmentModel
            {
                ApartmentID = reader["APARTMENTID"].ToString(),
                HostID = reader["HOSTID"].ToString(),
                Name = reader["NAME"].ToString(),
                Description = reader["DESCRIPTION"].ToString(),
                AddressLine = reader["ADDRESSLINE"].ToString(),
                CityID = reader["CITYID"].ToString(),
                CityName = reader["CITYNAME"].ToString(),
                ZipCode = reader["ZIPCODE"].ToString(),
                PricePerNight = reader["PRICEPERNIGHT"].ToString(),
                MainPhotoURL = reader["MAINPHOTOURL"].ToString(),
                SizeInSquareFeet = reader["SIZEINSQUAREFEET"].ToString(),
                NumberOfRooms = reader["NUMBEROFROOMS"].ToString(),
                AverageRating = reader["AVERAGERATING"].ToString(),
                NumberOfReviews = reader["NUMBEROFREVIEWS"].ToString(),
                HouseRules = reader["HOUSERULES"].ToString(),
                CancellationPolicy = reader["CANCELLATIONPOLICY"].ToString()
            }, parameters);

            return results.ToList();
        }

        public List<ReviewModel> GetReviewsList(string apartmentId)
        {
            string query =
                @"SELECT 
                    R.REVIEWID, 
                    R.BOOKINGID, 
                    U.FIRSTNAME||' '||U.LASTNAME AS GUESTFULLNAME, 
                    'FROM : '||B.CHECKINDATE||'  TO : '||B.CHECKOUTDATE AS PERIODOFSTAYING,
                    R.RATING, 
                    R.REVIEW, 
                    R.CREATEDAT
                  FROM REVIEWS R, BOOKINGS B, USERS U
                    WHERE R.BOOKINGID = B.BOOKINGID
                    AND B.APARTMENTID = :APARTMENTID
                    AND U.USERID = B.GUESTID";

            var parameters = new OracleParameter[]
            {
                new OracleParameter("APARTMENTID", apartmentId)
            };

            var results = ExecuteQuery(query, reader => new ReviewModel
            {
                ReviewId = reader["REVIEWID"].ToString(),
                BookingId = reader["BOOKINGID"].ToString(),
                UserFullName = reader["GUESTFULLNAME"].ToString(),
                PeriodOfStaying = reader["PERIODOFSTAYING"].ToString(),
                Rating = reader["RATING"].ToString(),
                Review = reader["REVIEW"].ToString(),
                CreateDate = reader["CREATEDAT"].ToString()
            }, parameters);


            return results.ToList();
        }
    }
}
