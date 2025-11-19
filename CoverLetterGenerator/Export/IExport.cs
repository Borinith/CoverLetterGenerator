using System.Threading.Tasks;

namespace CoverLetterGenerator.Export
{
    public interface IExport
    {
        Task<bool> ExportToPdfAsync(string text);
    }
}