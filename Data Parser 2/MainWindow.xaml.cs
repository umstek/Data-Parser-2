using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using Data_Parser_2.Api;
using Data_Parser_2.Serialization;
using Microsoft.Win32;

namespace Data_Parser_2
{
    public partial class MainWindow
    {
        private List<Result> _results;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonSearch_ClickAsync(object sender, RoutedEventArgs e)
        {
            progressBarSearch.IsIndeterminate = true;

            var results = await
                Api.Api.FetchAllNearbyPlaces(textBoxLocation.Text, int.Parse(textBoxRadius.Text),
                    textBoxTypes.Text);
            _results = results;

            progressBarSearch.IsIndeterminate = false;

            MessageBox.Show(this,
                $"{results.Count} results fetched. \n" +
                "Click \"search\" again to try again. \n" +
                "Click \"Save to CSV\" to save results. ",
                "Data Parser 2", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (_results == null || !_results.Any()) return;

            var dialog = new SaveFileDialog
            {
                OverwritePrompt = true,
                DefaultExt = ".csv",
                AddExtension = true,
                FileName = $"{textBoxLocation.Text}-{textBoxTypes.Text}-{textBoxRadius.Text}"
            };

            if (dialog.ShowDialog() ?? false)
                File.WriteAllText(dialog.FileName, CsvSerializer.EncodeResults(_results), Encoding.UTF8);
        }
    }
}