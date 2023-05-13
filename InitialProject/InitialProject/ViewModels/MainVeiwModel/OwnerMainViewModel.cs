using InitialProject.Model;
using InitialProject.Utilities;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.View.OwnerView.MyAccommodations;
using System.Linq;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;
using InitialProject.Repository.StatisticRepo;
using InitialProject.View.OwnerView.Notes;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.View.OwnerView.Reservations;
using InitialProject.ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using InitialProject.View.OwnerView.Renovations;
using System.Windows.Controls;
using System.Windows.Navigation;
using FluentScheduler;
using InitialProject.Service;
using InitialProject.Service.NotificationServices;
using InitialProject.Service.RenovationServices;
using InitialProject.View.OwnerView.MainWindow;
using InitialProject.View.OwnerView.Notifications;

namespace InitialProject.ViewModel
{
    public class OwnerMainViewModel : SideScreenViewModel
    {


        public NavigationService NavigationService { get; set; }

        private readonly OwnerNotificationService _notificationService;
        private readonly OwnerEventsService _eventsService;

        public OwnerMainViewModel(User user)
        {
            LogedInUser = user;
            NavigationService = SelectedPage.NavigationService;
            SelectedPage.Content = new MainPageView(user, NavigationService);
            _notificationService = new OwnerNotificationService();
            _eventsService = new OwnerEventsService();
            CheckNotifications();
            CheckForEvents();

        }


        private bool _hasNewNotifications;

        public bool HasNewNotifications
        {
            get { return _hasNewNotifications;}
            set
            {
                _hasNewNotifications = value;
                OnPropertyChanged("HasNewNotifications");
            }
        }

        private void CheckForEvents()
        {
            JobManager.Initialize();
            //Check for notifications
            JobManager.AddJob(
                () => CheckNotifications(),
                s => s.ToRunEvery(3).Seconds());
            //Check accommodation renovation status
            JobManager.AddJob(
                ()=> _eventsService.CheckAccommodationRenovationStatus(_loggedInUser.Id),
                s=>s.ToRunEvery(1).Days()
                );
        }

        private void CheckNotifications()
        {
            _hasNewNotifications = !_notificationService.HasNewNotifications(_loggedInUser.Id);
            OnPropertyChanged("HasNewNotifications");
        }


        private Frame _selectedPage = new Frame();
        public Frame SelectedPage
        {
            get => _selectedPage;
            set
            {
                _selectedPage = value;
                OnPropertyChanged("SelectedPage");
            }
        }


        private User _loggedInUser;

        public User LogedInUser
        {
            get { return _loggedInUser; }
            set
            {
                _loggedInUser = value;
                OnPropertyChanged(nameof(LogedInUser));
            }
        }

        public string WelcomeMessage => LogedInUser.Username;



        private void BurgerBarOpen()
        {
            SideScreenVisibility = Visibility.Visible;
            MainScreenVisibility = Visibility.Collapsed;
        }

        private void BurgerBarClosed()
        {
            SideScreenVisibility = Visibility.Collapsed;
            MainScreenVisibility = Visibility.Visible;
        }


        protected override void MyAccommoadionsOpen()
        {
            NavigationService.Navigate(new MyAccommodationsMainView(_loggedInUser, NavigationService));
            BurgerBarClosed();
        }


        protected override void RatingsOpen()
        {
            NavigationService.Navigate(new RatingsMainView(_loggedInUser, NavigationService));
            BurgerBarClosed();
        }


        protected override void ReservationsOpen()
        {
            NavigationService.Navigate(new ReservationsMainView(_loggedInUser, NavigationService));
            BurgerBarClosed();
        }

        protected override void RenovationsOpen()
        {
            NavigationService.Navigate(new RenovationsMainView(_loggedInUser, NavigationService));
            BurgerBarClosed();
        }

        public ICommand NotesOpenCommand => new RelayCommand(NotesOpen);

        private void NotesOpen()
        {
            NotesView notesView = new NotesView(_loggedInUser);
            notesView.ShowDialog();
        }

        public ICommand NotificationOpenCommand => new RelayCommand(NotificationOpen);

        private void NotificationOpen()
        {
            _notificationService.UpdateNewNotifications(_loggedInUser.Id);
            OnPropertyChanged("HasNewNotifications");
            NavigationService.Navigate(new OwnerNotificationsView(_loggedInUser, NavigationService));
        }


    }
}