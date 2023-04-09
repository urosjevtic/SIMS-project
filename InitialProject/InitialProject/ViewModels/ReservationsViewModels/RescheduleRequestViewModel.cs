using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using InitialProject.Utilities;
using InitialProject.View;
using InitialProject.View.OwnerView;

namespace InitialProject.ViewModels.ReservationsViewModels
{
    public class RescheduleRequestViewModel : INotifyPropertyChanged
    {
        private readonly AccommodationReservationRescheduleRequestService _accommodationReservationRescheduleRequestService;
        private readonly DeclinedAccommodationReservationRescheduleRequestService _declinedReservationRescheduleRequestService;
        private readonly User _loggedInUser;

        public ObservableCollection<AccommodationReservationRescheduleRequest> RescheduleRequests { get; }

        public RescheduleRequestViewModel(User loggedInUser)
        {
            _accommodationReservationRescheduleRequestService = new AccommodationReservationRescheduleRequestService();
            _loggedInUser = loggedInUser;

            RescheduleRequests = new ObservableCollection<AccommodationReservationRescheduleRequest>(_accommodationReservationRescheduleRequestService.GetAllByOwnerId(_loggedInUser.Id));
        }


        public ICommand ApproveRescheduleCommand => new RelayCommandWithParams(ApproveReschedule);

        private void ApproveReschedule(object parameter)
        {
            if (parameter is AccommodationReservationRescheduleRequest selectedRescheduleRequest)
            {
                _accommodationReservationRescheduleRequestService.ApproveReschedule(selectedRescheduleRequest);
                RescheduleRequests.Remove(selectedRescheduleRequest);

            }
        }

        public ICommand DeclineRescheduleCommand => new RelayCommandWithParams(DeclineReschedule);

        private void DeclineReschedule(object parameter)
        {
            if (parameter is AccommodationReservationRescheduleRequest selectedRescheduleRequest)
            {
                RescheduleDeclineWindow rescheduleDeclineWindow = new RescheduleDeclineWindow(selectedRescheduleRequest);
                rescheduleDeclineWindow.Show();
                RescheduleRequests.Remove(selectedRescheduleRequest);

            }
        }



        public delegate void CloseWindowAction();
        public CloseWindowAction CloseAction { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
