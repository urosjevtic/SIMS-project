using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;

namespace InitialProject.Service
{
    public class SuperGuideStatusService
    {
        private readonly ISuperGuideStatusRepository _superGuideStatusRepository;   

        public SuperGuideStatusService()
        {
            _superGuideStatusRepository = Injector.Injector.CreateInstance<ISuperGuideStatusRepository>();

        }

        public Dictionary<string, int> GetRepetitionsLanguageNumber()
        {
            TourService _tourService = new TourService();   
            Dictionary<string, int> toursNumberAtLanguage = new Dictionary<string, int>();
            foreach (Tour tour in _tourService.GetLastYearTours())
            {
                if (toursNumberAtLanguage.ContainsKey(tour.Language))
                {
                    toursNumberAtLanguage[tour.Language]++;
                }
                else
                {
                    toursNumberAtLanguage[tour.Language] = 1;
                }
            }
            return toursNumberAtLanguage.Where(x => x.Value > 2).ToDictionary(pair => pair.Key, pair => pair.Value);

        }

        public double FindAverageRatingForLanguage(string language)
        {

            double sum = 0;
            double i = 0;
            TourService _tourService = new TourService();
            RatedGuideTourService _ratedGuideTourService = new RatedGuideTourService();  
            foreach (Tour tour in _tourService.GetLastYearTours())
            {
                foreach (RatedGuideTour ratedTour in _ratedGuideTourService.GetAll())
                {
                    if (tour.Id == ratedTour.IdTour && tour.Language.Equals(language))
                    {
                        sum += ratedTour.GuideLanguage + ratedTour.GuideKnowledge + ratedTour.InterestingTour;
                        i += 3;
                    }
                }

            }
            return sum / i;
        }


        public List<SuperGuideStatus> GetAll()
        {
            foreach(SuperGuideStatus status in _superGuideStatusRepository.GetAll())
            {
                _superGuideStatusRepository.Delete(status);
            }
            Dictionary<string, int> toursNumberAtlanguage = GetRepetitionsLanguageNumber();
            foreach (string key in toursNumberAtlanguage.Keys)
            {
                double averageRating = FindAverageRatingForLanguage(key);
                if (averageRating > 4.0)
                {
                    SuperGuideStatus superGuideStatus = new SuperGuideStatus(key, averageRating, toursNumberAtlanguage[key]);
                    _superGuideStatusRepository.Save(superGuideStatus);
                }
            }
            return _superGuideStatusRepository.GetAll();
        }



        public List<string> GetSuperGuideLanguages()
        {
            List<string> superGuideLanguages = new List<string>();
            foreach(SuperGuideStatus status in GetAll())
            {
                if (!superGuideLanguages.Contains(status.Language))
                {
                    superGuideLanguages.Add(status.Language);
                }   
            }
            return superGuideLanguages;
        }

    }
}
