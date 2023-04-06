using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourGuests
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Surname { get; set; }
        public UserPresence Presence { get; set; }
        public String CheckPointName { get; set; }

        public TourGuests() { }

        public TourGuests(int id,String name,String surname,UserPresence presence,String CheckPoint)
        {
            Id = id;    
            Name = name;    
            Surname = surname;
            Presence = presence;
            CheckPointName = CheckPoint;
        }
    }
}
