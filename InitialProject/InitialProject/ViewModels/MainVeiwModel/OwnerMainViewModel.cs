﻿using InitialProject.Model;
using InitialProject.Utilities;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.View.OwnerView.MyAccommodations;
using System.Linq;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.View.OwnerView.Reservations;
using InitialProject.ViewModels;

namespace InitialProject.ViewModel
{
    public class OwnerMainViewModel : BaseViewModel
    {
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

        public string WelcomeMessage => "Welcome";

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


        public OwnerMainViewModel(User user)
        {
            LogedInUser = user;
        }

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

        public ICommand MyAccommoadionsOpenCommand => new RelayCommand(MyAccommoadionsOpen);

        private void MyAccommoadionsOpen()
        {
            MyAccommodationsMainWindow myAccommodationsMainWindow = new MyAccommodationsMainWindow(LogedInUser);
            CloseCurrentWindow();
            myAccommodationsMainWindow.Show();
           
        }

        public ICommand RatingsOpenCommand => new RelayCommand(RatingsOpen);

        private void RatingsOpen()
        {
            RatingsMainWindow ratingsMain = new RatingsMainWindow(LogedInUser);
            CloseCurrentWindow();
            ratingsMain.Show();
        }

        public ICommand ReservationsOpenCommand => new RelayCommand(ReservationsOpen);

        private void ReservationsOpen()
        {
            ReservationsMainWindow reservationsMain = new ReservationsMainWindow(LogedInUser);
            CloseCurrentWindow();
            reservationsMain.Show();
        }




    }
}