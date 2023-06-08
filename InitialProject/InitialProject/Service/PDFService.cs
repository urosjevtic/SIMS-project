using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InitialProject.Domain.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;

namespace InitialProject.Service
{
    public static class PDFService
    {
        private static string OpenFilePicker()
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            if(saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }
            throw new Exception("Save file dialog returned error!");
        }

        public static void GenerateTourReservationPDF(Tour tour, TourReservation reservation)
        {
            try
            {
                string filePath = OpenFilePicker();

                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);
                writer.SetFullCompression();

                document.Open();

                string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                string imagePath = $"{assemblyName}.Resources.Images.logopdf.png";

                using(Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imagePath))
                {
                    if(stream != null)
                    {
                        iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance(stream);

                        float desiredHeight = 150f;
                        float aspectRatio = logoImage.Width / logoImage.Height;
                        float desiredWidth = desiredHeight * aspectRatio;

                        PdfPTable table = new PdfPTable(2);
                        table.DefaultCell.Border = Rectangle.NO_BORDER;

                        PdfPCell imageCell = new PdfPCell(logoImage);
                        imageCell.FixedHeight = desiredHeight;
                        imageCell.Border = Rectangle.NO_BORDER;
                        table.AddCell(imageCell);

                        PdfPCell textCell = new PdfPCell(new Phrase("Booking"));
                        textCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        textCell.Border = Rectangle.NO_BORDER;

                        BaseFont baseFont = BaseFont.CreateFont("Helvetica-Bold", "Cp1252", false);
                        Font font = new Font(baseFont, 30f);

                        Chunk chunk = new Chunk("Booking", font);
                        Phrase phrase = new Phrase(chunk);
                        textCell.AddElement(phrase);

                        table.AddCell(textCell);

                        document.Add(table);
                    }
                    else
                    {
                        MessageBox.Show("Error loading image!");
                    }
                }

                Paragraph heading = new Paragraph($"Tour reservation report: \n\n", new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD))
                {
                    SpacingAfter = 15f
                };
                document.Add(heading);

                Paragraph details = new($"Your tour through {tour.Location.City}, {tour.Location.Country} starts at {reservation.DateAndTime:dd.MM.yyyy hh:mm:ss}, \n" +
                                        $"Number of guests you reserved for: {reservation.NumberOfPeople}",
                                        new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD))
                                        {
                                            SpacingAfter = 15f
                                        };
                document.Add(details);
                Paragraph footer = new($"Thank you for your trust \n" + 
                                        $"Your Booking", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD))
                {
                    SpacingAfter = 15f
                };
                document.Add(footer);
                document.Close();
            } catch(Exception ex)
            {
                MessageBox.Show("Error generating PDF file: "+ ex.Message);
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        public static void GenerateGuideReportPDF(List<ShowingTour> tours,DateTime from,DateTime to)
        {
            
                string filePath = OpenFilePicker();

                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);
                writer.SetFullCompression();

                document.Open();

                string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                string imagePath = $"{assemblyName}.Resources.Images.logoZemlja.png";

                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imagePath))
                {
                    if (stream != null)
                    {
                        iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance(stream);

                        float desiredHeight = 150f;
                        float aspectRatio = logoImage.Width / logoImage.Height;
                        float desiredWidth = desiredHeight * aspectRatio;

                        PdfPTable table = new PdfPTable(2);
                        table.DefaultCell.Border = Rectangle.NO_BORDER;

                        PdfPCell imageCell = new PdfPCell(logoImage);
                        imageCell.FixedHeight = desiredHeight;
                        imageCell.Border = Rectangle.NO_BORDER;
                        table.AddCell(imageCell);

                        PdfPCell textCell = new PdfPCell(new Phrase("Tour app"));
                        textCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        textCell.Border = Rectangle.NO_BORDER;

                        BaseFont baseFont = BaseFont.CreateFont("Helvetica-Bold", "Cp1252", false);
                        Font font = new Font(baseFont, 30f);

                        Chunk chunk = new Chunk("Booking", font);
                        Phrase phrase = new Phrase(chunk);
                        textCell.AddElement(phrase);

                        table.AddCell(textCell);

                        document.Add(table);
                    }
                    else
                    {
                        MessageBox.Show("Error loading image!");
                    }
                }

                Paragraph heading = new Paragraph($"Tours report: \n\n", new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD))
                {
                    SpacingAfter = 15f
                };
                document.Add(heading);

                Paragraph dateRange = new Paragraph($"From: {from:dd.MM.yyyy hh:mm:ss}  to {to:dd.MM.yyyy hh:mm:ss} you have this tours:\n", new Font(Font.FontFamily.HELVETICA, 18, Font.NORMAL))
                {
                    SpacingAfter = 15f
                };
                document.Add(dateRange);

                foreach (ShowingTour tour in tours)
                {
                    Paragraph details = new($"Tour name: {tour.Name}\nLocation: {tour.Location}  \nMax number of guests: {tour.MaxGuests}\n" +
                           $"Language: {tour.Language}\nStart date: {tour.Start:dd.MM.yyyy hh:mm:ss}\n",
                                     new Font(Font.FontFamily.HELVETICA, 14, Font.NORMAL))
                    {
                        
                    };
                    document.Add(details);
                    
                    Paragraph novired = new($"\n\n",
                                   new Font(Font.FontFamily.HELVETICA, 14, Font.NORMAL))
                    {

                    };
                    document.Add(novired);

                }

               
                
                Paragraph footer = new($"\n" +
                                        $"Your Booking", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD))
                {
                    SpacingAfter = 15f
                };
                document.Add(footer);
                document.Close();
                MessageBox.Show("You successfuly generated report!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);


            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c start \"\" \"{filePath}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            });


        }
    }
}
