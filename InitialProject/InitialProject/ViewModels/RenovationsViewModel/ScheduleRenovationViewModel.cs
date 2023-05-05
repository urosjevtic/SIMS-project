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


        public ScheduleRenovationViewModel(User logedInUser)
        {
            _accommodationService = new AccommodationService();
            _logedInUser = logedInUser;
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodationByOwnerId(logedInUser.Id));
        }



        public ICommand ScheduleRenovationCommand => new RelayCommandWithParams(ScheduleRenovation);

        private void ScheduleRenovation(object parameter)
        {
            if (parameter is Accommodation selectedAccommodation)
            {
                // Navigate to the other window passing the selected guest as a parameter
                ScheduleRenovationFormView scheduleRenovationForm =
                    new ScheduleRenovationFormView(_logedInUser, selectedAccommodation);
                CloseCurrentWindow();
                scheduleRenovationForm.Show();

            }
        }
    }
}
