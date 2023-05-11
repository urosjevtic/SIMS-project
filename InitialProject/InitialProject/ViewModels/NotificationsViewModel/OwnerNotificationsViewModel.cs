using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Service.NotificationServices;
using InitialProject.Service.ReservationServices;
using InitialProject.Utilities;
using InitialProject.View;
using FluentScheduler;
using InitialProject.Service;
using InitialProject.View.OwnerView.Ratings;

namespace InitialProject.ViewModels.NotificationsViewModel
{
    public class OwnerNotificationsViewModel : BaseViewModel
    {
        private readonly User _logedInUser;
        private readonly OwnerNotificationService _ownerNotificationService;
        private readonly AccommodationReservationRescheduleRequestService _rescheduleService;
        private readonly UnratedGuestService _unratedGuestService;
        public NavigationService NavigationService { get; set; }
        public OwnerNotificationsViewModel(User logedInUser, NavigationService navigationService)
        {
            _logedInUser = logedInUser;
            _ownerNotificationService = new OwnerNotificationService();
            _rescheduleService = new AccommodationReservationRescheduleRequestService();
            _unratedGuestService = new UnratedGuestService();
            NavigationService = navigationService;
        }

        private int _rescheduleRequestCount;

        public int RescheduleRequestCount
        {
            get
            {
                _rescheduleRequestCount =
                    _rescheduleService.GetAllByOwnerId(_logedInUser.Id).Count;
                return _rescheduleRequestCount;
            }
            set
            {
                _rescheduleRequestCount = value;
                OnPropertyChanged("RescheduleRequestCount");
            }
        }

        private int _renovationRecommendationCount;

        public int RenovationRecommendationCount
        {
            get
            {
                return _renovationRecommendationCount;
            }
            set
            {
                _renovationRecommendationCount = value;
                OnPropertyChanged("RenovationRecommendation");
            }
        }

        private int _forumNotificationCount;

        public int ForumNotificationCount
        {
            get
            {
                return _forumNotificationCount;
            }
            set
            {
                _forumNotificationCount = value;
                OnPropertyChanged("ForumNotificationCount");
            }
        }

        private int _unratedGuestsCount;

        public int UnratedGuestsCount
        {
            get
            {
                _unratedGuestsCount = _unratedGuestService.GetUnratedGuestsByOwnerId(_logedInUser.Id).Count;
                return _unratedGuestsCount;
            }
            set
            {
                _unratedGuestsCount = value;
                OnPropertyChanged("UnratedGuestsCount");
            }
        }


        public ICommand OpenRescheduleRequestsCommand => new RelayCommand(OpenRescheduleRequests);

        private void OpenRescheduleRequests()
        {
            NavigationService.Navigate(new RescheduleRequestView(_logedInUser, NavigationService));
        }

        public ICommand OpenUnratedGuestsCommand => new RelayCommand(OpenUnratedGuests);

        private void OpenUnratedGuests()
        {
            NavigationService.Navigate(new UnratedGuestsListView(_logedInUser, NavigationService));
        }
    }
}
