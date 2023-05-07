using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InitialProject.Service;
using InitialProject.View.OwnerView.MyAccommodations;
using InitialProject.Utilities;
using System.Windows.Input;

namespace InitialProject.ViewModels
{
    public class MyAccommodationListViewModel : BaseViewModel
    {

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private readonly AccommodationService _accommodationService;
        private readonly User _logedInUser;
        public Navigator Navigator { get; set; }
        public MyAccommodationListViewModel(User logedInUser, Navigator navigator)
        {
            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodationByOwnerId(logedInUser.Id));
            _logedInUser = logedInUser;
            Navigator = navigator;
        }


        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            Navigator.NavigateTo(new MyAccommodationsMainView(_logedInUser, Navigator));
        }


        public ICommand SeeImagesCommand => new RelayCommandWithParams(SeeImages);

        private void SeeImages(object parameter)
        {
            if (parameter is Accommodation selectedAccommodation)
            {
                Navigator.NavigateTo(new MyAccommodationImagesView(selectedAccommodation, _logedInUser, Navigator));
            }
        }
    }
}
