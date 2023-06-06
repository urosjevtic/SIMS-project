using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.View.OwnerView.Renovations;

namespace InitialProject.ViewModels.RenovationsViewModel
{
    public class ScheduleRenovationViewModel : BaseViewModel
    {
        private readonly AccommodationService _accommodationService;
        private readonly User _logedInUser;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public NavigationService NavigationService { get; set; }

        public ScheduleRenovationViewModel(User logedInUser, NavigationService navigationService)
        {
            _accommodationService = new AccommodationService();
            _logedInUser = logedInUser;
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodationByOwnerId(logedInUser.Id));
            NavigationService = navigationService;
        }



        public ICommand ScheduleRenovationCommand => new RelayCommandWithParams(ScheduleRenovation);

        private void ScheduleRenovation(object parameter)
        {
            if (parameter is Accommodation selectedAccommodation)
            {
                NavigationService.Navigate(new ScheduleRenovationFormView(_logedInUser, selectedAccommodation, NavigationService));

            }
        }
        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            NavigationService.Navigate(new RenovationsMainView(_logedInUser, NavigationService));
        }

        public ICommand OpenScheduleRenovationSugestionsCommand => new RelayCommand(OpenScheduleRenovationSugestions);
        private void OpenScheduleRenovationSugestions()
        {
            NavigationService.Navigate(new RenovationSugestionView(_logedInUser, NavigationService));
        }


    }
}
