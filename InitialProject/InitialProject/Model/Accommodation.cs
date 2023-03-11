using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{   
    public enum AccommodationType { appartment, house, cabin}
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinReservationDays { get; set; }
        public int CancelationPeriod { get; set; }
        public List<String> Images { get; set; }

        
        public Accommodation() 
        {
            Location = new Location();
        }

        public Accommodation(int id, string name, Location location, AccommodationType type, int maxGuests, int minReservationDays, int cancelationPeriod, List<string> images)
        {
            Id = id;
            Name = name;
            Location = location;
            Type = type;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancelationPeriod = cancelationPeriod;
            Images = images;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Location.Id.ToString(), Type.ToString(), MaxGuests.ToString(), MinReservationDays.ToString(), CancelationPeriod.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location.Id = Convert.ToInt32(values[2]);
            switch(values[3])
            {
                case "appartment":
                    Type = AccommodationType.appartment;
                    break;
                case "house":
                    Type = AccommodationType.house; 
                    break;
                case "cabin":
                    Type = AccommodationType.cabin;
                    break;
            }
            MaxGuests = Convert.ToInt32(values[4]);
            MinReservationDays = Convert.ToInt32(values[5]);
            CancelationPeriod = Convert.ToInt32(values[6]);

        }

    }
}
