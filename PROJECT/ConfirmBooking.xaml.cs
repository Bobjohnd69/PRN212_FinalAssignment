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
    /// Interaction logic for ConfirmBooking.xaml
    /// </summary>
    public partial class ConfirmBooking : Window
    {
        BookedServices bookedService;
        Booked NewBooked { get; set; }
        public ConfirmBooking(  int roomNumber, DateOnly startDate, DateOnly endDate, 
                                String guestName, double price, int daysBetween)
        {
            InitializeComponent();
            bookedService = new BookedServices();
            NewBooked = new Booked();
            Load(roomNumber,startDate,endDate,guestName,price,daysBetween);
        }
        
        private void Load(int roomNumber, DateOnly startDate, DateOnly endDate, String guestName,double price,int daysBetween)
        {
            txtRoomNumber.Text = roomNumber.ToString();
            txtStartDate.Text = startDate.ToString();
            txtEndDate.Text = endDate.ToString();
            txtGuestName.Text = guestName.ToString();
            txtPrice.Text = price.ToString();
            txtTotalCost.Text = calculateTotalCost(price,daysBetween).ToString();
        }

        private double calculateTotalCost(double price, int daysBetween)
        {
           double totalCost = (double) (price * daysBetween);
            return totalCost;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            NewBooked.BookedId = Guid.NewGuid();
            NewBooked.RoomNumber = Convert.ToInt32(txtRoomNumber.Text);
            NewBooked.GuestName = txtGuestName.Text;
            NewBooked.StartDate = DateOnly.Parse(txtStartDate.Text);
            NewBooked.EndDate = DateOnly.Parse(txtEndDate.Text); 
            NewBooked.Cost = Convert.ToDecimal(txtTotalCost.Text);
            NewBooked.Status = 1;

            DialogResult = true;
            bookedService.Add(NewBooked);
            Close();
        }
    }
}
