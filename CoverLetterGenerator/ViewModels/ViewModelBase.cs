using ReactiveUI;

namespace CoverLetterGenerator.ViewModels
{
    public class ViewModelBase : ReactiveObject, IActivatableViewModel
    {
        public ViewModelActivator Activator { get; } = new();
    }
}