using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Service.RenovationServices;
using InitialProject.Service.ReportService;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Renovations;

namespace InitialProject.ViewModels.RenovationsViewModel
{
    public class RenovationReportPreviewViewModel : BaseViewModel
    {

        private readonly RenovationService _renovationService;
        private readonly User _logedInUser;
        private readonly OwnerReportService _ownerReportService;
        private readonly DateTime _fromDate;
        private readonly DateTime _toDate;
        public NavigationService NavigationService { get;  set; }

        public ObservableCollection<Renovation> Renovations { get; set; }
        public RenovationReportPreviewViewModel(User logedInUser, NavigationService navigationService, DateTime fromDate, DateTime toDate)
        {
            _ownerReportService = new OwnerReportService();
            _renovationService = new RenovationService();
            _logedInUser = logedInUser;
            _fromDate = fromDate;
            _toDate = toDate;
            NavigationService = navigationService;
            Renovations = new ObservableCollection<Renovation>(_ownerReportService.PrepareReport(_logedInUser.Id, fromDate, toDate));
        }

        public ICommand DownloadCommand => new RelayCommand(Download);

        private void Download()
        {
            _ownerReportService.GenerateRenovationReport(Renovations.ToList(), _fromDate, _toDate);
            NavigationService.Navigate(new RenovationsMainView(_logedInUser, NavigationService));
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            NavigationService.Navigate(new RenovationsMainView(_logedInUser, NavigationService));
        }
    }
}
