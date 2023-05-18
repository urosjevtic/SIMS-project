using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Reservations;

namespace InitialProject.Domain.Model
{
    public class RatedGuest : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation {get; set; }
        public int RuleFollowingRating { get; set; }
        public int CleanlinessRating { get; set; }
        public string AdditionalComment { get; set; }


        public RatedGuest()
        {
            Reservation = new AccommodationReservation();
        }

        public RatedGuest(int id, AccommodationReservation reservation, int ruleFollowingRating, int cleanlinessRating, string additionalComment)
        {
            Id = id;
            Reservation = reservation;
            RuleFollowingRating = ruleFollowingRating;
            CleanlinessRating = cleanlinessRating;
            AdditionalComment = additionalComment;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Reservation.Id.ToString(), RuleFollowingRating.ToString(), CleanlinessRating.ToString(), AdditionalComment};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation.Id = Convert.ToInt32(values[1]);
            RuleFollowingRating = Convert.ToInt32(values[2]);
            CleanlinessRating = Convert.ToInt32(values[3]);
            AdditionalComment = values[4];
        }
    }
}
