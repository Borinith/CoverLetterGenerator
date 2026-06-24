using CoverLetterGenerator.Data;
using CoverLetterGenerator.Export;
using CoverLetterGenerator.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace CoverLetterGenerator.ViewModels
{
    public class ReactiveViewModel : ReactiveObject, IDisposable
    {
        private readonly IDataDefault _dataDefault;
        private readonly IExport _export;
        private readonly CompositeDisposable _subscriptions = new();

        private string _exportButtonText = "Export to PDF";
        private bool _isExportEnabled = true;
        private bool _isUniversity = true;
        private Position _selectedPosition;

        public ReactiveViewModel(IDataDefault dataDefault, IExport export)
        {
            _dataDefault = dataDefault;
            _export = export;

            Positions = _dataDefault.Positions;
            SelectedPosition = Positions.First();

            Skills = _dataDefault.Skills
                .Select(x => new SkillViewModel(x.Name, x.IsChecked))
                .ToList();

            _subscriptions.Add(this.WhenAnyValue(o => o.SelectedPosition.Name)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(CoverLetterText))));

            _subscriptions.Add(this.WhenAnyValue(o => o.IsUniversity)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(CoverLetterText))));

            foreach (var skill in Skills)
            {
                _subscriptions.Add(skill.WhenAnyValue(s => s.IsChecked)
                    .Subscribe(_ => this.RaisePropertyChanged(nameof(CoverLetterText))));
            }
        }

        public List<Position> Positions { get; }

        public Position SelectedPosition
        {
            get => _selectedPosition;
            set => this.RaiseAndSetIfChanged(ref _selectedPosition, value);
        }

        public List<SkillViewModel> Skills { get; }

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
            Skills.Where(x => x.IsChecked).Select(x => x.Name).ToList(),
            IsUniversity);

        public async Task ExportToPdfButton()
        {
            const int timeOut = 3000;

            IsExportEnabled = false;
            var isSavedSuccessfully = await _export.ExportToPdfAsync(CoverLetterText);
            ExportButtonText = isSavedSuccessfully ? "Exported!" : "Error!";

            await Task.Delay(timeOut);

            IsExportEnabled = true;
            ExportButtonText = "Export to PDF";
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}