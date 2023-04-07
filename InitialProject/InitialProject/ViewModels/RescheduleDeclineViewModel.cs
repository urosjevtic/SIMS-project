using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using static InitialProject.ViewModels.RescheduleRequestViewModel;

namespace InitialProject.ViewModels
{
    public class RescheduleDeclineViewModel : INotifyPropertyChanged
    {
        private readonly DeclinedAccommodationReservationRescheduleRequestService _declinedReservationRescheduleRequestService;
        private readonly AccommodationReservationRescheduleRequestService _reservationRescheduleRequestService;
        private AccommodationReservationRescheduleRequest RescheduleRequest;
        public RescheduleDeclineViewModel(AccommodationReservationRescheduleRequest rescheduleRequest)
        {
            _declinedReservationRescheduleRequestService = new DeclinedAccommodationReservationRescheduleRequestService();
            _reservationRescheduleRequestService = new AccommodationReservationRescheduleRequestService();
            RescheduleRequest = rescheduleRequest;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        } 

        public void ConfirmReschedule()
        {
            _reservationRescheduleRequestService.DeclineReschedule(RescheduleRequest);
            DeclinedAccommodationReservationRescheduleRequest declinedRequest =
                new DeclinedAccommodationReservationRescheduleRequest();

            declinedRequest.RescheduleRequest = RescheduleRequest;
            declinedRequest.ReasonForDeclining = _comment;
            _declinedReservationRescheduleRequestService.Save(declinedRequest);

            CloseAction();
        }

        public CloseWindowAction CloseAction { get; set; }
    }
}
