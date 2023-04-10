using InitialProject.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.View.OwnerView.MyAccommodations;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.View.OwnerView.Reservations;

namespace InitialProject.ViewModels
{
    public class MyAccommodationsMainViewModel :  SideScreenViewModel
    {

        public User LogedInUser;
        public MyAccommodationsMainViewModel(User logedInUser)
        {
            LogedInUser = logedInUser;
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

        public ICommand OpenRegistrationFormCommand => new RelayCommand(OpenRegistrationForm);

        private void OpenRegistrationForm()
        {
            AccommodationRegistrationForm accommodationRegistrationForm = new AccommodationRegistrationForm(LogedInUser);
            CloseCurrentWindow();
            accommodationRegistrationForm.Show();

        }

        public ICommand OpenAccommodationListCommand => new RelayCommand(OpenAccommodationList);

        private void OpenAccommodationList()
        {
            MyAccommodationsList myAccommodationsList = new MyAccommodationsList(LogedInUser);
            CloseCurrentWindow();
            myAccommodationsList.Show();

        }



        protected override void MyAccommoadionsOpen()
        {
            MyAccommodationsMainWindow myAccommodationsMainWindow = new MyAccommodationsMainWindow(LogedInUser);
            CloseCurrentWindow();
            myAccommodationsMainWindow.Show();

        }


        protected override void RatingsOpen()
        {
            RatingsMainWindow ratingsMain = new RatingsMainWindow(LogedInUser);
            CloseCurrentWindow();
            ratingsMain.Show();
        }


        protected override void ReservationsOpen()
        {
            ReservationsMainWindow reservationsMain = new ReservationsMainWindow(LogedInUser);
            CloseCurrentWindow();
            reservationsMain.Show();
        }


    }
}
