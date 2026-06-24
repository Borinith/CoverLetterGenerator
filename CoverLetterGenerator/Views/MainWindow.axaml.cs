using Avalonia.Controls;
using CoverLetterGenerator.ViewModels;
using System;

namespace CoverLetterGenerator.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            (DataContext as MainWindowViewModel)?.ReactiveViewModel.Dispose();
        }
    }
}