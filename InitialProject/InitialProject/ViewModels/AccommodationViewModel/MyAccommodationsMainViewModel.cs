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
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    public class MyAccommodationsMainViewModel :  BaseViewModel
    {

        public User LogedInUser;
        public NavigationService NavigationService { get; set; }
        public MyAccommodationsMainViewModel(User logedInUser, NavigationService navigationService)
        {
            LogedInUser = logedInUser;
            NavigationService = navigationService;
        }


        public ICommand OpenRegistrationFormCommand => new RelayCommand(OpenRegistrationForm);

        private void OpenRegistrationForm()
        {
            NavigationService.Navigate(new AccommodationRegistrationForm(LogedInUser, NavigationService));
        }

        public ICommand OpenAccommodationListCommand => new RelayCommand(OpenAccommodationList);

        private void OpenAccommodationList()
        {
            NavigationService.Navigate(new MyAccommodationsListView(LogedInUser, NavigationService));

        }

        public ICommand OpenAccommodationStatisticsCommand => new RelayCommand(OpenAccommodationStatistics);

        private void OpenAccommodationStatistics()
        {
            NavigationService.Navigate(new MyAccommodationStatisticView(LogedInUser, NavigationService));
        }

        public ICommand OpenRegistrationSuggestionCommand => new RelayCommand(OpenRegistrationSuggestion);

        private void OpenRegistrationSuggestion()
        {
            NavigationService.Navigate(new NewAccommodationSuggestionsView(LogedInUser, NavigationService));
        }


    }
}
