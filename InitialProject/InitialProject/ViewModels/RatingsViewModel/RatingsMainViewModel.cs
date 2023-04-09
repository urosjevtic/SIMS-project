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

namespace InitialProject.ViewModels.RatingsViewModel
{
    public class RatingsMainViewModel : INotifyPropertyChanged
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


        private void CloseCurrentWindow()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
