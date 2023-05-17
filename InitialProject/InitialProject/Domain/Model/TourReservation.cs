using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public class TourReservation : ISerializable
    {
        public int IdReservation { get; set; }
        public int IdTour { get; set; }
        public int IdGuest { get; set; }
        public int NumberOfPeople { get; set; }
        public bool IsUsingVoucher { get; set; }
        public double AverageAge { get; set; }
        public DateTime DateAndTime { get; set; }
        public TourReservation() { }


        public string[] ToCSV()
        {
            string[] csvValues = {
               IdReservation.ToString(),
               IdTour.ToString(),
               IdGuest.ToString(),
               NumberOfPeople.ToString(),
               IsUsingVoucher.ToString(),
               AverageAge.ToString(),
               DateAndTime.ToString(),
            };

            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            IdReservation = Convert.ToInt32(values[0]);
            IdTour = Convert.ToInt32(values[1]);
            IdGuest = Convert.ToInt32(values[2]);
            NumberOfPeople = Convert.ToInt32(values[3]);
            IsUsingVoucher = Convert.ToBoolean(values[4]);
            AverageAge = Convert.ToDouble(values[5]);
            DateAndTime = Convert.ToDateTime(values[6]);
        }
    }
}
