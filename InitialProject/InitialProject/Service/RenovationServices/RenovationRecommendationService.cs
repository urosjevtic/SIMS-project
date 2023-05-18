using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces.IAccommodationRenovationsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service.ReservationServices;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository.AccommodationRenovationRepo;
using InitialProject.Serializer;
using InitialProject.View;

namespace InitialProject.Service.RenovationServices
{
    public class RenovationRecommendationService
    {
        private readonly IRenovationRecommendationRepository _renovationRecommendationRepository;
        private readonly AccommodationReservationService _accommodationReservationService;


        public RenovationRecommendationService()
        {
            _renovationRecommendationRepository = Injector.Injector.CreateInstance<IRenovationRecommendationRepository>();
            _accommodationReservationService = new AccommodationReservationService();
        }


        public List<RenovationRecommendation> GetAll()
        {
            return _renovationRecommendationRepository.GetAll();
        }

        public List<RenovationRecommendation> GetByReservationId(int reservationId)
        {
            return _renovationRecommendationRepository.GetByReservationId(reservationId);
        }

        public List<RenovationRecommendation> GetByGuestId(int guestId)
        {
            List<RenovationRecommendation> allRecommendations = _renovationRecommendationRepository.GetAll();
            List<RenovationRecommendation> recommendationByGuestId = new List<RenovationRecommendation>();

            BindReservationToRecommendation(allRecommendations);

            foreach (var recommendation in allRecommendations)
            {
                if (recommendation.Reservation.User.Id == guestId)
                {
                    recommendationByGuestId.Add(recommendation);
                }
            }

            return recommendationByGuestId;
        }

//<<<<<<< HEAD

//        private void BindReservationToRecommendation(List<RenovationRecommendation> recommendations, int guestId)
//        {
//            List<AccommodationReservation> reservations = _accommodationReservationService.GetAllReservationByGuestId(guestId);
//=======
        public List<RenovationRecommendation> GetByOwnerId(int ownerId)
        {
            List<RenovationRecommendation> allRecommendations = _renovationRecommendationRepository.GetAll();
            List<RenovationRecommendation> recommendationByOwnerId = new List<RenovationRecommendation>();
            BindReservationToRecommendation(allRecommendations);
            foreach (var recommendation in allRecommendations)
            {
                if (recommendation.Reservation.Accommodation.Owner.Id == ownerId)
                {
                    recommendationByOwnerId.Add(recommendation);
                }
            }

            return recommendationByOwnerId;
        }

        private void BindReservationToRecommendation(List<RenovationRecommendation> recommendations)
        {
            List<AccommodationReservation> reservations = _accommodationReservationService.GetAll();


            foreach (var recommendation in recommendations)
            {
                AccommodationReservation reservation = reservations.FirstOrDefault(a => a.Id == recommendation.Reservation.Id);

                if (reservation != null)
                {
                    recommendation.Reservation = reservation;
                }
            }
        }

        public void Save(RenovationRecommendation recommendation)
        {
            _renovationRecommendationRepository.Save(recommendation);
        }

        public RenovationRecommendation CreateRenovation(AccommodationReservation reservation, int urgencyLevel, string recommendation)
        {
            return new RenovationRecommendation
            {
                Reservation = reservation,
                UrgencyLevel = urgencyLevel,
                Recommendation = recommendation
            };
        }


        public void Delete(RenovationRecommendation recommendation)
        {
            _renovationRecommendationRepository.Delete(recommendation);
        }

    }

}

