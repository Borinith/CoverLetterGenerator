using CoverLetterGenerator.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CoverLetterGenerator.Tests
{
    public class ServiceRegistrationTests
    {
        [Test]
        public async Task AddCommonServices_Resolves_MainWindowViewModel()
        {
            var services = new ServiceCollection();
            services.AddCommonServices();
            using var provider = services.BuildServiceProvider();
            using var vm = provider.GetRequiredService<MainWindowViewModel>();

            await Assert.That(vm.Positions).IsNotEmpty();
        }
    }
}