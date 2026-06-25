using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CoverLetterGenerator.ViewModels;
using CoverLetterGenerator.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CoverLetterGenerator
{
    public class App : Application
    {
        private ServiceProvider? _services;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // Register all the services needed for the application to run
            var collection = new ServiceCollection();
            collection.AddCommonServices();

            // The container lives for the whole application. It owns the singletons and the
            // transient MainWindowViewModel (IDisposable); disposing it on exit disposes the
            // view model and its subscriptions in turn.
            _services = collection.BuildServiceProvider();

            // The view model is an IDisposable assigned to DataContext. DisposableFixer (DF0033)
            // can't see that the container created above owns it and disposes it on Exit, so the
            // warning is a false positive at this composition root.
#pragma warning disable DF0033
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = _services.GetRequiredService<MainWindowViewModel>()
                };

                desktop.Exit += (_, _) => _services?.Dispose();
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new Control
                {
                    DataContext = _services.GetRequiredService<MainWindowViewModel>()
                };
            }
#pragma warning restore DF0033

            base.OnFrameworkInitializationCompleted();
        }
    }
}