using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public class RescheduleRequest : ISerializable
    {
        public int Id;
        public AccommodationReservation Reservation { get; set; }
        public DateTime RescheduleStartDate { get; set; }
        public DateTime RescheduleEndDate { get; set; }
        public bool IsAlreadyReserved { get; set; }

        public RescheduleRequest()
        {
            Reservation = new AccommodationReservation();
        }

        public RescheduleRequest(AccommodationReservation reservation, DateTime rescheduleStartDate, DateTime rescheduleEndDate, bool isAlreadyReserved = false)
        {
            Reservation = reservation;
            RescheduleStartDate = rescheduleStartDate;
            RescheduleEndDate = rescheduleEndDate;
            IsAlreadyReserved = isAlreadyReserved;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
                {Id.ToString(), Reservation.Id.ToString(), RescheduleStartDate.ToString(), RescheduleEndDate.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation.Id = Convert.ToInt32(values[1]);
            RescheduleStartDate = Convert.ToDateTime(values[2]);
            RescheduleEndDate = Convert.ToDateTime(values[3]);
        }
    }

}
