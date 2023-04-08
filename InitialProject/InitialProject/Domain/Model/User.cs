using InitialProject.Serializer;
using System;

namespace InitialProject.Domain.Model
{
    public enum UserRole { Guest, Owner, Guide, Guest2}
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public User() { }

        public User(string username, string password, UserRole role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString()};
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
           
        }
    }
}
