using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;

namespace InitialProject.Service
{
    public class RatedGuideTourService
    {
        private readonly IRatedGuideTourRepository _ratedGuideTourRepository;

        public RatedGuideTourService()
        {
            _ratedGuideTourRepository = Injector.Injector.CreateInstance<IRatedGuideTourRepository>();
        }
        public void Create(User user,int idTour, int guideKnowledge, int guideLanguage, int interestingTour)
        {
            RatedGuideTour rate = new(user, idTour,  guideKnowledge, guideLanguage, interestingTour);
            rate.Id = _ratedGuideTourRepository.NextId();
            _ratedGuideTourRepository.Save(rate);
        }
        public List<RatedGuideTour> GetAll()
        {
            return _ratedGuideTourRepository.GetAll();
        }

        public List<RatedGuideTour> FindAllTourRatings(Tour tour)
        {
            List<RatedGuideTour> ratings = new List<RatedGuideTour>();
            foreach(RatedGuideTour ratedTour in _ratedGuideTourRepository.GetAll())
            {
                if(tour.Id == ratedTour.IdTour)
                {
                    ratings.Add(ratedTour);
                }
            }
            return ratings;
        }
    }
}
