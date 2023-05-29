using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
    public class SuperGuideStatus : ISerializable
    {
        public int Id { get; set; } 
        public string Language { get; set; }
        public double AverageRating { get; set; }      
        public int ToursNumber { get; set; }

        public SuperGuideStatus(string language, double averageRating, int toursNumber)
        {
            Language = language;
            AverageRating = averageRating;  
            ToursNumber = toursNumber;  
        }

        public SuperGuideStatus()
        {

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Language, AverageRating.ToString(), ToursNumber.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Language = values[1];
            AverageRating = Convert.ToDouble(values[2]);
            ToursNumber = Convert.ToInt32(values[3]);
        }
    }
}
