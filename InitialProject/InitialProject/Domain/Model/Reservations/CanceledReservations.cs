using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model.Reservations
{
    public class CanceledReservations : Serializer.ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }

        public CanceledReservations() { }

        public CanceledReservations(int id, int reservationId)
        {
            Id = id;
            ReservationId = reservationId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ReservationId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
        }
    }

} 
