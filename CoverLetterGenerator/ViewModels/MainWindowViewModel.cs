using CoverLetterGenerator.Data;
using CoverLetterGenerator.Export;

namespace CoverLetterGenerator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IDataDefault dataDefault, IExport export)
        {
            ReactiveViewModel = new ReactiveViewModel(dataDefault, export);
        }

        public ReactiveViewModel ReactiveViewModel { get; }
    }
}