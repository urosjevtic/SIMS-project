using InitialProject.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using InitialProject.Domain.Model;
using InitialProject.View;
using InitialProject.View.OwnerView.Reservations;
using InitialProject.View.OwnerView.MyAccommodations;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.View.OwnerView.Renovations;

namespace InitialProject.ViewModels.ReservationsViewModels
{
    public class ReservationsMainViewModel : SideScreenViewModel
    {
        private readonly User _logedInUser;

        public ReservationsMainViewModel(User logedInUser)
        {
            _logedInUser = logedInUser;
        }



        public ICommand ShowAllReservationsCommand => new RelayCommand(ShowAllReservations);

        private void ShowAllReservations()
        {
            ReservationListWindow reservationListWindow = new ReservationListWindow(_logedInUser);
            CloseCurrentWindow();
            reservationListWindow.Show();
        }

        public ICommand HandeReschedulesCommand => new RelayCommand(HandleReschedules);

        private void HandleReschedules()
        {
            RescheduleRequestWindow rescheduleRequestWindow = new RescheduleRequestWindow(_logedInUser);
            CloseCurrentWindow();
            rescheduleRequestWindow.Show();
        }


        protected override void MyAccommoadionsOpen()
        {
            MyAccommodationsMainWindow myAccommodationsMainWindow = new MyAccommodationsMainWindow(_logedInUser);
            CloseCurrentWindow();
            myAccommodationsMainWindow.Show();

        }


        protected override void RatingsOpen()
        {
            RatingsMainWindow ratingsMain = new RatingsMainWindow(_logedInUser);
            CloseCurrentWindow();
            ratingsMain.Show();
        }


        protected override void ReservationsOpen()
        {
            ReservationsMainWindow reservationsMain = new ReservationsMainWindow(_logedInUser);
            CloseCurrentWindow();
            reservationsMain.Show();
        }

        protected override void RenovationsOpen()
        {
            RenovationsMainView renovationsMainView = new RenovationsMainView(_logedInUser);
            CloseCurrentWindow();
            renovationsMainView.Show();
        }

    }
}
