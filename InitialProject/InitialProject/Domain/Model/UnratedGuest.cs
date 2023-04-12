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
    public class UnratedGuest : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; }


        public UnratedGuest()
        {
            Reservation = new AccommodationReservation();
        }

        public UnratedGuest(int id, AccommodationReservation reservation)
        {
            Id = id;
            Reservation = reservation;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),  Reservation.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation.Id = Convert.ToInt32(values[1]);

        }
    }
}
