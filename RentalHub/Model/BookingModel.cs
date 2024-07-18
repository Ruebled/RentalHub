using System.ComponentModel;
using System.Runtime.CompilerServices;

public record BookingModel
{
    public string? BookingId { get; init; }
    public string? GuestId { get; set; }
    public string? GuestName { get; init; }
    public string? ApartmentName { get; init; }
    public string? CheckInDate { get; init; }
    public string? CheckOutDate { get; init; }
    public string? TotalPrice { get; init; }
    public string? Status { get; init; }
    public string? CreateDate { get; init; }
}
