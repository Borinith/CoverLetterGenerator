using System.Threading.Tasks;

namespace CoverLetterGenerator.Export
{
    public interface IExport
    {
        Task ExportToPdf(string text);
    }
}