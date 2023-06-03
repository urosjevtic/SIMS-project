using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Service.RenovationServices;
using Font = ceTe.DynamicPDF.Font;
using Image = ceTe.DynamicPDF.PageElements.Image;

namespace InitialProject.Service.ReportServices
{
    public class GuestReportService
    {

        private readonly RatedGuestService _guestService;

        //private readonly RenovationService _renovationServices;

        public GuestReportService()
        {
            //_renovationServices = new RenovationService();
            _guestService = new RatedGuestService();
        }


        public void GenerateRenovationReport()
        {
            Document pdfDocument = new Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            pdfDocument.Pages.Add(page);
            Label label = new Label("Average rating from the owner", 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center);
            page.Elements.Add(label);

            AverageGrade(_guestService.GetRatedGuests());

            int initialY = 120;
            Label cleanliness = new Label("Cleanliness:", 10, initialY, 504, 30, Font.Helvetica, 16, TextAlign.Left);
            Label ocena1 = new Label(AverageGrade(_guestService.GetRatedGuests()).ToString(), 10, initialY + 40, 504, 30, Font.Helvetica, 12, TextAlign.Left);
            Label rules = new Label("Followinf rules:", 10, initialY + 80, 504, 30, Font.Helvetica, 12, TextAlign.Left);
            
            // List<Renovation> renovations = GetAllRenovationInDateRange(ownerId);


            //foreach (var renovation in renovations)
            //{
            //    Label nameLabel = new Label(renovation.Accommodation.Name, 10, initialY, 504, 30, Font.Helvetica, 16, TextAlign.Left);
            //    Label starEndDateLabel = new Label($"Star: {renovation.StartDate}, End Date: {renovation.EndDate}", 10, initialY + 40, 504, 30, Font.Helvetica, 12, TextAlign.Left);
            //    Label renovationLabel = new Label(renovation.Description, 10, initialY + 80, 504, 30, Font.Helvetica, 12, TextAlign.Left);
            //    Label finishedLabel = new Label($"Finished: {renovation.IsFinished}", 10, initialY + 120, 504, 30, Font.Helvetica, 12, TextAlign.Left);

            //    page.Elements.Add(nameLabel);
            //    page.Elements.Add(starEndDateLabel);
            //    page.Elements.Add(renovationLabel);
            //    page.Elements.Add(finishedLabel);

            //    initialY += 220;
            //}



            pdfDocument.Draw("../../../Reports/Guest/NewPdf.pdf");
        }

        public static Dictionary<string, double> AverageGrade(List<RatedGuest> guests)
        {
            var averageGrade = new Dictionary<string, double>();
            var cleanliness = 0;
            var rulesFollowing = 0;

            foreach (var guest in guests)
            {
                cleanliness += guest.CleanlinessRating;
                rulesFollowing += guest.RuleFollowingRating;
            }

            var averageCleanliness = (double)cleanliness / guests.Count;
            var averageRulesFollowing= (double)rulesFollowing / guests.Count;

            averageGrade["cleanliness"] = averageCleanliness;
            averageGrade["rulesFollowing"] = averageRulesFollowing;

            return averageGrade;
        }
    }
}
