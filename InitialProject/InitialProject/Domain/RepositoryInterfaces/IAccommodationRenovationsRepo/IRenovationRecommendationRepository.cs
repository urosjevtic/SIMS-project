using InitialProject.Domain.Model.AccommodationRenovation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces.IAccommodationRenovationsRepo
{
    public interface IRenovationRecommendationRepository
    {
        List<RenovationRecommendation> GetAll();
        List<RenovationRecommendation> GetByReservationId(int id);

        void Delete(RenovationRecommendation renovationRecommendation);
        void Save(RenovationRecommendation renovationRecommendation);
    }
}

