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
using InitialProject.View.OwnerView.Renovations;
using InitialProject.View.OwnerView.Reservations;

namespace InitialProject.ViewModels
{
    public class MyAccommodationsMainViewModel :  BaseViewModel
    {

        public User LogedInUser;
        public Navigator Navigator { get; set; }
        public MyAccommodationsMainViewModel(User logedInUser, Navigator navigator)
        {
            LogedInUser = logedInUser;
            Navigator = navigator;
        }


        public ICommand OpenRegistrationFormCommand => new RelayCommand(OpenRegistrationForm);

        private void OpenRegistrationForm()
        {
            Navigator.NavigateTo(new AccommodationRegistrationForm(LogedInUser, Navigator));
        }

        public ICommand OpenAccommodationListCommand => new RelayCommand(OpenAccommodationList);

        private void OpenAccommodationList()
        {
            Navigator.NavigateTo(new MyAccommodationsListView(LogedInUser, Navigator));

        }

        public ICommand OpenAccommodationStatisticsCommand => new RelayCommand(OpenAccommodationStatistics);

        private void OpenAccommodationStatistics()
        {
            Navigator.NavigateTo(new MyAccommodationStatisticView(LogedInUser, Navigator));
        }


    }
}
