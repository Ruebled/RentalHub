using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHub.Model
{
    public class ReviewModel
    {
        public string ReviewId { get; set; }
        public string BookingId { get; set; }
        public string UserFullName { get; set; }
        public string PeriodOfStaying { get; set; }
        public string Rating { get; set; }
        public string Review {  get; set; }
        public string CreateDate {  get; set; }
    }
}
