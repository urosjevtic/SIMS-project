using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.Service.AccommodationServices;

namespace InitialProject.ViewModels.AccommodationViewModel
{
    public class NewAccommodationSuggestionsViewModel : BaseViewModel
    {
        public ObservableCollection<Location> SuggestedLocations { get; set; }
        private readonly LocationService _locationService;
        private readonly AccommodationSuggestionService _accommodationSuggestionService;
        public NewAccommodationSuggestionsViewModel()
        {

            _locationService = new LocationService();
            _accommodationSuggestionService = new AccommodationSuggestionService();
            SuggestedLocations = new ObservableCollection<Location>(_locationService.GetLocations());
        }

    }
}
