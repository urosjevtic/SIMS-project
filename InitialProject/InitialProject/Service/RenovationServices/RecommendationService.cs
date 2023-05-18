using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service.RenovationServices
{
    public class RecommendationService
    {
        private readonly RenovationRecommendationService _renovationRecommendationService;

        public RecommendationService()
        {
            _renovationRecommendationService = new RenovationRecommendationService();
        }
      
        public void SubmitRating(UnratedOwner unratedOwner, int urgencyLevel, string recommendation)
        {
            RenovationRecommendation renovationRecommendation = new RenovationRecommendation
            {
                Reservation = unratedOwner.Reservation,
                UrgencyLevel = urgencyLevel,
                Recommendation = recommendation
            };
            
            _renovationRecommendationService.Save(renovationRecommendation);
        }
    }
}
