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
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Utilities;

namespace InitialProject.ViewModels.RenovationsViewModel
{
    public class RenovationsMainViewModel : BaseViewModel
    {


        private readonly User _logedInUser;
        public NavigationService NavigationService { get;  set; }
        public RenovationsMainViewModel(User logedInUser, NavigationService navigationService)
        {
            _logedInUser = logedInUser;
            NavigationService = navigationService;
        }





        public ICommand OpenScheduleRenovationCommand => new RelayCommand(OpenScheduleRenovation);

        private void OpenScheduleRenovation()
        {
            NavigationService.Navigate(new ScheduleRenovationListView(_logedInUser, NavigationService));
        }

        public ICommand OpenScheduledRenovationsCommand => new RelayCommand(OpenScheduledRenovations);

        private void OpenScheduledRenovations()
        {
            NavigationService.Navigate(new ScheduledRenovationListView(_logedInUser, NavigationService));
        }

        public ICommand OpenSugestedRenovationsCommand => new RelayCommand(OpenSugestedRenovations);

        private void OpenSugestedRenovations()
        {
            NavigationService.Navigate(new RenovationSugestionView(_logedInUser, NavigationService));
        }

        public ICommand OpenGeneratePdfCommand => new RelayCommand(OpenGeneratePdf);

        private void OpenGeneratePdf()
        {
            RenovationReportFormView renovationReportFormView = new RenovationReportFormView(_logedInUser, NavigationService);
            renovationReportFormView.ShowDialog();
        }
    }
}
