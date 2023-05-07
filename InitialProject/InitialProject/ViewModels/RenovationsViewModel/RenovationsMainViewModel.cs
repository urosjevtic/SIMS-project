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
    public class RenovationsMainViewModel : BaseViewModel
    {


        private readonly User _logedInUser;
        public Navigator Navigator { get;  set; }
        public RenovationsMainViewModel(User logedInUser, Navigator navigator)
        {
            _logedInUser = logedInUser;
            Navigator = navigator;
        }





        public ICommand OpenScheduleRenovationCommand => new RelayCommand(OpenScheduleRenovation);

        private void OpenScheduleRenovation()
        {
            Navigator.NavigateTo(new ScheduleRenovationListView(_logedInUser, Navigator));
        }

        public ICommand OpenScheduledRenovationsCommand => new RelayCommand(OpenScheduledRenovations);

        private void OpenScheduledRenovations()
        {
            Navigator.NavigateTo(new ScheduledRenovationListView(_logedInUser, Navigator));
        }
    }
}
