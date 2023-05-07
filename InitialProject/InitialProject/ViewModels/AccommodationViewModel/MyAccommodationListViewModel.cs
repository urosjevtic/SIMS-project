using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InitialProject.Service;
using InitialProject.View.OwnerView.MyAccommodations;
using InitialProject.Utilities;
using System.Windows.Input;
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    public class MyAccommodationListViewModel : BaseViewModel
    {

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private readonly AccommodationService _accommodationService;
        private readonly User _logedInUser;
        public NavigationService NavigationService { get; set; }
        public MyAccommodationListViewModel(User logedInUser, NavigationService navigationService)
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


        public ICommand SeeImagesCommand => new RelayCommandWithParams(SeeImages);

        private void SeeImages(object parameter)
        {
            if (parameter is Accommodation selectedAccommodation)
            {
                NavigationService.Navigate(new MyAccommodationImagesView(selectedAccommodation, _logedInUser, NavigationService));
            }
        }
    }
}
