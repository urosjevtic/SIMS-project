using InitialProject.Serializer;
using System;

namespace InitialProject.Model
{
    public enum UserRole { Guest, Owner, Guide, Guest2}
    public enum UserPresence { Yes, No, Unknown }
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserPresence Presence { get; set; }
        public UserRole Role { get; set; }

        public User() { }

        public User(string username, string password, UserRole role)
        {
            Username = username;
            Password = password;
            Role = role;
            Presence = UserPresence.Unknown;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString(), Presence.ToString() };
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
            switch (values[4])
            {
                case "Yes":
                    Presence = UserPresence.Yes;
                    break;
                case "No":
                    Presence = UserPresence.No;
                    break;
                case "Unknown":
                    Presence = UserPresence.Unknown;
                    break;
            }
        }
    }
}
