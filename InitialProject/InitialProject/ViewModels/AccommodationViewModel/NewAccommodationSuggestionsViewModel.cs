using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Service;
using InitialProject.Service.AccommodationServices;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.MyAccommodations;

namespace InitialProject.ViewModels.AccommodationViewModel
{
    public class NewAccommodationSuggestionsViewModel : BaseViewModel
    {
        public LocationStatistic SuggestedLocation { get; set; }
        public NavigationService NavigationService { get; set; }
        public Accommodation AccommodationForClosing {get; set; }
        private readonly LocationService _locationService;
        private readonly AccommodationSuggestionService _accommodationSuggestionService;
        private readonly User _logedInUser;
        public NewAccommodationSuggestionsViewModel(User logedInUser, NavigationService navigationService)
        {
            _logedInUser = logedInUser;
            _locationService = new LocationService();
            _accommodationSuggestionService = new AccommodationSuggestionService();
            NavigationService = navigationService;
            SuggestedLocation = _accommodationSuggestionService.ReccommmendPlaceForNewAccommdoation();
            AccommodationForClosing =
                _accommodationSuggestionService.ReccommendedAccommodationForClosing(_logedInUser.Id);
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            NavigationService.Navigate(new MyAccommodationsMainView(_logedInUser, NavigationService));
        }

        public ICommand RegistrateNewAccommdoationCommand => new RelayCommand(RegistrateNewAccommdoation);
        private void RegistrateNewAccommdoation()
        {
            NavigationService.Navigate(new AccommodationRegistrationForm(_logedInUser, NavigationService, SuggestedLocation.Location.Country, SuggestedLocation.Location.City));
        }



    }
}
