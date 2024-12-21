using CoverLetterGenerator.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CoverLetterGenerator
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection collection)
        {
            collection.AddTransient<MainWindowViewModel>();
        }
    }
}