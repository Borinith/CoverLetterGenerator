using Avalonia.Controls;
using Avalonia.Interactivity;
using CoverLetterGenerator.Data;
using CoverLetterGenerator.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoverLetterGenerator.ViewModels
{
    public class ReactiveViewModel : ReactiveObject
    {
        private Position _selectedPosition;

        public ReactiveViewModel()
        {
            Positions = DataDefault.Positions;
            SelectedPosition = Positions.First();

            foreach (var skill in DataDefault.Skills)
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

        public static byte ColumnCount => DataDefault.COLUMN_COUNT;

        public string CoverLetterText => DataDefault.GenerateCoverLetterText(
            SelectedPosition.Name,
            Skills.Where(x => x.IsChecked!.Value).Select(x => x.Content!.ToString())!,
            University.IsChecked!.Value);

        private void OnCheckBoxesChanged(object? sender, RoutedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(CoverLetterText));
        }
    }
}