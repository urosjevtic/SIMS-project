using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Service.RenovationServices;
using InitialProject.Service.ReportService;
using InitialProject.View.OwnerView.Renovations;

namespace InitialProject.ViewModels.RenovationsViewModel
{
    public class RenovationReportSelectionViewModel:BaseViewModel
    {

        private readonly RenovationService _renovationService;
        private readonly User _logedInUser;
        public ObservableCollection<Renovation> Renovations {get; set; }
        public NavigationService NavigationService { get; set; }
        public RenovationReportSelectionViewModel(User logedInUser, NavigationService navigationService)
        {
            _renovationService = new RenovationService();
            _logedInUser = logedInUser;
            NavigationService = navigationService;
            Renovations = new ObservableCollection<Renovation>(_renovationService.GetByOwnerId(_logedInUser.Id));
        }


        private DateTime _fromDate = DateTime.Today;

        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                _fromDate = value; 
                OnPropertyChanged("FromDate");
            }
        }

        private DateTime _toDate = DateTime.Today.AddDays(1);

        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                _toDate = value;
                OnPropertyChanged("ToDate");
            }
        }


        public ICommand GeneratePdfCommand => new RelayCommand(GeneratePdf);

        private void GeneratePdf()
        {
            NavigationService.Navigate(new RenovationReportPreviewView(_logedInUser, NavigationService, _fromDate, _toDate));
            CloseCurrentWindow();
        }

        public ICommand CancelCommand => new RelayCommand(CloseWindow);

        private void CloseWindow()
        {
            CloseCurrentWindow();
        }
    }
}
