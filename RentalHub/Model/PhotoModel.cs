using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHub.Model
{
    public class PhotoModel
    {
        public string ApartmentImageId { get; set; }
        public string ApartmentId { get; set; }
        public string PhotoURL {  get; set; }
        public string UploadedDate { get; set; }
    }
}
