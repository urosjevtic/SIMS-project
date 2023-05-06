using System.Collections.ObjectModel;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using InitialProject.Service.StatisticService;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Reservations;

namespace InitialProject.ViewModels.ReservationsViewModels
{
    public class ReservationListViewModel : BaseViewModel
    {

        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly YearlyAccommodationService _yearlyAccommodationStatisticService;
        private readonly AccommodationStatisticService _statisticService;
        private readonly User _logedInUser;
        public ReservationListViewModel(User logedInUser)
        {
            _accommodationReservationService = new AccommodationReservationService();
            _yearlyAccommodationStatisticService = new YearlyAccommodationService();
            _statisticService = new AccommodationStatisticService();
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

        public ICommand CancelReservationCommand => new RelayCommandWithParams(CancelReservation);

        private void CancelReservation(object parameter)
        {
            if (parameter is AccommodationReservation selecterdReservation)
            {
                _accommodationReservationService.Delete(selecterdReservation);
                IncreaseCancelReservationCount(selecterdReservation.AccommodationId);
                Reservations.Remove(selecterdReservation);
            }
        }

        private void IncreaseCancelReservationCount(int accommodationId)
        {
            _statisticService.IncreasCancelationCount(accommodationId);
        }



    }
}
