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
using InitialProject.Domain.Model.Users;
using InitialProject.Service;
using InitialProject.Service.NotificationServices;
using InitialProject.Service.RenovationServices;
using InitialProject.Service.SettingsService;
using InitialProject.View.OwnerView.MainWindow;
using InitialProject.View.OwnerView.Notifications;
using InitialProject.View.OwnerView.Settings;
using System;
using InitialProject.Properties;
using InitialProject.View.OwnerView.Forums;
using InitialProject.View.OwnerView.Help;

namespace InitialProject.ViewModel
{
    public class OwnerMainViewModel : SideScreenViewModel
    {


        public NavigationService NavigationService { get; set; }

        private readonly OwnerNotificationService _notificationService;
        private readonly OwnerEventsService _eventsService;
        private readonly OwnerSettingsService _settingsService;

        public OwnerMainViewModel(User user)
        {
            LogedInUser = user;
            NavigationService = SelectedPage.NavigationService;
            SelectedPage.Content = new MainPageView(user, NavigationService);
            _notificationService = new OwnerNotificationService();
            _eventsService = new OwnerEventsService();
            _settingsService = new OwnerSettingsService();
            LoadSettings();
            CheckNotifications();
            CheckForEvents();

        }


        private void LoadSettings()
        {
            var app = (App)Application.Current;
            OwnerSettings setting = _settingsService.GetByOwnerId(_loggedInUser.Id);
            app.ChangeLanguage(setting.Language);
            LoadTheme(setting.Theme);
        }


        private void LoadTheme(string theme)
        {
            ResourceDictionary themeDictionary = new ResourceDictionary();
            if (theme.Equals("light"))
            {
                themeDictionary.Source = new Uri("../../Themes/LightTheme.xaml", UriKind.Relative);
            }
            else
            {
                themeDictionary.Source = new Uri("../../Themes/DarkTheme.xaml", UriKind.Relative);
            }
            App.Current.Resources.MergedDictionaries.Add(themeDictionary);
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

        protected override void NotesOpen()
        {
            NotesView notesView = new NotesView(_loggedInUser);
            notesView.ShowDialog();
        }

        protected override void SettingsOpen()
        {
            NavigationService.Navigate(new OwnerSettingsView(_loggedInUser, NavigationService));
            BurgerBarClosed();
        }

        protected override void ForumOpen()
        {
            NavigationService.Navigate(new ForumSelcetionView(_loggedInUser, NavigationService));
            BurgerBarClosed();
        }

        protected override void LogOut()
        {
            SignInForm signInForm = new SignInForm();
            CloseCurrentWindow();
            signInForm.Show();

        }

        protected override void Help()
        {
            NavigationService.Navigate(new OwnerHelpView(NavigationService));
            BurgerBarClosed();
        }

        public ICommand NotificationOpenCommand => new RelayCommand(NotificationOpen);

        protected override void NotificationOpen()
        {
            _notificationService.UpdateNewNotifications(_loggedInUser.Id);
            OnPropertyChanged("HasNewNotifications");
            NavigationService.Navigate(new OwnerNotificationsView(_loggedInUser, NavigationService));
            BurgerBarClosed();
        }



    }
}