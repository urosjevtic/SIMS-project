using System;
using System.Windows.Controls;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for PDFViewerPage.xaml
    /// </summary>
    public partial class PDFViewerPage : Page
    {
        public string PDFFilePath { get; set; }

        public PDFViewerPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public PDFViewerPage(string pdfFilePath)
        {
            InitializeComponent();
            this.DataContext = this;

            PDFFilePath = pdfFilePath;
            PdfWebViewer.Navigate(new Uri(PDFFilePath, UriKind.Absolute));
        }

        private void PdfViewerPage_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            PdfWebViewer.Navigate(new Uri("about:blank"));
        }
    }
}
