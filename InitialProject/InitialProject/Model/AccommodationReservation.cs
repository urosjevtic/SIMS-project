
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public User Guest { get; set; }
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
        public List<DateTime> FreeDates { get; set; }

        public AccommodationReservation()
        {
            Guest = new User();
            Accommodation= new Accommodation();
        }

        public AccommodationReservation(int id, DateTime startDate, DateTime endDate, int userId, User guest, int accommodationId, Accommodation accommodation, List<DateTime>freeDates)
        {  
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId;
            Guest=guest;
            AccommodationId= accommodationId;
            Accommodation = accommodation;
            FreeDates = freeDates;
            
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), StartDate.ToString(), EndDate.ToString(), UserId.ToString(), Guest.Id.ToString(), AccommodationId.ToString(), Accommodation.Id.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest.Id = Convert.ToInt32(values[1]);
            StartDate = DateTime.Parse(values [2]);
            EndDate = DateTime.Parse(values[3]);
            UserId= Convert.ToInt32(values[4]); 
            Guest.Id= Convert.ToInt32(values[5]);
            AccommodationId= Convert.ToInt32(values[6]);
            Accommodation.Id= Convert.ToInt32(values[7]);
        }

    }
}