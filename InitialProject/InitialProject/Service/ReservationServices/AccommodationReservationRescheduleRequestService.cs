using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces.IReservationsRepo;
using InitialProject.Repository;

namespace InitialProject.Service.ReservationServices
{
    public class AccommodationReservationRescheduleRequestService
    {
        private readonly IAccommodationReservationRescheduleRequestRepository _accommodationReservationRescheduleRequestRepository;
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly DeclinedAccommodationReservationRescheduleRequestService _declinedReservationRescheduleRequestService;


        public AccommodationReservationRescheduleRequestService()
        {
            _accommodationReservationRescheduleRequestRepository = Injector.Injector.CreateInstance<IAccommodationReservationRescheduleRequestRepository>();
            _accommodationReservationService = new AccommodationReservationService();
            _declinedReservationRescheduleRequestService =
                new DeclinedAccommodationReservationRescheduleRequestService();
        }

        public List<AccommodationReservationRescheduleRequest> GetAll()
        {

            List<AccommodationReservationRescheduleRequest> rescheduleRequests = _accommodationReservationRescheduleRequestRepository.GetAll();
            BindReservationToRequest(rescheduleRequests);
            foreach (var rescheduleRequest in rescheduleRequests)
            {
                IsAlreadyOccupied(rescheduleRequest);
            }
            return rescheduleRequests;
        }

        public List<AccommodationReservationRescheduleRequest> GetAllByOwnerId(int id)
        {
            List<AccommodationReservationRescheduleRequest> rescheduleRequestsByOwnerId = new List<AccommodationReservationRescheduleRequest>();
            List<AccommodationReservationRescheduleRequest> rescheduleRequests = GetAll();
            foreach (var rescheduleRequest in rescheduleRequests)
            {
                if (id == rescheduleRequest.Reservation.Accommodation.Owner.Id && rescheduleRequest.Status == RescheduleStatus.pending)
                    rescheduleRequestsByOwnerId.Add(rescheduleRequest);
            }
            return rescheduleRequestsByOwnerId;
        }

        private void BindReservationToRequest(List<AccommodationReservationRescheduleRequest> rescheduleRequests)
        {
            List<AccommodationReservation> accommodations = _accommodationReservationService.GetAll();
            foreach (AccommodationReservationRescheduleRequest rescheduleRequest in rescheduleRequests)
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

        private void IsAlreadyOccupied(AccommodationReservationRescheduleRequest request)
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

        public void Delete(AccommodationReservationRescheduleRequest request)
        {
            _accommodationReservationRescheduleRequestRepository.Delete(request);
        }

        public void ApproveReschedule(AccommodationReservationRescheduleRequest rescheduleRequest)
        {
            int reservationId = rescheduleRequest.Reservation.Id;
            DateTime newStarDate = rescheduleRequest.RescheduleStartDate;
            DateTime newEndDate = rescheduleRequest.RescheduleEndDate;
            int userId = rescheduleRequest.Reservation.UserId;
            int accommodationId = rescheduleRequest.Reservation.AccommodationId;
            int guestNumber = rescheduleRequest.Reservation.GuestNumber;

            AccommodationReservation reservation = _accommodationReservationService.Create(reservationId, newStarDate,
                newEndDate, userId, accommodationId, guestNumber);
            _accommodationReservationService.Update(reservation);

            ChangeStatus(rescheduleRequest, RescheduleStatus.approved);
        }

        public void DeclineReschedule(AccommodationReservationRescheduleRequest rescheduleRequest)
        {

            ChangeStatus(rescheduleRequest, RescheduleStatus.declined);

        }
        private void ChangeStatus(AccommodationReservationRescheduleRequest request, RescheduleStatus status)
        {
            request.Status = status;
            _accommodationReservationRescheduleRequestRepository.Update(request);
        }
    }
}
