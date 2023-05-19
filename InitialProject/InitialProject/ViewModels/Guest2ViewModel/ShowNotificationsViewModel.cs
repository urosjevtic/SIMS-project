using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Model;
using InitialProject.Service;

namespace InitialProject.ViewModels.Guest2ViewModel
{
    public class ShowNotificationsViewModel : BaseViewModel
    {
        public ICommand OkCommand { get; set; }
        public ICommand OkCommandTemplate { get; set; }
        public ICommand YesCommand { get; set; }
        public ICommand NoCommand { get; set; }
        public List<Notification> ListNotifications { get; set; }
        public TourGuest Guest { get; set; }

        private ObservableCollection<TourNotification> _tourNotifications;
        public ObservableCollection<TourNotification> TourNotifications
        {
            get { return _tourNotifications; }
            set
            {
                if (_tourNotifications != value)
                {
                    _tourNotifications = value;
                    OnPropertyChanged(nameof(TourNotifications));
                }
            }
        }
        private ObservableCollection<TourNotification> _starts;
        public ObservableCollection<TourNotification> Starts
        {
            get { return _starts; }
            set
            {
                if (_starts != value)
                {
                    _starts = value;
                    OnPropertyChanged(nameof(Starts));
                }
            }
        }

        public NotificationService _notificationService;
        public GuestsCheckPointService _guestsCheckPointService;
        public CheckPointService _checkPointService;
        public TourGuestService _tourGuestService;

        public User LoggedUser { get; set; } = App.LoggedUser;
        public MyDataTemplateSelector MyDataTemplateSelector { get; set; }
        public NavigationService navService { get; }
        public ShowNotificationsViewModel(NavigationService nav)
        {
            this.navService = nav;
            _notificationService = new NotificationService();
            _guestsCheckPointService = new GuestsCheckPointService();
            _checkPointService = new CheckPointService();
            _tourGuestService = new TourGuestService();
            MyDataTemplateSelector = new MyDataTemplateSelector();
            OkCommand = new RelayCommand(GoBack);
            OkCommandTemplate = new RelayCommand<TourNotification>(Ok);
            YesCommand = new RelayCommand<TourNotification>(Yes);
            NoCommand = new RelayCommand<TourNotification>(No);
            TourNotifications = new ObservableCollection<TourNotification>(_notificationService.GetToursForNotifications(LoggedUser.Id));
            Guest = _tourGuestService.GetById(LoggedUser.Id);
        }
        public void GoBack()
        {
            if (navService.CanGoBack)
            {
                navService.GoBack();
            }
        }
        public void Yes(TourNotification n)
        {
            TourNotifications.Remove(n);
            Guest.Presence = Model.UserPresence.Yes;
            _tourGuestService.Update(Guest);
            n.notification.IsGoing = true;
            n.notification.IsChecked = true;
            if (Guest.CheckPointName.Equals(""))
            {
                CheckPoint checkPoint = _checkPointService.GetById(n.notification.CheckPointId);
                Guest.CheckPointName = checkPoint.Name;
                _tourGuestService.Update(Guest);
                AddGuestCheckPoint(Guest, checkPoint);
            }
            _notificationService.Update(n.notification);
        }
        public void No(TourNotification n)
        {
            TourNotifications.Remove(n);
            Guest.Presence = Model.UserPresence.No;
            _tourGuestService.Update(Guest);
            n.notification.IsGoing = false;
            n.notification.IsChecked = true;
            _notificationService.Update(n.notification);
        }
        public void Ok(TourNotification n)
        {
            TourNotifications.Remove(n);
            _notificationService.Delete(n.notification);
        }
        private void AddGuestCheckPoint(Model.TourGuest guest, CheckPoint checkPoint)
        {
            GuestsCheckPoint guestCheckPoint = new GuestsCheckPoint();
            guestCheckPoint.CheckPoint = checkPoint;
            guestCheckPoint.Guest = guest;
            _guestsCheckPointService.Save(guestCheckPoint);
        }
    }
}
