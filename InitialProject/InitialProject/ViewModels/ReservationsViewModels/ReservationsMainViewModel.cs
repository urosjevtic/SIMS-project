using InitialProject.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using InitialProject.Domain.Model;
using InitialProject.View;
using InitialProject.View.OwnerView.Reservations;

namespace InitialProject.ViewModels.ReservationsViewModels
{
    public class ReservationsMainViewModel : INotifyPropertyChanged
    {
        private readonly User _logedInUser;

        public ReservationsMainViewModel(User logedInUser)
        {
            _logedInUser = logedInUser;
        }


        private Visibility _sideScreenVisibility = Visibility.Collapsed;

        public Visibility SideScreenVisibility
        {
            get { return _sideScreenVisibility; }
            set
            {
                _sideScreenVisibility = value;
                OnPropertyChanged(nameof(SideScreenVisibility));
            }
        }

        private Visibility _mainScreenVisibility = Visibility.Visible;

        public Visibility MainScreenVisibility
        {
            get { return _mainScreenVisibility; }
            set
            {
                _mainScreenVisibility = value;
                OnPropertyChanged(nameof(MainScreenVisibility));
            }
        }

        public ICommand BurgerBarClosedCommand => new RelayCommand(BurgerBarClosed);
        public ICommand BurgerBarOpenCommand => new RelayCommand(BurgerBarOpen);

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

        public ICommand ShowAllReservationsCommand => new RelayCommand(ShowAllReservations);

        private void ShowAllReservations()
        {
            ReservationListWindow reservationListWindow = new ReservationListWindow(_logedInUser);
            reservationListWindow.Show();
        }

        public ICommand HandeReschedulesCommand => new RelayCommand(HandleReschedules);

        private void HandleReschedules()
        {
            RescheduleRequestWindow rescheduleRequestWindow = new RescheduleRequestWindow(_logedInUser);
            rescheduleRequestWindow.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
