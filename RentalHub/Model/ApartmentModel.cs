using System.Windows.Media.Imaging;

namespace RentalHub.Model
{
    public class ApartmentModel
    {
        public string? ApartmentID { get; set; }
        public string? HostID { get; set; }
        public string? OwnerName { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AddressLine { get; set; }
        public string? CityID { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? CityName { get; set; }
        public string? ZipCode { get; set; }
        public string? PricePerNight { get; set; }
        public string? MainPhotoURL { get; set; }
        public string? SizeInSquareFeet { get; set; }
        public string? NumberOfRooms { get; set; }
        public string? AverageRating { get; set; }
        public string? NumberOfReviews {  get; set; }
        public string? HouseRules { get; set; }
        public string? CancellationPolicy { get; set; }
        public string? CreatedAt { get; set; }
    }
}
