using Oracle.ManagedDataAccess.Client;

using RentalHub.Repositories;

public class BookingRepository : RepositoryBase
{
    public List<BookingModel> RetrieveBookings(string userId)
    {
        string query = @"
            SELECT 
                BOOKINGS.BOOKINGID, 
                USERS.FIRSTNAME||' '||USERS.LASTNAME AS FULLNAME, 
                APARTMENTS.NAME, 
                BOOKINGS.CHECKINDATE, 
                BOOKINGS.CHECKOUTDATE, 
                BOOKINGS.TOTALPRICE, 
                BOOKINGS.STATUS, 
                BOOKINGS.CREATEDAT 
            FROM 
                BOOKINGS, USERS, APARTMENTS 
            WHERE 
                BOOKINGS.GUESTID = :USERID AND 
                USERS.USERID = BOOKINGS.GUESTID AND 
                APARTMENTS.APARTMENTID = BOOKINGS.APARTMENTID";

        var parameters = new OracleParameter[]
        {
            new OracleParameter("USERID", OracleDbType.Varchar2) { Value = userId }
        };

        var results = ExecuteQuery(query, reader => new BookingModel
        {
            BookingId = reader["BOOKINGID"]?.ToString(),
            GuestName = reader["FULLNAME"]?.ToString(),
            ApartmentName = reader["NAME"]?.ToString(),
            CheckInDate = reader["CHECKINDATE"] != DBNull.Value ?
                            (DateTime)reader.GetDateTime(reader.GetOrdinal("CHECKINDATE")) : DateTime.MinValue,
            CheckOutDate = reader["CHECKOUTDATE"] != DBNull.Value ?
                            (DateTime)reader.GetDateTime(reader.GetOrdinal("CHECKOUTDATE")) : DateTime.MinValue,
            TotalPrice = reader["TOTALPRICE"]?.ToString(),
            Status = reader["STATUS"]?.ToString(),
            CreateDate = reader["CREATEDAT"] != DBNull.Value ?
                            (DateTime)reader.GetDateTime(reader.GetOrdinal("CREATEDAT")) : DateTime.MinValue
        }, parameters);

        return results.ToList();
    }
}
