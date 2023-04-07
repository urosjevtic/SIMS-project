using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model.Reservations
{
    public class DeclinedAccommodationReservationRescheduleRequest : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservationRescheduleRequest RescheduleRequest { get; set; }
        public string ReasonForDeclining { get; set; }

        public DeclinedAccommodationReservationRescheduleRequest()
        {
            RescheduleRequest = new AccommodationReservationRescheduleRequest();
        }

        public DeclinedAccommodationReservationRescheduleRequest(int id,
            AccommodationReservationRescheduleRequest rescheduleRequest, string reasonForDeclining)
        {
            Id = id;
            RescheduleRequest = rescheduleRequest;
            ReasonForDeclining = reasonForDeclining;
        }



        public string[] ToCSV()
        {
            string[] csvValue = { Id.ToString(), RescheduleRequest.Id.ToString(), ReasonForDeclining };
            return csvValue;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            RescheduleRequest.Id = Convert.ToInt32(values[1]);
            ReasonForDeclining = values[2];
        }
    }
}
