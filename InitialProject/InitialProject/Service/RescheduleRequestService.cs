using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class RescheduleRequestService
    {
        private readonly IRescheduleRequestRepository _rescheduleRequestRepository;
        private readonly AccommodationReservationService _accommodationReservationService;

        public RescheduleRequestService()
        {
            _rescheduleRequestRepository = Injector.Injector.CreateInstance<IRescheduleRequestRepository>();
            _accommodationReservationService = new AccommodationReservationService();
        }

        public List<RescheduleRequest> GetAll()
        {
            
            List<RescheduleRequest> rescheduleRequests = _rescheduleRequestRepository.GetAll();
            BindReservationToRequest(rescheduleRequests);
            foreach (var rescheduleRequest in rescheduleRequests)
            {
                IsAlreadyOccupied(rescheduleRequest);
            }
            return rescheduleRequests;
        }

        public List<RescheduleRequest> GetAllByOwnerId(int id)
        {
            List<RescheduleRequest> rescheduleRequestsByOwnerId = new List<RescheduleRequest>();    
            List<RescheduleRequest> rescheduleRequests = GetAll();
            foreach (var rescheduleRequest in rescheduleRequests)
            {
                if(id == rescheduleRequest.Reservation.Accommodation.Owner.Id)
                    rescheduleRequestsByOwnerId.Add(rescheduleRequest);
            }
            return rescheduleRequestsByOwnerId;
        }

        private void BindReservationToRequest(List<RescheduleRequest> rescheduleRequests)
        {
            List<AccommodationReservation> accommodations = _accommodationReservationService.GetAll();
            foreach (RescheduleRequest rescheduleRequest in rescheduleRequests)
            {
                foreach (AccommodationReservation accommodationReservation in accommodations)
                {
                    if (accommodationReservation.Id == rescheduleRequest.Reservation.Id)
                    {
                        rescheduleRequest.Reservation = accommodationReservation;
                        break;
                    }
                }
            }

        }

        private void IsAlreadyOccupied(RescheduleRequest request)
        {
            List<AccommodationReservation> accommodationReservations = _accommodationReservationService.GetReservationsByAccommodationId(request.Reservation.AccommodationId);
            foreach (AccommodationReservation accommodationReservation in accommodationReservations)
            {
                if (accommodationReservation.ReservedDates.Contains(request.RescheduleStartDate) ||
                    accommodationReservation.ReservedDates.Contains(request.RescheduleEndDate))
                {
                    request.IsAlreadyReserved = true;
                    break;
                }
            }
            request.IsAlreadyReserved = false;
        }

        public void Delete(RescheduleRequest request)
        {
            _rescheduleRequestRepository.Delete(request);
        }

        public void ApproveReschedule(RescheduleRequest rescheduleRequest)
        {
            int reservationId = rescheduleRequest.Reservation.Id;
            DateTime newStarDate = rescheduleRequest.RescheduleStartDate;
            DateTime newEndDate = rescheduleRequest.RescheduleEndDate;
            int userId = rescheduleRequest.Reservation.UserId;
            int accommodationId = rescheduleRequest.Reservation.AccommodationId;
            int guestNumber = rescheduleRequest.Reservation.GuestNumber;

            AccommodationReservation reservation = _accommodationReservationService.Create(reservationId, newStarDate,
                newEndDate, userId, accommodationId, guestNumber);
            _accommodationReservationService.Delete(rescheduleRequest.Reservation);
            _accommodationReservationService.Save(reservation);
            Delete(rescheduleRequest);
        }
    }
}
