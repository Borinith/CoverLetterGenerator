using CoverLetterGenerator.Data;

namespace CoverLetterGenerator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IDataDefault dataDefault)
        {
            ReactiveViewModel = new ReactiveViewModel(dataDefault);
        }

        public ReactiveViewModel ReactiveViewModel { get; }
    }
}