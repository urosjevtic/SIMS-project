using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Reservations;

namespace InitialProject.ViewModels.ReservationsViewModels
{
    public class ReservationListViewModel : BaseViewModel
    {

        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly User _logedInUser;
        public ReservationListViewModel(User logedInUser)
        {
            _accommodationReservationService = new AccommodationReservationService();
            Reservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetReservationByOwnerId(logedInUser.Id));
            _logedInUser = logedInUser;
        }


        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            ReservationsMainWindow reservationsMain = new ReservationsMainWindow(_logedInUser);
            CloseCurrentWindow();
            reservationsMain.Show();
        }



    }
}
