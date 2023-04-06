using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using InitialProject.Model;

namespace InitialProject.DTO
{
    //public enum UserRole { Guest, Owner, Guide, Guest2 }
    //public enum UserPresence { Yes, No, Unknown }
    public class GuestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  String Surname { get; set; }
        public UserPresence Presence { get; set; }
        public String CheckPointName { get; set; }  

        public GuestDTO() { }

        public GuestDTO(int id, String name,String surname, String checkPoint)
        {
            Id = id;
            Name = name;
            Surname = surname;
            CheckPointName = checkPoint;
            Presence = UserPresence.Unknown;
        }

       

    }
}
