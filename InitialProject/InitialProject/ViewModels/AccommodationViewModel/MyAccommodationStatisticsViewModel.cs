using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View.OwnerView.MyAccommodations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Ratings;
using System.Windows.Navigation;

namespace InitialProject.ViewModels.AccommodationViewModel
{
    public class MyAccommodationStatisticsViewModel : BaseViewModel
    {
        public ObservableCollection<Accommodation> Accommodations { get; }
        private readonly AccommodationService _accommodationService;
        private readonly User _logedInUser;
        public NavigationService NavigationService;

        public MyAccommodationStatisticsViewModel(User logedInUser, NavigationService navigationService)
        {
            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodationByOwnerId(logedInUser.Id));
            _logedInUser = logedInUser;
            NavigationService = navigationService;
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            NavigationService.Navigate(new MyAccommodationsMainView(_logedInUser, NavigationService));
        }

        public ICommand SeeStatisticCommand => new RelayCommandWithParams(SeeStatistic);

        private void SeeStatistic(object parameter)
        {
            if (parameter is Accommodation selectedAccommodation)
            {
                // Navigate to the other window passing the selected guest as a parameter
                NavigationService.Navigate(new MyAccommodationYearlyStatisticView(selectedAccommodation.Id, _logedInUser, NavigationService));

            }
        }
    }
}
