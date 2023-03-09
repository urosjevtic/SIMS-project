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
        public String Name { get; set; }
        public Location Location { get; set; }       
        public String Description { get; set; }
        public String Language { get; set; }
        public int MaxGuests { get; set; }  
        public DateTime Start { get; set; }
        public int Duration { get; set; }   
        public String CoverImageUrl { get; set; }   


        public string[] ToCSV()
        {
            string[] csvValues = {
               Id.ToString(),
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
            Name = values[1];
            Location.Id = Convert.ToInt32(values[2]);   
            Description = values[3];
            Language = values[4];   
            MaxGuests = Convert.ToInt32(values[5]); 
            Start = DateTime.Parse(values[6]);
            Duration = Convert.ToInt32(values[7]);  
            CoverImageUrl = values[8];  
        }



    }
}
