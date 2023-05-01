using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using InitialProject.Service.StatisticService;
using InitialProject.Utilities;
using InitialProject.View;
using InitialProject.View.OwnerView;
using InitialProject.View.OwnerView.Reservations;

namespace InitialProject.ViewModels.ReservationsViewModels
{
    public class RescheduleRequestViewModel : BaseViewModel
    {
        private readonly AccommodationReservationRescheduleRequestService _accommodationReservationRescheduleRequestService;
        private readonly DeclinedAccommodationReservationRescheduleRequestService _declinedReservationRescheduleRequestService;
        private readonly YearlyAccommodationService _yearlyAcoommodationStatisticService;
        private readonly MonthlyAccommodationStatisticService _monthlyAccommodationStatisticService;
        private readonly User _loggedInUser;

        public ObservableCollection<AccommodationReservationRescheduleRequest> RescheduleRequests { get; }

        public RescheduleRequestViewModel(User loggedInUser)
        {
            _accommodationReservationRescheduleRequestService = new AccommodationReservationRescheduleRequestService();
            _yearlyAcoommodationStatisticService = new YearlyAccommodationService();
            _monthlyAccommodationStatisticService = new MonthlyAccommodationStatisticService();
            _loggedInUser = loggedInUser;

            RescheduleRequests = new ObservableCollection<AccommodationReservationRescheduleRequest>(_accommodationReservationRescheduleRequestService.GetAllByOwnerId(_loggedInUser.Id));
        }



        public ICommand ApproveRescheduleCommand => new RelayCommandWithParams(ApproveReschedule);

        private void ApproveReschedule(object parameter)
        {
            if (parameter is AccommodationReservationRescheduleRequest selectedRescheduleRequest)
            {
                _accommodationReservationRescheduleRequestService.ApproveReschedule(selectedRescheduleRequest);
                _yearlyAcoommodationStatisticService.IncreaseRescheduleCount(selectedRescheduleRequest.Reservation.AccommodationId);
                _monthlyAccommodationStatisticService.IncreaseRescheduleCount(selectedRescheduleRequest.Reservation.AccommodationId);
                
                RescheduleRequests.Remove(selectedRescheduleRequest);


            }
        }

        public ICommand DeclineRescheduleCommand => new RelayCommandWithParams(DeclineReschedule);

        private void DeclineReschedule(object parameter)
        {
            if (parameter is AccommodationReservationRescheduleRequest selectedRescheduleRequest)
            {
                RescheduleDeclineWindow rescheduleDeclineWindow = new RescheduleDeclineWindow(selectedRescheduleRequest);
                rescheduleDeclineWindow.ShowDialog();
                RescheduleRequests.Remove(selectedRescheduleRequest);

            }
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            ReservationsMainWindow reservationsMain = new ReservationsMainWindow(_loggedInUser);
            CloseCurrentWindow();
            reservationsMain.Show();
        }






    }
}
