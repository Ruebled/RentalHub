using System.Windows.Media.Imaging;

namespace RentalHub.Model
{
    public class ApartmentModel
    {
        public string? ApartmentID { get; set; }
        public string? HostID { get; set; }
        public string? Name { get; set; }
        public string? Description {  get; set; }
        public string? AddresLine {  get; set; }
        public string? CountryID { get; set; }
        public string? ZipCode {  get; set; }
        public string? PricePerNight { get; set; }
        public BitmapImage? MainImage { get; set; }
        public List<BitmapImage>? ImageGallery { get; set; }
    }
}
