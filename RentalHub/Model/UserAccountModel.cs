
using System.Windows.Media.Imaging;

namespace RentalHub.Model
{
    public class UserAccountModel
    {
        public string Username { get; set; }
        public string DisplayName {  get; set; }
        public BitmapImage ProfilePicture { get; set; }
    }
}
