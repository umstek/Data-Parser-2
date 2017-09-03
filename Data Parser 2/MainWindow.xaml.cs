using System;
using System.Reactive.Linq;
using System.Windows;

namespace Data_Parser_2
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            Console.WriteLine(results.Count);
            //MessageBox.Show($"{results.Count} results fetched. ");
            progressBarSearch.IsIndeterminate = false;
        }
    }
}