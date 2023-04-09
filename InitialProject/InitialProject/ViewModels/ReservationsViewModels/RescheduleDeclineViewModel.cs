using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using InitialProject.Utilities;
using static InitialProject.ViewModels.ReservationsViewModels.RescheduleRequestViewModel;

namespace InitialProject.ViewModels
{
    public class RescheduleDeclineViewModel : INotifyPropertyChanged
    {
        private readonly DeclinedAccommodationReservationRescheduleRequestService _declinedReservationRescheduleRequestService;
        private readonly AccommodationReservationRescheduleRequestService _reservationRescheduleRequestService;
        private readonly AccommodationReservationRescheduleRequest _rescheduleRequest;
        public RescheduleDeclineViewModel(AccommodationReservationRescheduleRequest rescheduleRequest)
        {
            _declinedReservationRescheduleRequestService = new DeclinedAccommodationReservationRescheduleRequestService();
            _reservationRescheduleRequestService = new AccommodationReservationRescheduleRequestService();
            _rescheduleRequest = rescheduleRequest;
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

        public ICommand ConfirmDeclineCommand => new RelayCommand(ConfirmDecline);

        public void ConfirmDecline()
        {
            _reservationRescheduleRequestService.DeclineReschedule(_rescheduleRequest);
            DeclinedAccommodationReservationRescheduleRequest declinedRequest =
                new DeclinedAccommodationReservationRescheduleRequest();

            declinedRequest.RescheduleRequest = _rescheduleRequest;
            declinedRequest.ReasonForDeclining = _comment;
            _declinedReservationRescheduleRequestService.Save(declinedRequest);
            CloseCurrentWindow();

        }

        private void CloseCurrentWindow()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
