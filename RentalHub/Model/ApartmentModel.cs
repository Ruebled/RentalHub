

using System.Security.Permissions;

namespace RentalHub.Model
{
    public class ApartmentModel
    {
        public string? ApartmentID { get; set; }
        public string? HostID { get; set; }
        public string? Name { get; set; }
        public string? Description {  get; set; }
        public string? AddresLine1 {  get; set; }
        public string? AddresLine2 {  get; set; }
        public string? CityID {  get; set; }
        public string? StateID {  get; set; }
        public string? CountryID { get; set; }
        public string? ZipCodeID {  get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? PricePerNight { get; set; }
        public string? MaxGuests { get; set; }
        public string? ImageUrl { get; set; }
    }
}
