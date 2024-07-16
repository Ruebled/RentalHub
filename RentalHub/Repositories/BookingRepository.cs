using Oracle.ManagedDataAccess.Client;

using RentalHub.Repositories;

public class BookingRepository : RepositoryBase
{
    public static BookingRepository Instance = new BookingRepository();
    public List<BookingModel> RetrieveBookingsOfUser(string userId)
    {
        string query = @"
            SELECT 
                BOOKINGS.BOOKINGID,
                BOOKINGS.GUESTID,
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
            GuestId = reader["GUESTID"]?.ToString(),
            GuestName = reader["FULLNAME"]?.ToString(),
            ApartmentName = reader["NAME"]?.ToString(),
            CheckInDate = reader["CHECKINDATE"]?.ToString(),
            CheckOutDate = reader["CHECKOUTDATE"]?.ToString(),
            TotalPrice = reader["TOTALPRICE"]?.ToString(),
            Status = reader["STATUS"]?.ToString(),
            CreateDate = reader["CREATEDAT"]?.ToString()
        }, parameters);

        return results.ToList();
    }

    public List<BookingModel> RetrieveBookingsOfApartments(string apartmentId)
    {
        string query = @"
            SELECT 
                BOOKINGS.BOOKINGID,
                BOOKINGS.GUESTID,
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
                BOOKINGS.APARTMENTID = :APARTMENTID AND 
                USERS.USERID = BOOKINGS.GUESTID AND 
                APARTMENTS.APARTMENTID = BOOKINGS.APARTMENTID";

        var parameters = new OracleParameter[]
        {
            new OracleParameter("APARTMENTID", apartmentId)
        };

        var results = ExecuteQuery(query, reader => new BookingModel
        {
            BookingId = reader["BOOKINGID"]?.ToString(),
            GuestId = reader["GUESTID"]?.ToString(),
            GuestName = reader["FULLNAME"]?.ToString(),
            ApartmentName = reader["NAME"]?.ToString(),
            CheckInDate = reader["CHECKINDATE"]?.ToString(),
            CheckOutDate = reader["CHECKOUTDATE"]?.ToString(),
            TotalPrice = reader["TOTALPRICE"]?.ToString(),
            Status = reader["STATUS"]?.ToString(),
            CreateDate = reader["CREATEDAT"]?.ToString()
        }, parameters);

        return results.ToList();
    }
}
