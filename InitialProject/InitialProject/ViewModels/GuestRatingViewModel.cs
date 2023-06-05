using ceTe.DynamicPDF.PageElements;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Service;
using InitialProject.Service.RenovationServices;
using InitialProject.Service.ReportService;
using InitialProject.Service.ReportServices;
using InitialProject.Utilities;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    internal class GuestRatingViewModel : BaseViewModel
    {
        public ObservableCollection<RatedGuest>Ratings { get; set; }
        private readonly RatedGuestService _ratedGuestService;
        private  User LoggedUser { get; set; } = App.LoggedUser;
      
        
        /// ///////////////////////////
        public readonly GuestReportService _guestReportService;
        public ObservableCollection<Renovation> Renovations { get; set; }
        private readonly RenovationService _renovationService;
        /// /////////////////////////////////////////

        public NavigationService NavigationService { get; set; }
        
        public GuestRatingViewModel(NavigationService navigationService)
        {
            _guestReportService = new GuestReportService();
            _renovationService = new RenovationService();
            _ratedGuestService = new RatedGuestService();
            Ratings = new ObservableCollection<RatedGuest>(_ratedGuestService.GetRatedGuests());
            NavigationService = navigationService;
            Renovations = new ObservableCollection<Renovation>(_renovationService.GetByOwnerId(LoggedUser.Id));
        }
        public ICommand GeneratePdfCommand => new RelayCommand(GeneratePdf);

        private void GeneratePdf()
        {
            string fileName = "Reviews Report - " + DateTime.Now.ToString("dd.MM.yyyy") + ".pdf";
            string filePath = "../../../Reports/" + fileName;

            string absoluteFilePath = System.IO.Path.GetFullPath(filePath);

            _guestReportService.GenerateReviewsReport(absoluteFilePath);
            this.NavigationService.Navigate(new PDFViewerPage(absoluteFilePath));
        }
    }
}
