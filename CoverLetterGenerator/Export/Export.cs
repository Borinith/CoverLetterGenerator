using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Quality;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CoverLetterGenerator.Export
{
    public class Export : IExport
    {
        public Export()
        {
            // Resolve fonts from the embedded, metric-compatible Arimo so export works on any platform
            GlobalFontSettings.FontResolver ??= new EmbeddedFontResolver();
        }

        public async Task<bool> ExportToPdfAsync(string text, string filename, string title = "", string author = "")
        {
            return await Task.Run(() =>
            {
                // Create a new PDF document
                using var document = new PdfDocument();

                document.Info.Title = title;
                document.Info.Author = author;
                document.Info.Subject = title;

                // Create an empty page
                var page = document.AddPage();

                // Set size of the page
                page.Size = PageSize.A4;
                page.Orientation = PageOrientation.Portrait;

                page.TrimMargins.Top = new XUnit(2.54, XGraphicsUnit.Centimeter);
                page.TrimMargins.Left = new XUnit(2.54, XGraphicsUnit.Centimeter);
                page.TrimMargins.Bottom = new XUnit(2.54, XGraphicsUnit.Centimeter);
                page.TrimMargins.Right = new XUnit(2.54, XGraphicsUnit.Centimeter);

                page.Height = page.Height - page.TrimMargins.Top - page.TrimMargins.Bottom;
                page.Width = page.Width - page.TrimMargins.Left - page.TrimMargins.Right;

                // Get an XGraphics object for drawing
                using var gfx = XGraphics.FromPdfPage(page);

                var tf = new XTextFormatterEx2(gfx,
                    new XTextFormatterEx2.LayoutOptions
                    {
                        Spacing = 8,
                        SpacingMode = XTextFormatterEx2.SpacingMode.Relative,
                        SpacingOnNewLine = true
                    })
                {
                    Alignment = XParagraphAlignment.Justify
                };

                // Create a font
                var font = new XFont("Arimo", 14);

                // Draw the text
                tf.DrawString(text, font, XBrushes.Black,
                    new XRect(0, 0, page.Width.Point, page.Height.Point),
                    XStringFormats.TopLeft);

                // Set PDF/A
                document.SetPdfA();

                // Save the document
                var path = Path.Combine(Environment.CurrentDirectory, filename);

                try
                {
                    document.Save(path);
                    PdfFileUtility.ShowDocumentIfDebugging(filename);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
    }
}