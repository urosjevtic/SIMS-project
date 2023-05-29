using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model.Reservations
{

    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
        public int GuestNumber { get; set; }

        public List<DateTime> ReservedDates { get; set; }

        public bool IsAbleToCancel
        {
            get { return !(Accommodation.CancelationPeriod < (StartDate - DateTime.Now).Days); }
        }
        public User User { get; set; }
        public bool IsRated { get; set; }
        //public int broj;
        public AccommodationReservation()
        {
            Accommodation = new Accommodation();
            ReservedDates = new List<DateTime>();
            User = new User();
        }

        public AccommodationReservation(int id, DateTime startDate, DateTime endDate, int userId, int accommodationId, Accommodation accommodation, int guestNumber, List<DateTime> reservedDates, User user, bool isRated=false)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId;
            AccommodationId = accommodationId;
            Accommodation = accommodation;
            GuestNumber = guestNumber;
            User = user;
            IsRated = isRated;
            ReservedDates = reservedDates;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), StartDate.ToString(), EndDate.ToString(), UserId.ToString(), AccommodationId.ToString(), GuestNumber.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            StartDate = DateTime.Parse(values[1]);
            EndDate = DateTime.Parse(values[2]);
            UserId = Convert.ToInt32(values[3]);
            AccommodationId = Convert.ToInt32(values[4]);
            //Accommodation.Id = Convert.ToInt32(values[4]);
            GuestNumber = Convert.ToInt32(values[5]);
        }

    }
}