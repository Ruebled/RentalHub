using System.Windows.Media.Imaging;

namespace RentalHub.Model
{
    public class ApartmentModel
    {
        public string? ApartmentID { get; set; }
        public string? HostID { get; set; }
        public string? Name { get; set; }
        public string? Description {  get; set; }
        public string? AddressLine {  get; set; }
        public string? CityID { get; set; }
        public string? CityName { get; set; }
        public string? ZipCode {  get; set; }
        public string? PricePerNight { get; set; }
        public BitmapImage? MainImage { get; set; }
        public List<BitmapImage>? ImageGallery { get; set; }
    }
}
