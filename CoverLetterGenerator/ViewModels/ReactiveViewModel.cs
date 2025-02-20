using Avalonia.Controls;
using Avalonia.Interactivity;
using CoverLetterGenerator.Data;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoverLetterGenerator.ViewModels
{
    public class ReactiveViewModel : ReactiveObject
    {
        private string? _name;

        public ReactiveViewModel()
        {
            Positions = DataDefault.Positions;

            foreach (var skill in DataDefault.Skills)
            {
                var checkBox = new CheckBox
                {
                    Content = skill.Name,
                    IsChecked = skill.IsChecked
                };

                checkBox.IsCheckedChanged += OnSkillsChanged;
                Skills.Add(checkBox);
            }

#pragma warning disable DF0001
            this.WhenAnyValue(o => o.Name)!
                .Subscribe(new Action<object>(_ => this.RaisePropertyChanged(nameof(Greeting))));
#pragma warning restore DF0001
        }

        public string[] Positions { get; }

        public List<CheckBox> Skills { get; set; } = [];

        public static byte ColumnCount => DataDefault.COLUMN_COUNT;

        public string? Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public int Checked => Skills.Count(x => x.IsChecked!.Value);

        public string Greeting =>
            string.IsNullOrWhiteSpace(Name)
                ? "Hello World from Avalonia.Samples"
                : $"Hello {Name}";

        private void OnSkillsChanged(object? sender, RoutedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(Checked));
        }
    }
}