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
        public string Username { get; set; }
        public UserRole Role { get; set; }
        public InitialProject.Model.UserPresence Presence { get; set; }

        public GuestDTO() { }

        public GuestDTO(int id, string username, UserRole role)
        {
            Id = id;
            Username = username;
            Role = role;
            Presence = UserPresence.Unknown;
        }

        public GuestDTO(int id, string username, UserRole role, UserPresence presence)
        {
            Id = id;
            Username = username;
            Role = role;
            Presence = presence;
        }

    }
}
