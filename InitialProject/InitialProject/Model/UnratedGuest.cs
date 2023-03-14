using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class UnratedGuest : ISerializable
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Accommodation ReservedAccommodation { get; set; }
        public DateOnly ReservationStartDate { get; set; }
        public DateOnly ReservationEndDate { get; set; }

        public UnratedGuest() 
        {
            User = new User();
            ReservedAccommodation = new Accommodation();
        }

        public UnratedGuest(int id, User user,  Accommodation reservedAccommodation, DateOnly reservationStartDate, DateOnly reservationEndDate)
        {
            Id = id;
            User = user;
            ReservedAccommodation = reservedAccommodation;
            ReservationStartDate = reservationStartDate;
            ReservationEndDate = reservationEndDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(),  User.Id.ToString(), ReservedAccommodation.Id.ToString(), ReservationStartDate.ToString(), ReservationEndDate.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User.Id = Convert.ToInt32(values[1]);
            ReservedAccommodation.Id = Convert.ToInt32(values[2]);
            ReservationStartDate = DateOnly.Parse(values[3]);
            ReservationEndDate = DateOnly.Parse(values[4]);

        }
    }
}
