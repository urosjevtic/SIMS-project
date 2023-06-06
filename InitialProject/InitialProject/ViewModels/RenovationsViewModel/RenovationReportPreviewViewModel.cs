using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Service.RenovationServices;
using InitialProject.Service.ReportService;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.PopupWindows;
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

            SuccessfullPdfDownload successfullPdfDownload = new SuccessfullPdfDownload();
            successfullPdfDownload.ShowDialog();

            NavigationService.Navigate(new RenovationsMainView(_logedInUser, NavigationService));
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void GoBack()
        {
            NavigationService.Navigate(new RenovationsMainView(_logedInUser, NavigationService));
        }



        private string _headerText;

        public string HeaderText
        {
            
            get
            {
                var culture = TranslationSource.Instance.CurrentCulture;
                if (culture.Name == "en-US")
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(
                        "Renovation report containing all the information about renovations in the period between ");
                    sb.Append(_fromDate.ToString("dd/MM/yyyy"));
                    sb.Append(" and ");
                    sb.Append(_toDate.ToString("dd/MM/yyyy"));

                    return sb.ToString();

                }
                else if(culture.Name == "sr-Latn")

                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(
                        "Izveštaj o renoviranju koji sadrži sve informacije o renoviranjima u periodu od ");
                    sb.Append(_fromDate.ToString("dd/MM/yyyy"));
                    sb.Append(" do ");
                    sb.Append(_toDate.ToString("dd/MM/yyyy"));

                    return sb.ToString();
                }

                return null;
            }
        }
    }
}
