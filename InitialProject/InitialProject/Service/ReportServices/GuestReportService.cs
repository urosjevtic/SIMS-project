//using System;
//using System.Collections.Generic;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using Font = ceTe.DynamicPDF.Font;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO.Packaging;
using System.Reflection.Metadata;

namespace InitialProject.Service.ReportServices
{
    public class GuestReportService :Template
    {


        private readonly RatedGuestService _guestService;
        private object assemblyName;

        //private readonly RenovationService _renovationServices;

        public GuestReportService()
        {
            //_renovationServices = new RenovationService();
            _guestService = new RatedGuestService();
        }

        public class HeaderFooterTemplate : Template
        {
            string nesto;
            string drugo;
            private string v1;
            private string v2;

            public HeaderFooterTemplate(string v1, string v2)
            {
                this.v1 = v1;
                this.v2 = v2;
            }
        }
        public void GenerateReviewsReport(string filePath)
        {
            ceTe.DynamicPDF.Document pdfDocument = new ceTe.DynamicPDF.Document();
            Page page = new Page(ceTe.DynamicPDF.PageSize.Letter, PageOrientation.Portrait, 54.0f);
            pdfDocument.Pages.Add(page);
            Label label = new Label("Average rating from the owner", 0, 0, 504, 100, Font.Helvetica, 25, TextAlign.Center);
            page.Elements.Add(label);
            HeaderFooterTemplate header = new HeaderFooterTemplate("HeaderText", "FooterText");
            pdfDocument.Template = header;

            

            PdfPCell textCell=new PdfPCell(new iTextSharp.text.Phrase ("BOOKING"));
            textCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            textCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            string datum = System.DateTime.Now.ToString("dd.MM.yyyy");
            string autor = "Guest";

            int initialY = 120;


            Label text= new Label("Average scores for each category: ", 10, initialY, 504, 30, Font.Helvetica, 15, TextAlign.Left);
            Label cleanliness = new Label("Cleanliness: 4.76", 10, initialY + 40, 504, 30, Font.Helvetica, 12, TextAlign.Left);
            Label rules = new Label("Following rules: 3,89", 10, initialY + 80, 504, 30, Font.Helvetica, 12, TextAlign.Left);
            page.Elements.Add (text);
            page.Elements.Add(cleanliness);
            page.Elements.Add(rules);

            Label footer = new Label ("Thank you for your trust \n" + "Your bookinng", 10, initialY+140, 504,30, Font.Helvetica, 13);
            page.Elements.Add (footer);
            // page.Elements.Add(new Image(@"C:\JELENA\Sesti semestar\SIMS\BookingApp\SIMS-project\InitialProject\InitialProject\Resources\Images\logo.png", 30, 30));
            Label autor1= new Label(autor, 10, initialY + 180, 504, 30, Font.Helvetica, 14, TextAlign.Right);
            Label datum1 = new Label(datum, 10, initialY + 220, 504, 30, Font.Helvetica, 14, TextAlign.Right);
            page.Elements.Add(autor1);
            page.Elements.Add(datum1);
            pdfDocument.Draw(filePath);
        }

    }
}
