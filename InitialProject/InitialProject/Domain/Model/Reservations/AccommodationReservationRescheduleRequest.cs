using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model.Reservations
{
    public enum RescheduleStatus{approved, declined, pending}
    public class AccommodationReservationRescheduleRequest : ISerializable
    {
        public int Id;
        public AccommodationReservation Reservation { get; set; }
        public DateTime RescheduleStartDate { get; set; }
        public DateTime RescheduleEndDate { get; set; }
        public bool IsAlreadyReserved { get; set; }
        public RescheduleStatus Status {get; set; }

        public AccommodationReservationRescheduleRequest()
        {
            Reservation = new AccommodationReservation();
        }

        public AccommodationReservationRescheduleRequest(AccommodationReservation reservation, DateTime rescheduleStartDate, DateTime rescheduleEndDate, bool isAlreadyReserved = false, RescheduleStatus status = RescheduleStatus.pending)
        {
            Reservation = reservation;
            RescheduleStartDate = rescheduleStartDate;
            RescheduleEndDate = rescheduleEndDate;
            IsAlreadyReserved = isAlreadyReserved;
            Status = status;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
                {Id.ToString(), Reservation.Id.ToString(), RescheduleStartDate.ToString(), RescheduleEndDate.ToString(), Status.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Reservation.Id = Convert.ToInt32(values[1]);
            RescheduleStartDate = Convert.ToDateTime(values[2]);
            RescheduleEndDate = Convert.ToDateTime(values[3]);
            switch (values[4])
            {
                case "pending":
                    Status = RescheduleStatus.pending;
                    break;
                case "approved":
                    Status = RescheduleStatus.approved;
                    break;
                case "declined":
                    Status = RescheduleStatus.declined;
                    break;
            }
        }
    }

}
