using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using InitialProject.View;
using InitialProject.View.OwnerView;

namespace InitialProject.ViewModels
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

        public void ApproveReschedule(AccommodationReservationRescheduleRequest rescheduleRequest)
        {
            _accommodationReservationRescheduleRequestService.ApproveReschedule(rescheduleRequest);
            RescheduleRequests.Remove(rescheduleRequest);
        }

        public void DeclineReschedule(AccommodationReservationRescheduleRequest rescheduleRequest)
        {
            RescheduleDeclineWindow rescheduleDeclineWindow = new RescheduleDeclineWindow(rescheduleRequest);
            rescheduleDeclineWindow.Show();
            RescheduleRequests.Remove(rescheduleRequest);
        }

        public void GoBack()
        {
            OwnerMainWindow ownerMainWindpWindow = new OwnerMainWindow(_loggedInUser);
            CloseAction();
            ownerMainWindpWindow.Show();
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
