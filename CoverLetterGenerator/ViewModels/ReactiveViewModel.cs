using Avalonia.Controls;
using Avalonia.Interactivity;
using CoverLetterGenerator.Data;
using CoverLetterGenerator.Export;
using CoverLetterGenerator.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoverLetterGenerator.ViewModels
{
    public class ReactiveViewModel : ReactiveObject
    {
        private readonly IDataDefault _dataDefault;
        private readonly IExport _export;

        private Position _selectedPosition;

        public ReactiveViewModel(IDataDefault dataDefault, IExport export)
        {
            _dataDefault = dataDefault;
            _export = export;

            Positions = _dataDefault.Positions;
            SelectedPosition = Positions.First();

            foreach (var skill in _dataDefault.Skills)
            {
                var checkBox = new CheckBox
                {
                    Content = skill.Name,
                    IsChecked = skill.IsChecked
                };

                checkBox.IsCheckedChanged += OnCheckBoxesChanged;
                Skills.Add(checkBox);
            }

            University = new CheckBox
            {
                Content = "University?",
                IsChecked = true
            };

            University.IsCheckedChanged += OnCheckBoxesChanged;

            ExportToPdf = new Button
            {
                Content = "Export to PDF"
            };

#pragma warning disable DF0001
            this.WhenAnyValue(o => o.SelectedPosition.Name)
                .Subscribe(new Action<object>(_ => this.RaisePropertyChanged(nameof(CoverLetterText))));
#pragma warning restore DF0001
        }

        public List<Position> Positions { get; }

        public Position SelectedPosition
        {
            get => _selectedPosition;
            set => this.RaiseAndSetIfChanged(ref _selectedPosition, value);
        }

        public List<CheckBox> Skills { get; set; } = [];

        public CheckBox University { get; }

        public Button ExportToPdf { get; }

        public byte ColumnCount => _dataDefault.ColumnCount;

        public string CoverLetterText => _dataDefault.GenerateCoverLetterText(
            SelectedPosition.Name,
            Skills.Where(x => x.IsChecked!.Value).Select(x => x.Content!.ToString()!).ToList(),
            University.IsChecked!.Value);

        private void OnCheckBoxesChanged(object? sender, RoutedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(CoverLetterText));
        }

        public async Task ExportToPdfButton()
        {
            const int timeOut = 3000;

            ExportToPdf.IsEnabled = false;
            var isSavedSuccessfully = await _export.ExportToPdf(CoverLetterText);
            ExportToPdf.Content = isSavedSuccessfully ? "Exported!" : "Error!";

            await Task.Delay(timeOut);
            ExportToPdf.IsEnabled = true;
            ExportToPdf.Content = "Export to PDF";
        }
    }
}