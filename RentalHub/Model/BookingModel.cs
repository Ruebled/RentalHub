using System.ComponentModel;
using System.Runtime.CompilerServices;

public class BookingModel : INotifyPropertyChanged
{
    private string _bookingId;
    public string BookingId
    {
        get { return _bookingId; }
        set { _bookingId = value; OnPropertyChanged(); }
    }
    private string _guestName;
    public string GuestName
    {
        get { return _guestName; }
        set { _guestName = value; OnPropertyChanged(); }
    }

    private string _apartmentName;
    public string ApartmentName
    {
        get { return _apartmentName; }
        set { _apartmentName = value; OnPropertyChanged(); }
    }

    private DateTime _checkInDate;
    public DateTime CheckInDate
    {
        get { return _checkInDate; }
        set { _checkInDate = value; OnPropertyChanged(); }
    }

    private DateTime _checkOutDate;
    public DateTime CheckOutDate
    {
        get { return _checkOutDate; }
        set { _checkOutDate = value; OnPropertyChanged(); }
    }

    private string _totalPrice;
    public string TotalPrice
    {
        get { return _totalPrice; }
        set { _totalPrice = value; OnPropertyChanged(); }
    }

    private string _status;
    public string Status
    {
        get { return _status; }
        set { _status = value; OnPropertyChanged(); }
    }

    private DateTime _createDate;
    public DateTime CreateDate
    {
        get { return _createDate; }
        set { _createDate = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
