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


        public AccommodationReservationRescheduleRequestService()
        {
            _accommodationReservationRescheduleRequestRepository = Injector.Injector.CreateInstance<IAccommodationReservationRescheduleRequestRepository>();
            _accommodationReservationService = new AccommodationReservationService();
        }

        public List<AccommodationReservationRescheduleRequest> GetAll()
        {

            List<AccommodationReservationRescheduleRequest> rescheduleRequests = _accommodationReservationRescheduleRequestRepository.GetAll();
            BindReservationToRequest(rescheduleRequests);
            foreach (var rescheduleRequest in rescheduleRequests)
            {
                CheckIfAlreadyReserved(rescheduleRequest);
            }
            return rescheduleRequests;
        }

        private void BindReservationToRequest(List<AccommodationReservationRescheduleRequest> rescheduleRequests)
        {
            Dictionary<int, AccommodationReservation> reservationsById = _accommodationReservationService.GetAll()
                .ToDictionary(reservation => reservation.Id);

            foreach (AccommodationReservationRescheduleRequest rescheduleRequest in rescheduleRequests)
            {
                if (reservationsById.TryGetValue(rescheduleRequest.Reservation.Id, out AccommodationReservation accommodationReservation))
                {
                    rescheduleRequest.Reservation = accommodationReservation;
                }
            }

        }


        private void CheckIfAlreadyReserved(AccommodationReservationRescheduleRequest request)
        {
            List<AccommodationReservation> accommodationReservations = _accommodationReservationService.GetReservationsByAccommodationId(request.Reservation.AccommodationId);
            request.IsAlreadyReserved = accommodationReservations.Any(accommodationReservation =>
                accommodationReservation.ReservedDates.Contains(request.RescheduleStartDate) ||
                accommodationReservation.ReservedDates.Contains(request.RescheduleEndDate));
        }

        public List<AccommodationReservationRescheduleRequest> GetAllByOwnerId(int id)
        {
            return GetAll()
                .Where(r => r.Reservation.Accommodation.Owner.Id == id && r.Status == RescheduleStatus.pending)
                .ToList();
        }

        public void Delete(AccommodationReservationRescheduleRequest request)
        {
            _accommodationReservationRescheduleRequestRepository.Delete(request);
        }

        public void ApproveReschedule(AccommodationReservationRescheduleRequest rescheduleRequest)
        {
            AccommodationReservation newReservation = CreateNewReservation(rescheduleRequest);

            _accommodationReservationService.Update(newReservation);

            ChangeStatus(rescheduleRequest, RescheduleStatus.approved);
        }

        private AccommodationReservation CreateNewReservation(AccommodationReservationRescheduleRequest rescheduleRequest)
        {
            int reservationId = rescheduleRequest.Reservation.Id;
            DateTime newStartDate = rescheduleRequest.RescheduleStartDate;
            DateTime newEndDate = rescheduleRequest.RescheduleEndDate;
            int userId = rescheduleRequest.Reservation.UserId;
            int accommodationId = rescheduleRequest.Reservation.AccommodationId;
            int guestNumber = rescheduleRequest.Reservation.GuestNumber;

            return _accommodationReservationService.Create(reservationId, newStartDate, newEndDate, userId, accommodationId, guestNumber);
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
