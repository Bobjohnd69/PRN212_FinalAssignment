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
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Services;

namespace PROJECT
{
    /// <summary>
    /// Interaction logic for HomeCustomer.xaml
    /// </summary>
    public partial class HomeStaff : Window
    {
        UserServices userService;
        RoomServices roomService;
        BookedServices bookedService;
        RoomServiceServices roomServiceService;
        public HomeStaff()
        {
            InitializeComponent();
            userService = new UserServices();
            roomService = new RoomServices();
            bookedService = new BookedServices();
            roomServiceService = new RoomServiceServices();
            InitProfile();
            Load();
        }

        public void Delete(Guid roomId, Guid serviceId)
        {
            var service = _context.RoomServices.FirstOrDefault(rs => rs.RoomId == roomId && rs.ServiceId == serviceId);
            if (service != null)
            {
                _context.RoomServices.Remove(service);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Service not found");
            }
        }


        private void Load()
        {
            var listRoomService = roomServiceService.GetAll();
            RoomServiceDataGrid.ItemsSource = listRoomService;
            var listRoom = roomService.GetAll();
            RoomDataGrid.ItemsSource = listRoom;
            var listBooked = bookedService.GetAll();
            CheckoutDataGrid.ItemsSource = listBooked;
        }
        private void InitProfile()
        {
            Guid userID = UserSession.SessionUser.UserId;
            User user = userService.GetByUserID(userID);
            if (user != null)
            {
                txtProfileEmail.Text = user.Email;
                txtProfileName.Text = user.FullName;
                txtProfilePhone.Text = user.Phone;
                if (DateTime.TryParse(user.Birthday.ToString(), out DateTime birthday))
                {
                    dtProfileDate.SelectedDate = birthday;
                }
                else
                {
                    MessageBox.Show("Loading Date Fail!!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("User not found!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnProfileUpdate_Click(object sender, RoutedEventArgs e)
        {
           /* try
            {
                int id = CustomerSession.SessionCustomer.CustomerId;
                DateOnly.TryParse(dtProfileDate.Text, out DateOnly birthday);
                Customer cus = context.Customers.FirstOrDefault(p => p.CustomerId == id);
                cus.Telephone = txtProfilePhone.Text;
                cus.CustomerFullName = txtProfileName.Text;
                cus.CustomerBirthday = birthday;
                context.Customers.Update(cus);
                context.SaveChanges();
                CustomerSession.SetSessionCustomer(cus);
                MessageBox.Show("Update successfully!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Fail!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        //btnlogout
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            UserSession.ClearSession();
            this.Close();
        }



        //tab control 3
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        // win dow
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure to quit?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                UserSession.ClearSession();
            }
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the selected service from the DataGrid
                var button = sender as Button;
                if (button == null)
                {
                    MessageBox.Show("Button is null!");
                    return;
                }

                var selectedService = button.DataContext as RoomService;
                if (selectedService != null)
                {
                    // Delete the selected service from the database
                    roomServiceService.Delete(selectedService.RoomId, selectedService.ServiceId);
                    MessageBox.Show("Service resolved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Reload the data grid to reflect the changes
                    Load();
                }
                else
                {
                    MessageBox.Show("Service not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to resolve service: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}


