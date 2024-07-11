using Repository.Models;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROJECT
{
    /// <summary>
    /// Interaction logic for AddNewBooking.xaml
    /// </summary>
    public partial class AddNewBooking : Window
    {
        
        public AddNewBooking(Room room)
        {
            InitializeComponent();
            txtRoomNumber.Text = room.RoomId.ToString();
            txtPrice.Text = room.Price.ToString();

            //Black out dates from yesterday and earlier
            dpStartDate.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            dpEndDate.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var roomNumber = Convert.ToInt32(txtRoomNumber.Text);
            var guestName = txtGuestName.Text;
            var price = Convert.ToDouble(txtPrice.Text);
            if (string.IsNullOrEmpty(dpStartDate.Text) || string.IsNullOrEmpty(dpEndDate.Text) ||
                string.IsNullOrEmpty(txtGuestName.Text))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            if (!DateOnly.TryParse(dpStartDate.Text, out DateOnly startDate) || 
                !DateOnly.TryParse(dpEndDate.Text, out DateOnly endDate))
            {
                MessageBox.Show("Wrong format Date!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return ;
            }

            //Calculate days between two dates
            TimeSpan difference = endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue);
            int daysBetween = difference.Days;
            if (daysBetween < 1) {
                MessageBox.Show("End date must be after start date by one day", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var ConfirmBooking = new ConfirmBooking(roomNumber,startDate,endDate,guestName,price,daysBetween);
            if (ConfirmBooking.ShowDialog() == true){
                DialogResult = true;
                Close();
            }
        }
    }
}
