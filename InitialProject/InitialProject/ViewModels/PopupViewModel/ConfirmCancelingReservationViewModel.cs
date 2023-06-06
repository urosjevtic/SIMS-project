using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using InitialProject.Service.StatisticService;
using InitialProject.Utilities;
using InitialProject.View;

namespace InitialProject.ViewModels.PopupViewModel
{
    public class ConfirmCancelingReservationViewModel : BaseViewModel
    {

        private readonly AccommodationStatisticService _statisticService;
        private readonly AccommodationReservationService _accommodationReservationService;

        private readonly AccommodationReservation _reservation;
        private readonly ObservableCollection<AccommodationReservation> _reservations;
        public ConfirmCancelingReservationViewModel(AccommodationReservation resrvation, ObservableCollection<AccommodationReservation> reservations)
        {
            _statisticService = new AccommodationStatisticService();
            _accommodationReservationService = new AccommodationReservationService();
            _reservation = resrvation;
            _reservations = reservations;
        }


        public ICommand YesCommand => new RelayCommand(Yes);

        private void Yes()
        {

            _accommodationReservationService.Delete(_reservation);
            IncreaseCancelReservationCount(_reservation.AccommodationId);
            _reservations.Remove(_reservation);
            CloseCurrentWindow();
        }

        private void IncreaseCancelReservationCount(int accommodationId)
        {
            _statisticService.IncreaseCancelationCount(accommodationId);
        }


        public ICommand NoCommand => new RelayCommand(No);

        private void No()
        {
            CloseCurrentWindow();
        }
    }
}
