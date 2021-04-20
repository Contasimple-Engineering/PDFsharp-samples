using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphics;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

namespace Graphics
{
    class Program
    {
        internal static PdfDocument Document;

        static void Main(string[] args)
        {
            // Use WinAnsi (i.e. not Unicode) for all fonts if no XPdfFontOptions are specified.
            GlobalFontSettings.DefaultFontEncoding = PdfFontEncoding.WinAnsi;

            var filename = String.Format("{0}_tempfile.pdf", Guid.NewGuid().ToString("D").ToUpper());
            Document = new PdfDocument();
            Document.Info.Title = "PDFsharp XGraphic Sample";
            Document.Info.Author = "Stefan Lange";
            Document.Info.Subject = "Created with code snippets that show the use of graphical functions";
            Document.Info.Keywords = "PDFsharp, XGraphics";


            // Create demonstration pages.
            new LinesAndCurves().DrawPage(Document.AddPage());
            new Shapes().DrawPage(Document.AddPage());
            new Paths().DrawPage(Document.AddPage());
            new Text().DrawPage(Document.AddPage());
            new Images().DrawPage(Document.AddPage());

            // Save the document...
            Document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
    }
}
