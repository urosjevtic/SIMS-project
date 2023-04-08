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

namespace InitialProject.ViewModels
{
    public class MyAccommodationsMainViewModel : INotifyPropertyChanged
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
            accommodationRegistrationForm.Show();

        }

        public ICommand OpenAccommodationListCommand => new RelayCommand(OpenAccommodationList);

        private void OpenAccommodationList()
        {
            MyAccommodationsList myAccommodationsList = new MyAccommodationsList(LogedInUser);
            myAccommodationsList.Show();

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
