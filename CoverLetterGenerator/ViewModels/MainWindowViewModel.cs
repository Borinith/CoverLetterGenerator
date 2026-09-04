using CoverLetterGenerator.Data;
using CoverLetterGenerator.Export;
using CoverLetterGenerator.Models;
using ReactiveUI;
using ReactiveUI.Primitives;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using DefaultExport = CoverLetterGenerator.Export.Export;

namespace CoverLetterGenerator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDataDefault _dataDefault;
        private readonly IExport _export;
        private string _exportButtonText = "Export to PDF";
        private bool _isExportEnabled = true;
        private bool _isUniversity = true;
        private Position _selectedPosition;

        // Parameterless constructor for the XAML previewer (Design.DataContext). Not used at runtime,
        // where the DI container picks the constructor below.
        public MainWindowViewModel() : this(new DataDefault(), new DefaultExport())
        {
        }

        [UnconditionalSuppressMessage("Trimming", "IL2026",
            Justification =
                "ReactiveUI WhenAnyValue reads the observed properties via reflection over expression trees. Those " +
                "properties (SelectedPosition.Name, IsUniversity, SkillViewModel.IsChecked) are also referenced by " +
                "compiled XAML bindings and CoverLetterText, so they survive trimming. The AOT build is smoke-tested.")]
        public MainWindowViewModel(IDataDefault dataDefault, IExport export)
        {
            _dataDefault = dataDefault;
            _export = export;

            Positions = _dataDefault.Positions;
            SelectedPosition = Positions.First();

            Skills = _dataDefault.Skills
                .Select(x => new SkillViewModel(x.Name, x.IsChecked))
                .ToArray();

#pragma warning disable DF0001
            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(o => o.SelectedPosition.Name)
                    .Subscribe(_ => this.RaisePropertyChanged(nameof(CoverLetterText)))
                    .DisposeWith(disposables);

                this.WhenAnyValue(o => o.IsUniversity)
                    .Subscribe(_ => this.RaisePropertyChanged(nameof(CoverLetterText)))
                    .DisposeWith(disposables);

                foreach (var skill in Skills)
                {
                    skill.WhenAnyValue(s => s.IsChecked)
                        .Subscribe(_ => this.RaisePropertyChanged(nameof(CoverLetterText)))
                        .DisposeWith(disposables);
                }
            });
#pragma warning restore DF0001
        }

        public List<Position> Positions { get; }

        public Position SelectedPosition
        {
            get => _selectedPosition;
            set => this.RaiseAndSetIfChanged(ref _selectedPosition, value);
        }

        public SkillViewModel[] Skills { get; }

        public bool IsUniversity
        {
            get => _isUniversity;
            set => this.RaiseAndSetIfChanged(ref _isUniversity, value);
        }

        public bool IsExportEnabled
        {
            get => _isExportEnabled;
            set => this.RaiseAndSetIfChanged(ref _isExportEnabled, value);
        }

        public string ExportButtonText
        {
            get => _exportButtonText;
            set => this.RaiseAndSetIfChanged(ref _exportButtonText, value);
        }

        public byte ColumnCount => _dataDefault.ColumnCount;

        public string CoverLetterText => _dataDefault.GenerateCoverLetterText(
            SelectedPosition.Name,
            Skills.Where(x => x.IsChecked).Select(x => x.Name).ToArray(),
            IsUniversity);

        public async Task ExportToPdfButton()
        {
            const int timeOut = 3000;

            IsExportEnabled = false;
            var isSavedSuccessfully = await _export.ExportToPdfAsync(CoverLetterText, "Cover letter.pdf", "Cover letter", "Borinith");
            ExportButtonText = isSavedSuccessfully ? "Exported!" : "Error!";

            await Task.Delay(timeOut);

            IsExportEnabled = true;
            ExportButtonText = "Export to PDF";
        }
    }
}