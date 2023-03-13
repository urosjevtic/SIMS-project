using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class Location : ISerializable
    {
        public int Id { get; set; } 
        public String Country { get; set; } 
        public String City { get; set; }    

        public Location() { }
        public Location(int id, String country, String city)
        {
            Id = id;
            Country = country;  
            City = city;    
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Country, City };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);    
            Country=values[1];
            City = values[2];        
        }

        public static Location ToLocation(string country, string city)
        {
            return new Location { Country = country, City = city };
        }
    }
}
