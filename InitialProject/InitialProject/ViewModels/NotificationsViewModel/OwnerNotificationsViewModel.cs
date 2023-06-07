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
using InitialProject.Service.ForumServices;
using InitialProject.Service.RenovationServices;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.View.OwnerView.Renovations;

namespace InitialProject.ViewModels.NotificationsViewModel
{
    public class OwnerNotificationsViewModel : BaseViewModel
    {
        private readonly User _logedInUser;
        private readonly OwnerNotificationService _ownerNotificationService;
        private readonly AccommodationReservationRescheduleRequestService _rescheduleService;
        private readonly UnratedGuestService _unratedGuestService;
        private readonly RenovationRecommendationService _renovationRecommendationService;
        private readonly ForumService _forumService;
        public NavigationService NavigationService { get; set; }
        public OwnerNotificationsViewModel(User logedInUser, NavigationService navigationService)
        {
            _logedInUser = logedInUser;
            _ownerNotificationService = new OwnerNotificationService();
            _rescheduleService = new AccommodationReservationRescheduleRequestService();
            _unratedGuestService = new UnratedGuestService();
            _renovationRecommendationService = new RenovationRecommendationService();
            _forumService = new ForumService();
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
                _renovationRecommendationCount = _renovationRecommendationService.GetByOwnerId(_logedInUser.Id).Count;
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
                return _forumNotificationCount = _forumService.GetByOwnerId(_logedInUser.Id).Count;
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

        public ICommand OpenRenovationSugestionsCommand => new RelayCommand(OpenRenovationSugestions);

        private void OpenRenovationSugestions()
        {
            NavigationService.Navigate(new RenovationSugestionView(_logedInUser, NavigationService));
        }

        public ICommand OpenUnratedGuestsCommand => new RelayCommand(OpenUnratedGuests);

        private void OpenUnratedGuests()
        {
            NavigationService.Navigate(new UnratedGuestsListView(_logedInUser, NavigationService));
        }
    }
}
