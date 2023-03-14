using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public User Guide { get; set; } 
        public string Name { get; set; }
        public Location Location { get; set; }       
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuests { get; set; }  
        public DateTime Start { get; set; }
        public int Duration { get; set; }   
        public string CoverImageUrl { get; set; }   

        public Tour()
        {
            Location = new Location();
            Guide = new User();
        }
        public Tour(int id, User giude, string name, Location location, string description, string language, int maxGuests, DateTime start, int duration, string coverImageUrl)
        {
            Id = id;
            Guide = giude;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            Start = start;
            Duration = duration;
            CoverImageUrl = coverImageUrl;

        }

        public string[] ToCSV()
        {
            string[] csvValues = {
               Id.ToString(),
               Guide.Id.ToString(),
               Name,
               Location.Id.ToString(),
               Description,
               Language,
               MaxGuests.ToString(),
               Start.ToString(),
               Duration.ToString(),
               CoverImageUrl
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guide.Id = Convert.ToInt32(values[1]);  
            Name = values[2];
            Location.Id = Convert.ToInt32(values[3]);   
            Description = values[4];
            Language = values[5];   
            MaxGuests = Convert.ToInt32(values[6]); 
            Start = DateTime.Parse(values[7]);
            Duration = Convert.ToInt32(values[8]);  
            CoverImageUrl = values[9];  
        }



    }
}
