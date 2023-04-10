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
using InitialProject.View.OwnerView.Ratings;
using Microsoft.VisualBasic.ApplicationServices;
using User = InitialProject.Domain.Model.User;
using InitialProject.View.OwnerView.MyAccommodations;
using InitialProject.View.OwnerView.Reservations;

namespace InitialProject.ViewModels.RatingsViewModel
{
    public class RatingsMainViewModel : BaseViewModel
    {
        private readonly User _logedInUser;
        public RatingsMainViewModel(User logedInUser)
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

        public ICommand OpenUnratedGuestsCommand => new RelayCommand(OpenUnratedGuests);

        private void OpenUnratedGuests()
        {
            UnratedGuestsList unratedGuests = new UnratedGuestsList(_logedInUser);
            CloseCurrentWindow();
            unratedGuests.Show();
        }


        public ICommand OpenOwnerRatingsCommand => new RelayCommand(OpenOwnerRatings);

        private void OpenOwnerRatings()
        {
            AccommodationReviewsSelectionWindow unratedGuests = new AccommodationReviewsSelectionWindow(_logedInUser);
            CloseCurrentWindow();
            unratedGuests.Show();
        }

        public ICommand MyAccommoadionsOpenCommand => new RelayCommand(MyAccommoadionsOpen);

        private void MyAccommoadionsOpen()
        {
            MyAccommodationsMainWindow myAccommodationsMainWindow = new MyAccommodationsMainWindow(_logedInUser);
            CloseCurrentWindow();
            myAccommodationsMainWindow.Show();

        }

        public ICommand RatingsOpenCommand => new RelayCommand(RatingsOpen);

        private void RatingsOpen()
        {
            RatingsMainWindow ratingsMain = new RatingsMainWindow(_logedInUser);
            CloseCurrentWindow();
            ratingsMain.Show();
        }

        public ICommand ReservationsOpenCommand => new RelayCommand(ReservationsOpen);

        private void ReservationsOpen()
        {
            ReservationsMainWindow reservationsMain = new ReservationsMainWindow(_logedInUser);
            CloseCurrentWindow();
            reservationsMain.Show();
        }


    }
}
