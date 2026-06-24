using ReactiveUI.Builder;
using System.Runtime.CompilerServices;

namespace CoverLetterGenerator.Tests
{
    internal static class ReactiveUiTestInitializer
    {
        // ReactiveUI 23.x requires explicit initialization before WhenAnyValue works.
        // The app does this via UseReactiveUI in Program.cs; tests need their own one-time init.
        [ModuleInitializer]
        internal static void Initialize()
        {
            RxAppBuilder.CreateReactiveUIBuilder()
                .WithCoreServices()
                .BuildApp();
        }
    }
}