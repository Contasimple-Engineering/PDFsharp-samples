using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace MarkupNetCore31
{
    class Program
    {
        private static readonly Int32 PictureMargin = 10;
        const string SamplePath = "../../../../../assets/images/Z3.jpg";

        static void Main(string[] args)
        {
            var document = new PdfDocument();
            document.Info.Creator = "Contasimple";
            document.Info.Author = "Contasimple";
            document.Info.Title = "Contasimple";
            document.Info.Subject = "Contasimple";
            document.Info.CreationDate = DateTime.Now;
            document.Info.ModificationDate = DateTime.Now;
            AppendXmpProperty(
                document,
                "Software",
                "SampleValue");
            // Append picture
            AppendPicture(
                document,
                File.ReadAllBytes(SamplePath));
            // Generate document
            const string filename = "Markup_net472.pdf";
            document.Save(filename);
            document.Close();
            // Open created PDF
            Process.Start("cmd.exe", $"/c {filename}");
        }

        private static void AppendXmpProperty(PdfDocument document, String name, String value)
        {
            document.Info.Elements.Add(
                new KeyValuePair<String, PdfItem>(
                    "/" + name,
                    new PdfString(value)));
        }

        /// <summary>
        /// Appends the given image to the document
        /// </summary>
        /// <param name="document"></param>
        /// <param name="picture"></param>
        private static void AppendPicture(PdfDocument document, Byte[] picture)
        {
            // Create image
            using (var stream = new MemoryStream(picture))
            {
                using (var image = Image.FromStream(stream))
                {
                    // Create a new page
                    var page = document.AddPage();
                    page.Width = image.Width + (PictureMargin * 2);
                    page.Height = image.Height + (PictureMargin * 2);

                    // Prepare graphics context
                    using (var gfx = XGraphics.FromPdfPage(page))
                    {
                        // Create the PDF image
                        using (var pdfImage = XImage.FromStream(stream))
                        {
                            // Prepare destination rectangle
                            var destinationRectangle = new XRect(
                                new XPoint(PictureMargin, PictureMargin),
                                new XSize((Single)page.Width - (PictureMargin * 2), (Single)page.Height - (PictureMargin * 2)));

                            // Draw image
                            gfx.DrawImage(pdfImage, destinationRectangle);
                        }
                    }
                }
            }
        }
    }
}
