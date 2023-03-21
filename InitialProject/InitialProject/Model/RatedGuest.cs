using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class RatedGuest : ISerializable
    {
        public int Id { get; set; } 
        public User User { get; set; }
        public int RuleFollowingRating { get; set; }
        public int CleanlinessRating { get; set; }
        public string AdditionalComment { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }


        public RatedGuest() 
        {
            User = new User();
            Accommodation = new Accommodation();
        }

        public RatedGuest(int id, User user, int ruleFollowingRating, int cleanlinessRating, string additionalComment, Accommodation accommodation, DateTime reservationStartDate, DateTime reservationEndDate)
        {
            Id = id;
            User = user;
            RuleFollowingRating = ruleFollowingRating;
            CleanlinessRating = cleanlinessRating;
            AdditionalComment = additionalComment;
            Accommodation = accommodation;
            ReservationStartDate = reservationStartDate;
            ReservationEndDate = reservationEndDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), User.Id.ToString(), RuleFollowingRating.ToString(), CleanlinessRating.ToString(), AdditionalComment, Accommodation.Id.ToString(), ReservationStartDate.ToString("dd'/'MM'/'yyyy"), ReservationEndDate.ToString("dd'/'MM'/'yyyy") };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User.Id = Convert.ToInt32(values[1]);
            RuleFollowingRating = Convert.ToInt32(values[2]);
            CleanlinessRating = Convert.ToInt32(values[3]);
            AdditionalComment = values[4];
            Accommodation.Id = Convert.ToInt32(values[5]);
            ReservationStartDate = DateTime.ParseExact(values[6], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            ReservationEndDate = DateTime.ParseExact(values[7], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
        }
    }
}
