using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
    public enum AccommodationType { appartment, house, cabin }
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinReservationDays { get; set; }
        public int CancelationPeriod { get; set; }
        public Image Images { get; set; }

        public bool IsRecentlyRenovated {get; set; }


        public Accommodation()
        {
            Owner = new User();
            Location = new Location();
            Images = new Image();
        }

        public Accommodation(int id, User owner, string name, Location location, AccommodationType type, int maxGuests, int minReservationDays, int cancelationPeriod, Image images, bool isRecentlyRenovated)
        {
            Id = id;
            Owner = owner;
            Name = name;
            Location = location;
            Type = type;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancelationPeriod = cancelationPeriod;
            Images = images;
            IsRecentlyRenovated = isRecentlyRenovated;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Owner.Id.ToString(), Name, Location.Id.ToString(), Type.ToString(), MaxGuests.ToString(), MinReservationDays.ToString(), CancelationPeriod.ToString(), Images.Id.ToString(), IsRecentlyRenovated.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Owner.Id = Convert.ToInt32(values[1]);
            Name = values[2];
            Location.Id = Convert.ToInt32(values[3]);
            switch (values[4])
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
            MaxGuests = Convert.ToInt32(values[5]);
            MinReservationDays = Convert.ToInt32(values[6]);
            CancelationPeriod = Convert.ToInt32(values[7]);
            Images.Id = Convert.ToInt32(values[8]);
            IsRecentlyRenovated = Convert.ToBoolean(values[9]);

        }
        public string Concatenate()
        {
            return Name + " " + Location.Country + " " + Location.City + " " + Type;
        }
    }
}
