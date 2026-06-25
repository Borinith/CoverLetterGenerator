using CoverLetterGenerator.Data;
using CoverLetterGenerator.Export;
using CoverLetterGenerator.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoverLetterGenerator.Tests
{
    public class MainWindowViewModelTests
    {
        private static MainWindowViewModel CreateViewModel()
        {
            return new MainWindowViewModel(new DataDefault(), new FakeExport());
        }

        [Test]
        public async Task CoverLetterText_Reflects_Selected_Position()
        {
            using var vm = CreateViewModel();
            vm.SelectedPosition = vm.Positions[1];

            await Assert.That(vm.CoverLetterText).Contains(vm.Positions[1].Name);
        }

        [Test]
        public async Task CoverLetterText_Lists_A_Newly_Checked_Skill()
        {
            using var vm = CreateViewModel();
            var skill = vm.Skills.First(s => s.Name == "PostgreSQL");

            skill.IsChecked = true;

            await Assert.That(vm.CoverLetterText).Contains("PostgreSQL");
        }

        [Test]
        public async Task CoverLetterText_Omits_University_Sentence_When_Disabled()
        {
            using var vm = CreateViewModel();
            vm.IsUniversity = false;

            await Assert.That(vm.CoverLetterText).DoesNotContain("graduated from university");
        }

        [Test]
        public async Task Changing_Position_Raises_CoverLetterText_Changed()
        {
            using var vm = CreateViewModel();
            var raised = RaisesCoverLetterTextChanged(vm, () => vm.SelectedPosition = vm.Positions[1]);

            await Assert.That(raised).IsTrue();
        }

        [Test]
        public async Task Toggling_A_Skill_Raises_CoverLetterText_Changed()
        {
            using var vm = CreateViewModel();
            var raised = RaisesCoverLetterTextChanged(vm, () => vm.Skills[0].IsChecked = !vm.Skills[0].IsChecked);

            await Assert.That(raised).IsTrue();
        }

        [Test]
        public async Task Toggling_University_Raises_CoverLetterText_Changed()
        {
            using var vm = CreateViewModel();
            var raised = RaisesCoverLetterTextChanged(vm, () => vm.IsUniversity = !vm.IsUniversity);

            await Assert.That(raised).IsTrue();
        }

        private static bool RaisesCoverLetterTextChanged(MainWindowViewModel vm, Action change)
        {
            var raised = false;

            vm.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(MainWindowViewModel.CoverLetterText))
                {
                    raised = true;
                }
            };

            change();

            return raised;
        }

        private sealed class FakeExport : IExport
        {
            public async Task<bool> ExportToPdfAsync(string text, string filename, string title = "", string author = "")
            {
                return await Task.FromResult(true);
            }
        }
    }
}