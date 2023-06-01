using ceTe.DynamicPDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace InitialProject.PdfGenerate
{
    class TextPdfAdapter //: IFileGenerator
    {
        private Document document;
        private String fileLocation = "../../pdfGenerate/Izvestaj.pdf";

        //public Boolean generate()
        //{
        //    endParagraph();
        //    iTextSharp.text.Image sig = iTextSharp.text.Image.GetInstance("../../Images/sigSmall.png");
        //    sig.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
        //    document.Add(sig);
        //    document.Close();
        //    return true;
        //}

    }
}
