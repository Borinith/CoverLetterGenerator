using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CoverLetterGenerator.Export
{
    public class Export : IExport
    {
        public async Task<bool> ExportToPdf(string text)
        {
            return await Task.Run(() =>
            {
                GlobalFontSettings.UseWindowsFontsUnderWindows = true;

                // Create a new PDF document
                using var document = new PdfDocument();

                document.Info.Title = "Cover letter";

                // todo add PDF/A complaint

                // Create an empty page
                var page = document.AddPage();

                page.TrimMargins.Top = new XUnit(2.54, XGraphicsUnit.Centimeter);
                page.TrimMargins.Left = new XUnit(2.54, XGraphicsUnit.Centimeter);
                page.TrimMargins.Bottom = new XUnit(2.54, XGraphicsUnit.Centimeter);
                page.TrimMargins.Right = new XUnit(2.54, XGraphicsUnit.Centimeter);

                page.Height = page.Height - page.TrimMargins.Top - page.TrimMargins.Bottom;
                page.Width = page.Width - page.TrimMargins.Left - page.TrimMargins.Right;

                //PdfGraphics.DrawString

                // Get an XGraphics object for drawing
                using var gfx = XGraphics.FromPdfPage(page);

                // Create a font
                var font = new XFont("Arial", 14);

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

                // Draw the text
                tf.DrawString(text, font, XBrushes.Black,
                    new XRect(0, 0, page.Width.Point, page.Height.Point),
                    XStringFormats.TopLeft);

                // Save the document...
                const string filename = "Cover letter.pdf";
                var path = Path.Combine(Environment.CurrentDirectory, filename);

                try
                {
                    document.Save(path);

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