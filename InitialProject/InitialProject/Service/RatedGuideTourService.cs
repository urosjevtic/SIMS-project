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
    }
}
