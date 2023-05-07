using InitialProject.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.View;
using InitialProject.View.OwnerView.Reservations;
using InitialProject.View.OwnerView.MyAccommodations;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.View.OwnerView.Renovations;

namespace InitialProject.ViewModels.ReservationsViewModels
{
    public class ReservationsMainViewModel : BaseViewModel
    {
        private readonly User _logedInUser;
        public NavigationService NavigationService { get; set; }
        public ReservationsMainViewModel(User logedInUser, NavigationService navigationService)
        {
            _logedInUser = logedInUser;
            NavigationService = navigationService;
        }



        public ICommand ShowAllReservationsCommand => new RelayCommand(ShowAllReservations);

        private void ShowAllReservations()
        {
            NavigationService.Navigate(new ReservationListView(_logedInUser, NavigationService));
        }

        public ICommand HandeReschedulesCommand => new RelayCommand(HandleReschedules);

        private void HandleReschedules()
        {
            NavigationService.Navigate(new RescheduleRequestView(_logedInUser, NavigationService));
        }



    }
}
