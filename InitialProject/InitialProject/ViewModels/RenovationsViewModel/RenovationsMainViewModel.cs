using InitialProject.View.OwnerView.MyAccommodations;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.View.OwnerView.Renovations;
using InitialProject.View.OwnerView.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Utilities;

namespace InitialProject.ViewModels.RenovationsViewModel
{
    public class RenovationsMainViewModel : SideScreenViewModel
    {


        private readonly User _logedInUser;
        public RenovationsMainViewModel(User logedInUser)
        {
            _logedInUser = logedInUser;
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


        public ICommand OpenScheduleRenovationCommand => new RelayCommand(OpenScheduleRenovation);

        private void OpenScheduleRenovation()
        {
            ScheduleRenovationListView scheduleRenovationList = new ScheduleRenovationListView(_logedInUser);
            CloseCurrentWindow();
            scheduleRenovationList.Show();
        }

        public ICommand OpenScheduledRenovationsCommand => new RelayCommand(OpenScheduledRenovations);

        private void OpenScheduledRenovations()
        {
            ScheduledRenovationListView scheduleRenovationList = new ScheduledRenovationListView(_logedInUser);
            CloseCurrentWindow();
            scheduleRenovationList.Show();
        }
    }
}
