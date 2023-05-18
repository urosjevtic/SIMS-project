using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
    public class ShowingTour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public List<CheckPoint> CheckPoints { get; set; }
        public int MaxGuests { get; set; }
        public DateTime Start { get; set; }
        public int Duration { get; set; }
        public string CoverImageUrl { get; set; }

        public ShowingTour()
        {
            CheckPoints = new List<CheckPoint>();
            //CoverImageUrl = new Image();    
        }

        public ShowingTour(int id,string name, string location, string description, string language, List<CheckPoint> checkPoints, int max, DateTime start, int duration, string image)
        {
            Id = id;
            Name = name;    
            Location = location;
            Description = description;
            Language = language;    
            CheckPoints = checkPoints;
            MaxGuests = max;
            Start = start;
            Duration = duration;
            CoverImageUrl = image;
        }
    }
}
