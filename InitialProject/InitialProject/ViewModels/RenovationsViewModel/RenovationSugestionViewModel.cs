using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Service.RenovationServices;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Renovations;

namespace InitialProject.ViewModels.RenovationsViewModel
{
    public class RenovationSugestionViewModel : BaseViewModel
    {

        private readonly User _logedInUser;
        private readonly RenovationRecommendationService _renovationRecommendationService;
        public NavigationService NavigationService {get; set; }

        public ObservableCollection<RenovationRecommendation> Recommendations { get; set; }

        public RenovationSugestionViewModel(User logedInUser, NavigationService navigationService)
        {
            _logedInUser = logedInUser;
            NavigationService = navigationService;
            _renovationRecommendationService = new RenovationRecommendationService();
            Recommendations =
                new ObservableCollection<RenovationRecommendation>(
                    _renovationRecommendationService.GetByOwnerId(_logedInUser.Id));
        }


        public ICommand ScheduleRenovationCommand => new RelayCommandWithParams(ScheduleRenovation);

        private void ScheduleRenovation(object parameter)
        {
            if (parameter is RenovationRecommendation selectedRecommendation)
            {
                NavigationService.Navigate(new ScheduleRenovationFormView(_logedInUser,
                    selectedRecommendation.Reservation.Accommodation, NavigationService, selectedRecommendation.Recommendation));
            }
        }

        public ICommand DeleteSugestionCommand => new RelayCommandWithParams(DeleteSugestion);

        private void DeleteSugestion(object parameter)
        {
            if (parameter is RenovationRecommendation selectedRecommendation)
            {
                Recommendations.Remove(selectedRecommendation);
                _renovationRecommendationService.Delete(selectedRecommendation);
            }
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            NavigationService.Navigate(new RenovationsMainView(_logedInUser, NavigationService));
        }


    }
}
