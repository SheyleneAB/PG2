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
                Strip strip = await stripService.GetStripAsync($"api/Strip/{id}");
                SelectedStripTextBox.Text = strip.ToString();
            }
            else
            {
                MessageBox.Show("Invalid Strip ID");
            }
        }
    }
}