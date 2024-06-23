using System.ComponentModel;
using System.Runtime.CompilerServices;

public class BookingModel : INotifyPropertyChanged
{
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

    // Add more properties as needed, such as guest name, price, etc.

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
