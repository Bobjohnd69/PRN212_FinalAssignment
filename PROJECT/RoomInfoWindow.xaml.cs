using Repository.Models;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PROJECT
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RoomInfoWindow : Window
    {
        RoomServices roomService;
        RoomServiceServices roomServiceService;
        private int currentRoomId;
        public RoomInfoWindow()
        {
            InitializeComponent();
        }

        public void LoadRoomDetails(int roomId, string roomDetails, List<Service> services)
        {
            txtRoomDetails.Text = $"Room ID: {roomId}\nDetails: {roomDetails}";
            ListViewServices.ItemsSource = services;
            currentRoomId = roomId;
        }

        private void CallService_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string serviceIdString = button.Tag.ToString();

            if (Guid.TryParse(serviceIdString, out Guid serviceId))
            {
                InsertRoomService(serviceId);
            }
            else
            {
                MessageBox.Show("Invalid service ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InsertRoomService(Guid serviceId)
        {
            try
            {
                roomServiceService = new RoomServiceServices();
                var service = ((List<Service>)ListViewServices.ItemsSource).FirstOrDefault(s => s.ServiceId == serviceId);

                if (service != null)
                {
                    var roomService = new RoomService
                    {
                        RoomId = currentRoomId,
                        ServiceId = service.ServiceId,
                        ServiceName = service.ServiceName,
                        Status = 1 // Assuming 1 means 'Not Completed' or 'Pending'
                    };

                    roomServiceService.Add(roomService);
                    roomServiceService.Save();
                    MessageBox.Show("Service requested successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Service not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to request service: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
