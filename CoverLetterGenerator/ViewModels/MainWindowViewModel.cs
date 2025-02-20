namespace CoverLetterGenerator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactiveViewModel ReactiveViewModel { get; } = new();
    }
}