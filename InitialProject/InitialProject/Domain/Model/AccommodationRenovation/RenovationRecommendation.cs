using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model.AccommodationRenovation
{
    public class RenovationRecommendation : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public string Recommendation { get; set; }
        public int UrgencyLevel { get; set; }

        public RenovationRecommendation() 
        {
            Reservation = new AccommodationReservation();    

        }
        public RenovationRecommendation(int id, AccommodationReservation reservation, string recommendation, int urgencyLevel)
        {
            Id = id;
            Reservation = reservation;
            Recommendation = recommendation;
            UrgencyLevel = urgencyLevel;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Reservation.Id.ToString(), Recommendation, UrgencyLevel.ToString()};
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation.Id = Convert.ToInt32(values[1]);
            Recommendation = values[2];
            UrgencyLevel = Convert.ToInt32(values[3]);
        }
    }
}
