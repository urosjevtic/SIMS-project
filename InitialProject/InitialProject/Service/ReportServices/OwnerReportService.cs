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

namespace InitialProject.Service.ReportService
{
    public class OwnerReportService
    {

        private readonly RenovationService _renovationServices;

        public OwnerReportService()
        {
            _renovationServices = new RenovationService();
        }


        public void GenerateRenovationReport(int ownerId, DateTime starDate, DateTime endTime)
        {
            Document pdfDocument = new Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            pdfDocument.Pages.Add(page);
            Label label = new Label("Hello world!", 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center);
            page.Elements.Add(label);

            List<Renovation> renovations = GetAllRenovationInDateRange(ownerId, starDate, endTime);

            int initialY = 120;

            foreach (var renovation in renovations)
            {
                Label nameLabel = new Label(renovation.Accommodation.Name, 10, initialY, 504, 30, Font.Helvetica, 16, TextAlign.Left);
                Label starEndDateLabel = new Label($"Star: {renovation.StartDate}, End Date: {renovation.EndDate}", 10, initialY + 40, 504, 30, Font.Helvetica, 12, TextAlign.Left);
                Label renovationLabel = new Label(renovation.Description, 10, initialY + 80, 504, 30, Font.Helvetica, 12, TextAlign.Left);
                Label finishedLabel = new Label($"Finished: {renovation.IsFinished}", 10, initialY + 120, 504, 30, Font.Helvetica, 12, TextAlign.Left);

                page.Elements.Add(nameLabel);
                page.Elements.Add(starEndDateLabel);
                page.Elements.Add(renovationLabel);
                page.Elements.Add(finishedLabel);

                initialY += 220;
            }



            pdfDocument.Draw("../../../Reports/Owner/NewPdf.pdf");
        }

        private List<Renovation> GetAllRenovationInDateRange(int ownerId, DateTime startDate, DateTime endDate)
        {
            List<Renovation> allRenovations = _renovationServices.GetByOwnerId(ownerId);
            List<Renovation> selectedRenovations = new List<Renovation>();
            foreach (var renovation in allRenovations)
            {
                if (renovation.StartDate >= startDate && renovation.EndDate <= endDate)
                {
                    selectedRenovations.Add(renovation);
                }
            }

            return selectedRenovations;
        }




    }

}
