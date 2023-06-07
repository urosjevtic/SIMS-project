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
using InitialProject.Utilities;
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


        public void GenerateEnglishnReport(List<Renovation> renovations, DateTime fromDate, DateTime toDate)
        {
            Document pdfDocument = new Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            pdfDocument.Pages.Add(page);
            Label label = new Label($"Renovation report containing all the information about renovations in the period between {fromDate.ToString("dd/MM/yyyy")} and {toDate.ToString("dd/MM/yyyy")}", 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center);
            page.Elements.Add(label);

            int initialY = 120;
            int numberOfReportsPerPage = 0;

            foreach (var renovation in renovations)
            {
                // Check if the current element will fit on the current page
                if (initialY + 210 > page.Dimensions.Height)
                {
                    // Create a new page and add it to the document
                    page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
                    pdfDocument.Pages.Add(page);
                    initialY = 120;
                    page.Elements.Add(label); // Add the label to the new page
                }

                Label nameLabel = new Label(renovation.Accommodation.Name, 10, initialY, 504, 30, Font.Helvetica, 16, TextAlign.Left);
                Label starDateLabel = new Label($"Start date: {renovation.StartDate}", 10, initialY + 40, 504, 30, Font.Helvetica, 12, TextAlign.Left);
                Label endDateLabel = new Label($"End Date: {renovation.EndDate}", 10, initialY + 80, 504, 30, Font.Helvetica, 12, TextAlign.Left);
                Label renovationLabel = new Label(renovation.Description, 10, initialY + 120, 504, 30, Font.Helvetica, 12, TextAlign.Left);
                Label finishedLabel = new Label($"Finished: {renovation.IsFinished}", 10, initialY + 160, 504, 30, Font.Helvetica, 12, TextAlign.Left);

                page.Elements.Add(nameLabel);
                page.Elements.Add(starDateLabel);
                page.Elements.Add(endDateLabel);
                page.Elements.Add(renovationLabel);
                page.Elements.Add(finishedLabel);

                numberOfReportsPerPage++;
                initialY += 200;
            }

            StringBuilder title = new StringBuilder();
            title.Append("../../../Reports/Owner/RenovationReport_");
            title.Append(fromDate.ToString("ddMMyyyy"));  // Convert fromDate to the desired format
            title.Append("_");
            title.Append(toDate.ToString("ddMMyyyy"));  // Convert toDate to the desired format
            title.Append(".pdf");

            pdfDocument.Draw(title.ToString());
        }

        public void GenerateSerbianReport(List<Renovation> renovations, DateTime fromDate, DateTime toDate)
        {
            Document pdfDocument = new Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            pdfDocument.Pages.Add(page);
            Label label = new Label($"Izveštaj o renoviranju koji sadrži sve informacije o renoviranjima u periodu od {fromDate.ToString("dd/MM/yyyy")} do {toDate.ToString("dd/MM/yyyy")}", 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center);

            page.Elements.Add(label);

            int initialY = 120;

            foreach (var renovation in renovations)
            {
                // Check if the current element will fit on the current page
                if (initialY + 210 > page.Dimensions.Height)
                {
                    // Create a new page and add it to the document
                    page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
                    pdfDocument.Pages.Add(page);
                    initialY = 120;
                    page.Elements.Add(label); // Add the label to the new page
                }

                Label nameLabel = new Label(renovation.Accommodation.Name, 10, initialY, 504, 30, Font.Helvetica, 16, TextAlign.Left);
                Label starDateLabel = new Label($"Datum početka: {renovation.StartDate}", 10, initialY + 40, 504, 30, Font.Helvetica, 12, TextAlign.Left);
                Label endDateLabel = new Label($"Datum kraja: {renovation.EndDate}", 10, initialY + 80, 504, 30, Font.Helvetica, 12, TextAlign.Left);
                Label renovationLabel = new Label(renovation.Description, 10, initialY + 120, 504, 30, Font.Helvetica, 12, TextAlign.Left);
                Label finishedLabel = new Label($"Završeno: {renovation.IsFinished}", 10, initialY + 160, 504, 30, Font.Helvetica, 12, TextAlign.Left);

                page.Elements.Add(nameLabel);
                page.Elements.Add(starDateLabel);
                page.Elements.Add(endDateLabel);
                page.Elements.Add(renovationLabel);
                page.Elements.Add(finishedLabel);

                initialY += 210;
            }

            StringBuilder title = new StringBuilder();
            title.Append("../../../Reports/Owner/RenovationReport_");
            title.Append(fromDate.ToString("ddMMyyyy"));  // Convert fromDate to the desired format
            title.Append("_");
            title.Append(toDate.ToString("ddMMyyyy"));  // Convert toDate to the desired format
            title.Append(".pdf");

            pdfDocument.Draw(title.ToString());
        }

        public void GenerateRenovationReport(List<Renovation> renovations, DateTime fromDate, DateTime toDate)
        {
            var culture = TranslationSource.Instance.CurrentCulture;
            if (culture.Name == "en-US")
                GenerateEnglishnReport(renovations, fromDate, toDate);
            else
                GenerateSerbianReport(renovations, fromDate, toDate);
        }

        public List<Renovation> PrepareReport(int ownerId, DateTime startDate, DateTime endDate)
        {
            return GetAllRenovationInDateRange(ownerId, startDate, endDate);
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
