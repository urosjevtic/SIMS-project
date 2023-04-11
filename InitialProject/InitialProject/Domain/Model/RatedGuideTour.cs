using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public class RatedGuideTour : ISerializable
    {
        public int Id { get; set; }
        public int IdTour { get; set; }
        public int IdUser { get; set; }
        public int GuideKnowledge { get; set; }
        public int GuideLanguage { get; set; }
        public int InterestingTour { get; set; }

        public RatedGuideTour()
        {

        }

        public RatedGuideTour(User user,int idTour, int knowledge, int language, int interestingTour)
        {
            IdUser = user.Id;
            IdTour = idTour;
            GuideKnowledge = knowledge;
            GuideLanguage = language;
            InterestingTour = interestingTour;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
               Id.ToString(),
               IdTour.ToString(),
               IdUser.ToString(),
               GuideKnowledge.ToString(),
               GuideLanguage.ToString(),
               InterestingTour.ToString()
            };

            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IdTour = Convert.ToInt32(values[1]);
            IdUser = Convert.ToInt32(values[2]);
            GuideKnowledge = Convert.ToInt32(values[3]);
            GuideLanguage = Convert.ToInt32(values[4]);
            InterestingTour = Convert.ToInt32(values[5]);
        }
    }
}
