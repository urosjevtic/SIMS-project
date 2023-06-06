using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using InitialProject.Service.StatisticService;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.PopupWindows;
using InitialProject.View.OwnerView.Reservations;

namespace InitialProject.ViewModels.ReservationsViewModels
{
    public class ReservationListViewModel : BaseViewModel
    {

        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly AccommodationStatisticService _statisticService;
        private readonly User _logedInUser;
        public NavigationService NavigationService { get; set; }

        public ReservationListViewModel(User logedInUser, NavigationService navigationService)
        {
            _accommodationReservationService = new AccommodationReservationService();
            _statisticService = new AccommodationStatisticService();
            Reservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetReservationByOwnerId(logedInUser.Id));
            _logedInUser = logedInUser;
            NavigationService = navigationService;
        }


        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            NavigationService.Navigate(new ReservationsMainView(_logedInUser, NavigationService));
        }

        public ICommand CancelReservationCommand => new RelayCommandWithParams(CancelReservation);

        //private void CancelReservation(object parameter)
        //{
        //    if (parameter is AccommodationReservation selecterdReservation)
        //    {
        //        _accommodationReservationService.Delete(selecterdReservation);
        //        IncreaseCancelReservationCount(selecterdReservation.AccommodationId);
        //        Reservations.Remove(selecterdReservation);
        //    }
        //}

        //private void IncreaseCancelReservationCount(int accommodationId)
        //{
        //    _statisticService.IncreaseCancelationCount(accommodationId);
        //}



        private void CancelReservation(object parameter)
        {
                if (parameter is AccommodationReservation selecterdReservation)
                {
                    ConfirmCancelingReservationView confirmCancelingReservationView =
                        new ConfirmCancelingReservationView(selecterdReservation, Reservations);

                    confirmCancelingReservationView.ShowDialog();
                }
        }


    }
}
