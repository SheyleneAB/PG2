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
        private List<ReservationTimeSlot> reservationSlots;
        private FitnessService fitnessService;
        public MainWindow()
        {
            InitializeComponent();
            fitnessService = new FitnessService();
            reservationSlots = new List<ReservationTimeSlot>(); 
            this.Loaded += async (s, e) => await LoadEquipmentAsync();
        }

        public async Task LoadEquipmentAsync()
        {
            try
            {
                var equipmentList = await fitnessService.GeefLijstEquipment($"/Equipment/GetAllAvailableEquipment");
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

            reservationSlots.Add(reservationTimeSlot); 

            MessageBox.Show("Time slot added to reservation.");

            UpdateReservationDisplay();
        }

        private async void btnMakeReservation_Click(object sender, RoutedEventArgs e)
        {
            // Validate Member ID
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

            // Validate Reservation Time Slots
            if (reservationSlots == null || !reservationSlots.Any())
            {
                MessageBox.Show("Please add at least one time slot to the reservation.");
                return;
            }

            // Create Reservation Object
            Reservation reservation = new Reservation
            {
                Member = member,
                Date = DateTime.Now,
                ReservationTimeSlots = reservationSlots
            };

            // Send Reservation to API
            string reservationPath = "/ReservationVoegtoe";
            bool success = await fitnessService.SchrijfReservatieAsync(reservationPath, reservation);

            if (success)
            {
                MessageBox.Show("Reservation successfully created!");
                reservationSlots.Clear(); // Clear the time slots list
                UpdateReservationDisplay(); // Refresh the UI
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
            reservationSlots.Remove(selectedReservationTimeSlot);


            UpdateReservationDisplay();

            MessageBox.Show("Time slot removed from reservation.");
        }
        private void UpdateReservationDisplay()
        {
            TimeSlotListBox.ItemsSource = null;
            TimeSlotListBox.ItemsSource = reservationSlots;
        }
    }
}