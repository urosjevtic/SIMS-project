using InitialProject.Domain.Model.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    public class MoveRequestViewModel
    {
        public AccommodationReservation SelectedReservation;
        public NavigationService _navigationService;
        public MoveRequestViewModel(AccommodationReservation selectedReservation)
        {
            //_navigationService= navigationService;
            SelectedReservation = selectedReservation;
        }
    }
}
