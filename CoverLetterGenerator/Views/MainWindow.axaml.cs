using CoverLetterGenerator.ViewModels;
using ReactiveUI.Avalonia;
using System.Diagnostics.CodeAnalysis;

namespace CoverLetterGenerator.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        [UnconditionalSuppressMessage("Trimming", "IL2026",
            Justification =
                "ReactiveUI.Avalonia ReactiveWindow activation uses reflection for expression-based member chains. " +
                "Required members are preserved by compiled XAML bindings and the trimmed/AOT build is smoke-tested.")]
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}