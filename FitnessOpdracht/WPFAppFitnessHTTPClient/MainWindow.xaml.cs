using Newtonsoft.Json;
using System.Net.Http;
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
using WPFAppFitnessHTTPClient.Model;
using WPFAppFitnessHTTPClient.Services;

namespace WPFAppFitnessHTTPClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Reservation reservation;
        private FitnessService fitnessService;
        public MainWindow()
        {
            InitializeComponent();
            fitnessService = new FitnessService();
            reservation = new();
            this.Loaded += async (s, e) => await LoadEquipmentAsync();
        }

        public async Task LoadEquipmentAsync()
        {
            try
            {
                var equipmentList = await fitnessService.GeefLijstEquipment($"/api/Equipment/Available");
                var equipmentDisplayList = equipmentList.Select(e => new
                {
                    Id = e.Id,
                    DisplayText = $"{e.DeviceType} ID: {e.Id}"  
                }).ToList();

                EquipmentComboBox.ItemsSource = equipmentDisplayList;
                EquipmentComboBox.DisplayMemberPath = "DisplayText"; 
                EquipmentComboBox.SelectedValuePath = "Id";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading equipment: {ex.Message}");
            }
        }

        private async void btnVindTimeSlot_Click(object sender, RoutedEventArgs e)
        {
            var selectedEquipment = EquipmentComboBox.SelectedItem as dynamic;
            if (selectedEquipment == null)
            {
                MessageBox.Show("Please select an equipment.");
                return;
            }
            int equipmentId = selectedEquipment.Id;

            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Now;
            string path = "/OngebruikteTimeslots";

            var timeSlots = await fitnessService.GetAvailableTimeSlots(path, equipmentId, selectedDate);

            if (timeSlots != null && timeSlots.Any())
            {
                TimeSlotComboBox.ItemsSource = timeSlots;
            }
            else
            {
                MessageBox.Show("No unused time slots available for the selected date.");
            }
        }


        private void btnAddTimeSlot_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var selectedEquipment = EquipmentComboBox.SelectedItem as dynamic;
                if (selectedEquipment == null)
                {
                    MessageBox.Show("Please select an equipment.");
                    return;
                }

                int equipmentId = selectedEquipment.Id;

                var selectedTimeSlot = TimeSlotComboBox.SelectedItem as TimeSlot;
                if (selectedTimeSlot == null)
                {
                    MessageBox.Show("Please select a time slot.");
                    return;
                }

                var reservationTimeSlot = new ReservationTimeSlot
                {
                    Equipment = new Equipment
                    {
                        Id = equipmentId,
                        DeviceType = selectedEquipment.DisplayText
                    },
                    TimeSlot = selectedTimeSlot
                };

                reservation.AddTimeSlot(reservationTimeSlot.TimeSlot, reservationTimeSlot.Equipment);

                MessageBox.Show("Time slot added to reservation.");

                UpdateReservationDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnMakeReservation_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MemberTextBox.Text) || !int.TryParse(MemberTextBox.Text, out int memberId))
            {
                MessageBox.Show("Please enter a valid Member ID.");
                return;
            }

            string memberPath = $"/Member/{memberId}";
            Member member = await fitnessService.GetMember(memberPath);

            if (member == null)
            {
                MessageBox.Show("Member not found.");
                return;
            }

            
            reservation.Member = member;
            reservation.Date = (DateTime)DatePicker.SelectedDate;
            string reservationPath = "/ReservationVoegtoe";
            bool success = await fitnessService.SchrijfReservatieAsync(reservationPath, reservation);

            if (success)
            {
                MessageBox.Show("Reservation successfully created!");
                reservation = new Reservation(); 
                UpdateReservationDisplay(); 
            }
            else
            {
                MessageBox.Show("Failed to create the reservation.");
            }
        }


        private void btnRemoveTimeSlot_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservationTimeSlot = TimeSlotListBox.SelectedItem as ReservationTimeSlot;

            if (selectedReservationTimeSlot == null)
            {
                MessageBox.Show("Please select a time slot to remove.");
                return;
            }
            reservation.RemoveTimeSlot( selectedReservationTimeSlot.TimeSlot, selectedReservationTimeSlot.Equipment);


            UpdateReservationDisplay();

            MessageBox.Show("Time slot removed from reservation.");
        }
        private void UpdateReservationDisplay()
        {
            TimeSlotListBox.ItemsSource = null;
            TimeSlotListBox.ItemsSource = reservation.ReservationTimeSlot;
        }
    }
}