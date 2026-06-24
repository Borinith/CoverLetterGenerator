using ReactiveUI;

namespace CoverLetterGenerator.ViewModels
{
    public class SkillViewModel : ReactiveObject
    {
        private bool _isChecked;

        public SkillViewModel(string name, bool isChecked)
        {
            Name = name;
            _isChecked = isChecked;
        }

        public string Name { get; }

        public bool IsChecked
        {
            get => _isChecked;
            set => this.RaiseAndSetIfChanged(ref _isChecked, value);
        }
    }
}