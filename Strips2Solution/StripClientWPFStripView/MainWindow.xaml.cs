using StripClientWPFStripView.Model;
using StripClientWPFStripView.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StripClientWPFStripView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StripService stripService;
        private Strip strip = new Strip();
        public MainWindow()
        {
            InitializeComponent();
            stripService = new StripService();
            
        }
        private async void StripButton_Click(object sender, RoutedEventArgs e)
        {
            string stid = StripIdTextBox.Text;
            if (int.TryParse(stid, out int id))
            {
                var fetchedStrip = await stripService.GetStripAsync($"api/Strip/{id}");
                if (fetchedStrip != null)
                {
                    strip.Id = fetchedStrip.Id;
                    strip.Titel = fetchedStrip.Titel;
                    strip.Reeksnummer = fetchedStrip.Reeksnummer;
                    strip.Reeks = fetchedStrip.Reeks;
                    strip.Uitgeverij = fetchedStrip.Uitgeverij;
                    strip.Auteurs = fetchedStrip.Auteurs;
                    this.DataContext = fetchedStrip;
                }
                else
                {
                    MessageBox.Show("Strip not found.");
                }
            }
            else
            {
                MessageBox.Show("Invalid Strip ID");
            }
        }
    }
}