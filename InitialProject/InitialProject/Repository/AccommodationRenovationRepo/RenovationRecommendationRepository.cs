using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Domain.RepositoryInterfaces.IAccommodationRenovationsRepo;
using InitialProject.Serializer;

namespace InitialProject.Repository.AccommodationRenovationRepo
{
    public class RenovationRecommendationRepository : IRenovationRecommendationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovationRecommendation.csv";

        private readonly Serializer<RenovationRecommendation> _serializer;

        private List<RenovationRecommendation> _renovationRecommendations;

        public RenovationRecommendationRepository()
        {
            _serializer = new Serializer<RenovationRecommendation>();
            _renovationRecommendations = _serializer.FromCSV(FilePath);
        }

        private int NextId()
        {
            _renovationRecommendations = _serializer.FromCSV(FilePath);
            if (_renovationRecommendations.Count < 1)
            {
                return 1;
            }
            return _renovationRecommendations.Max(c => c.Id) + 1;
        }


        public void Delete(RenovationRecommendation selectedRenovationRecommendation)
        {
            _renovationRecommendations = _serializer.FromCSV(FilePath);
            foreach (var renovationRecommendation in _renovationRecommendations)
            {
                if (renovationRecommendation.Id == selectedRenovationRecommendation.Id)
                {
                    _renovationRecommendations.Remove(renovationRecommendation);
                    break;
                }
            }
            _serializer.ToCSV(FilePath, _renovationRecommendations);
        }

        public void Save(RenovationRecommendation recommendation)
        {
            recommendation.Id = NextId();
            _renovationRecommendations = _serializer.FromCSV(FilePath);
            _renovationRecommendations.Add(recommendation);
            _serializer.ToCSV(FilePath, _renovationRecommendations);
        }

        public List<RenovationRecommendation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<RenovationRecommendation> GetByReservationId(int reservationId)
        {
            _renovationRecommendations = _serializer.FromCSV(FilePath);
            return _renovationRecommendations.FindAll(r => r.Reservation.Id == reservationId);
        }
    }
}
