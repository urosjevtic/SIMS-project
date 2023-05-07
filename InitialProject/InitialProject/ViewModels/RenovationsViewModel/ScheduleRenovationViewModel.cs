using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public Navigator Navigator { get; set; }

        public ScheduleRenovationViewModel(User logedInUser, Navigator navigator)
        {
            _accommodationService = new AccommodationService();
            _logedInUser = logedInUser;
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodationByOwnerId(logedInUser.Id));
            Navigator = navigator;
        }



        public ICommand ScheduleRenovationCommand => new RelayCommandWithParams(ScheduleRenovation);

        private void ScheduleRenovation(object parameter)
        {
            if (parameter is Accommodation selectedAccommodation)
            {
                Navigator.NavigateTo(new ScheduleRenovationFormView(_logedInUser, selectedAccommodation, Navigator));

            }
        }
        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            Navigator.NavigateTo(new RenovationsMainView(_logedInUser, Navigator));
        }
    }
}
