using InitialProject.Domain.Model.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    public class MoveRequestViewModel : BaseViewModel
    {
        private AccommodationReservation _selectedReservation;
        public NavigationService _navigationService;

        public AccommodationReservation SelectedReservation
        {
            get
            {
                return _selectedReservation;
            }
            set
            {
                if(value != _selectedReservation)
                {
                    _selectedReservation = value;
                    OnPropertyChanged(nameof(SelectedReservation));
                }
            }
        }


        public MoveRequestViewModel(AccommodationReservation selectedReservation, NavigationService navigationService)
        {
            _navigationService= navigationService;
            SelectedReservation = selectedReservation;
        }
    }
}
