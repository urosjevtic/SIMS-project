using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Serializer;

namespace InitialProject.Model 
{
    public enum UserPresence { Yes, No, Unknown }
    public class TourGuest : ISerializable
    {
       
        public int Id { get; set; }    
        public String Username { get; set; }
        public string Password { get; set; } 
        public UserRole Role { get; set; }

        public String Name { get; set; }    
        public String Surname { get; set; }
        public UserPresence Presence { get; set; }  

        public String CheckPointName { get; set; }
        

        public TourGuest()
        {
          Presence = UserPresence.Unknown;
            CheckPointName = "";
        }
       public TourGuest(int id,string username,string pass,UserRole role, String name, string surname, UserPresence userPresence, String checkPointName)
        {
            Id = id;
            Username = username;
            Password = pass;
            Role = role;
            Name = name;
            Surname = surname;
            Presence = userPresence;
            CheckPointName = checkPointName;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString(), Name, Surname,Presence.ToString(),CheckPointName };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            switch (values[3])
            {
                case "Guest":
                    Role = UserRole.Guest;
                    break;
                case "Guest2":
                    Role = UserRole.Guest2;
                    break;
                case "Owner":
                    Role = UserRole.Owner;
                    break;
                case "Guide":
                    Role = UserRole.Guide;
                    break;
            }
            Name = values[4];   
            Surname = values[5];
            switch (values[6])
            {
                case "No":
                    Presence = UserPresence.No;
                    break;
                case "Yes":
                    Presence = UserPresence.Yes;
                    break;
                case "Unknown":
                    Presence = UserPresence.Unknown;
                    break;
                
            }
            CheckPointName = values[7];
        }


    }
}
