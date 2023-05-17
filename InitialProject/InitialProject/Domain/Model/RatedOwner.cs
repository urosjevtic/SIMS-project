using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InitialProject.Domain.Model.Reservations

{
    public class RatedOwner : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public int OwnerCorrectness { get; set; }
        public int CleanlinessRating { get; set; }
        public string AdditionalComment { get; set; }

        //public Image ImageUrl { get; set; }
        public string ImageUrl { get; set; }

        public RatedOwner()
        {
            Reservation = new AccommodationReservation();
            //ImageUrl = new Image();
        }

        public RatedOwner(int id, AccommodationReservation reservation, int ownerCorrectness, int cleanlinessRating, string additionalComment, string imageUrl)
        {
            Id = id;
            Reservation = reservation;
            OwnerCorrectness = ownerCorrectness;
            CleanlinessRating = cleanlinessRating;
            AdditionalComment = additionalComment;
            ImageUrl = imageUrl;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Reservation.Id.ToString(), OwnerCorrectness.ToString(), CleanlinessRating.ToString(), AdditionalComment, ImageUrl };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation.Id = Convert.ToInt32(values[1]);
            OwnerCorrectness = Convert.ToInt32(values[2]);
            CleanlinessRating = Convert.ToInt32(values[3]);
            AdditionalComment = values[4];
            ImageUrl = values[5];
        }
    }
}


