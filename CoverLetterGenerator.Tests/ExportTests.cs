using System;
using System.IO;
using System.Threading.Tasks;
using ExportService = CoverLetterGenerator.Export.Export;

namespace CoverLetterGenerator.Tests
{
    public class ExportTests
    {
        [Test]
        public async Task ExportToPdfAsync_Writes_A_Pdf_File()
        {
            var export = new ExportService();
            var filename = $"export-test-{Guid.NewGuid():N}.pdf";
            var path = Path.Combine(Environment.CurrentDirectory, filename);

            try
            {
                var isSavedSuccessfully = await export.ExportToPdfAsync("Hello!\nThank you", filename);

                await Assert.That(isSavedSuccessfully).IsTrue();
                await Assert.That(File.Exists(path)).IsTrue();
            }
            finally
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}