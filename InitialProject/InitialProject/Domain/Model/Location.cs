using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public class Location : ISerializable, IEquatable<Location>
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public Location() { }
        public Location(int id, string country, string city)
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
            Country = values[1];
            City = values[2];
        }

        public static Location ToLocation(string country, string city)
        {
            return new Location { Country = country, City = city };
        }

        public override string ToString()
        {
            return $"{Country}, {City}";
        }

        public bool Equals(Location other)
        {
            if (other == null)
                return false;

            return string.Equals(Country, other.Country) && string.Equals(City, other.City);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Location);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Country, City);
        }

    }
}
