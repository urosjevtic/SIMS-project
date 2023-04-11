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
using System.Windows.Threading;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class ShowTourViewModel
    {
        public User LoggedUser { get; set; }
        private readonly NotificationRepository _notificationRepository;
        private readonly UserRepository _userRepository;
        private readonly TourGuestsRepository _tourGuestsRepository;
        private readonly CheckPointRepository _checkPointRepository;
        private readonly GuestsCheckPointRepository _guestsCheckPointRepository;

        public List<Notification> Notifications { get; set; }
        public Model.TourGuest Guest { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ShowMyToursCommand { get; set; }
        public ICommand ShowVouchersCommand { get; set; }

        public ShowTourViewModel(User user)
        {
            LoggedUser = user;
            Guest = _tourGuestsRepository.GetById(LoggedUser.Id);
            _notificationRepository = new NotificationRepository();
            _tourGuestsRepository = new TourGuestsRepository();
            _checkPointRepository = new CheckPointRepository();
            _guestsCheckPointRepository = new GuestsCheckPointRepository(); 

            _userRepository = new UserRepository();
            SearchCommand = new RelayCommand(Search);
            ShowMyToursCommand = new RelayCommand(ShowMyTours);
            ShowVouchersCommand = new RelayCommand(ShowVouchers);
            Guest = _tourGuestsRepository.GetById(LoggedUser.Id);
            Notifications = new List<Notification>(_notificationRepository.GetAll());
        }
        private void Search()
        {
            TourSearch tourSearch = new TourSearch(LoggedUser);
            tourSearch.Show();
        }
        private void ShowMyTours()
        {
            MyTours myTours = new MyTours(LoggedUser);
            myTours.Show();
        }
        private void ShowVouchers()
        {
            ShowVouchers showVouchers = new ShowVouchers();
            showVouchers.Show();
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
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        Guest.Presence = Model.UserPresence.No;
                        _tourGuestsRepository.Update(Guest);
                        notification.IsGoing = false;
                        _notificationRepository.Update(notification);
                    }
                    else
                    {
                        Guest.Presence = Model.UserPresence.Unknown;
                        _tourGuestsRepository.Update(Guest);
                        notification.IsGoing = false;
                        _notificationRepository.Update(notification);
                    }

                }
            }
        }

        private void AddGuestCheckPoint(Model.TourGuest guest, CheckPoint checkPoint)
        {
            GuestsCheckPoint guestCheckPoint = new GuestsCheckPoint();
            guestCheckPoint.CheckPointId = checkPoint.Id;
            guestCheckPoint.GuestsId = guest.Id;
            _guestsCheckPointRepository.Save(guestCheckPoint);
        }
    }
}
