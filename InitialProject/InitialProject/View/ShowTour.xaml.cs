using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InitialProject.Domain.Model;
using InitialProject.Repository;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ShowTour.xaml
    /// </summary>
    public partial class ShowTour : Window
    {
        public User LoggedUser { get; set; }
        private readonly NotificationRepository _notificationRepository;
        private readonly UserRepository _userRepository;
        private readonly TourGuestRepository _tourGuestsRepository;
        private readonly CheckPointRepository _checkPointRepository;
        private readonly GuestsCheckPointRepository _guestsCheckPointRepository;

        public List<Notification> Notifications { get; set; }
        public Model.TourGuest Guest { get; set; }
        
        public ShowTour(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            LoggedUser = user;
            
            _notificationRepository = new NotificationRepository();
            _tourGuestsRepository = new TourGuestRepository();
            _checkPointRepository = new CheckPointRepository();
            _guestsCheckPointRepository = new GuestsCheckPointRepository(); 

            _userRepository = new UserRepository();
            Guest = _tourGuestsRepository.GetById(LoggedUser.Id);
            Notifications = new List<Notification>(_notificationRepository.GetAll());
        }
        private void OpenSearchButtonClick(object sender, RoutedEventArgs e)
        {
            TourSearch tourSearch = new TourSearch(LoggedUser);
            tourSearch.Show();
        }

        private void OpenVouchersButtonClick(object sender, RoutedEventArgs e)
        {
            ShowVouchers showVouchers = new ShowVouchers();
            showVouchers.Show();
        }

        private void OpenMyToursButtonClick(object sender, RoutedEventArgs e)
        {
            MyTours myTours = new MyTours(LoggedUser);
            myTours.Show();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                GetNotification();
            }));
        }
        private void GetNotification()
        {
            foreach (Notification notification in Notifications)
            {
                if (notification.GuestId == LoggedUser.Id)
                {
                    MessageBoxResult result = MessageBox.Show("Da li ste prisutni na turi?", "Potvrda prisustva", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Guest.Presence = Model.UserPresence.Yes;
                        _tourGuestsRepository.Update(Guest);
                        notification.IsGoing = true;
                        if (Guest.CheckPointName.Equals(""))
                        {
                            CheckPoint checkPoint = _checkPointRepository.GetById(notification.CheckPointId);
                            Guest.CheckPointName = checkPoint.Name;
                            _tourGuestsRepository.Update(Guest);
                            AddGuestCheckPoint(Guest, checkPoint);
                        }
                        _notificationRepository.Update(notification);
                        _notificationRepository.Delete(notification);
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        Guest.Presence = Model.UserPresence.No;
                        _tourGuestsRepository.Update(Guest);
                        notification.IsGoing = false;
                        _notificationRepository.Update(notification);
                        _notificationRepository.Delete(notification);

                    }
                    else
                    {
                        Guest.Presence = Model.UserPresence.Unknown;
                        _tourGuestsRepository.Update(Guest);
                        notification.IsGoing = false;
                        _notificationRepository.Update(notification);
                        _notificationRepository.Delete(notification);

                    }

                }
            }
        }

        private void AddGuestCheckPoint(Model.TourGuest guest, CheckPoint checkPoint)
        {
            GuestsCheckPoint guestCheckPoint = new GuestsCheckPoint();
            guestCheckPoint.CheckPoint = checkPoint;
            guestCheckPoint.Guest = guest;
            _guestsCheckPointRepository.Save(guestCheckPoint);
        }
    }
}
