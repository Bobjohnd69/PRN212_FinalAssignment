using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for HomeAdmin.xaml
    /// </summary>
    public partial class HomeAdmin
    {

        UserServices userService;
        RoomServices roomService;
        ServiceServices serviceService;
        public HomeAdmin()
        {
            InitializeComponent();
            userService = new UserServices();
            roomService = new RoomServices();
            serviceService = new ServiceServices();
            pageLoad();
        }

        private void pageLoad()
        {
            //tab 1
            var listUser = userService.GetAll();
            ListUser.ItemsSource = listUser;
            //tab 2
            var listRoom = roomService.GetAll();
            ListRoom.ItemsSource = listRoom;
            //tab 3
            var listService = serviceService.GetAll();
            ListService.ItemsSource = listService;

        }

        //btnlogout
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserSession.ClearSession();
            this.Close();
        }

        //tabcontrol 1

        private void ListUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListUser.SelectedItem != null)
            {
                var selectedUser = ListUser.SelectedItem as User;

            }
        }
        private void btnUserAdd_Click(object sender, RoutedEventArgs e)
        {
            var user = new User();
            ComboBoxItem selectedStatusItem = cmbUserStatus.SelectedItem as ComboBoxItem;
            ComboBoxItem selectedRoleItem = cmbUserRole.SelectedItem as ComboBoxItem;

            try
            {
                if (string.IsNullOrEmpty(txtUserFullName.Text) ||
                    string.IsNullOrEmpty(txtUserPhone.Text) ||
                    string.IsNullOrEmpty(txtUserEmail.Text) ||
                    string.IsNullOrEmpty(txtUserBirthday.Text) ||
                    string.IsNullOrEmpty(txtUserPassword.Text) ||
                    selectedStatusItem == null ||
                    selectedRoleItem == null)
                {
                    MessageBox.Show("All fields must be filled in.", "Admin", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Parse the birthday
                if (!DateOnly.TryParse(txtUserBirthday.Text, out DateOnly datetime))
                {
                    MessageBox.Show("Wrong format Date!", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                user.UserId = Guid.NewGuid();
                user.FullName = txtUserFullName.Text;
                user.Phone = txtUserPhone.Text;
                user.Email = txtUserEmail.Text;
                user.Birthday = datetime;
                user.Status = Convert.ToInt32(selectedStatusItem.Tag);
                user.Role = selectedRoleItem.Content.ToString();
                user.Password = txtUserPassword.Text;

                userService.Add(user);
                MessageBox.Show("Add successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                clearUser();
                pageLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Add fail! Error: {ex.Message}", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnUserUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListUser.SelectedItem == null)
                {
                    MessageBox.Show("Please select a user to update.", "Admin", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var selectedUser = ListUser.SelectedItem as User;
                if (selectedUser == null)
                {
                    MessageBox.Show("Selected user is invalid.", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Validate input fields
                if (string.IsNullOrEmpty(txtUserFullName.Text) ||
                    string.IsNullOrEmpty(txtUserPhone.Text) ||
                    string.IsNullOrEmpty(txtUserEmail.Text) ||
                    string.IsNullOrEmpty(txtUserBirthday.Text) ||
                    string.IsNullOrEmpty(txtUserPassword.Text) ||
                    cmbUserStatus.SelectedItem == null ||
                    cmbUserRole.SelectedItem == null)
                {
                    MessageBox.Show("All fields must be filled in.", "Admin", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Parse the birthday
                if (!DateOnly.TryParse(txtUserBirthday.Text, out DateOnly datetime))
                {
                    MessageBox.Show("Wrong format Date!", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Create a new User object with updated details
                var updatedUser = new User
                {
                    UserId = selectedUser.UserId,
                    FullName = txtUserFullName.Text,
                    Phone = txtUserPhone.Text,
                    Email = txtUserEmail.Text,
                    Birthday = datetime,
                    Role = (cmbUserRole.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Status = Convert.ToInt32((cmbUserStatus.SelectedItem as ComboBoxItem)?.Tag),
                    Password = txtUserPassword.Text
                };

                // Update user using the service method
                userService.Update(updatedUser);
                MessageBox.Show("Update successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                clearUser();
                pageLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update fail! Error: {ex.Message}", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnUserDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListUser.SelectedItem != null)
            {
                var selectedUser = ListUser.SelectedItem as User;
                if (selectedUser != null)
                {
                    try
                    {
                        userService.Delete(selectedUser);
                        MessageBox.Show("Delete successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearUser();
                        pageLoad();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Delete fail! Error: {ex.Message}", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Admin", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnUserClear_Click(object sender, RoutedEventArgs e)
        {
            clearUser();
        }
        private void clearUser()
        {
            ListUser.SelectedItem = null;
            txtUserFullName.Text = string.Empty;
            txtUserPhone.Text = string.Empty;
            txtUserEmail.Text = string.Empty;
            txtUserBirthday.Text = string.Empty;
            cmbUserStatus.SelectedIndex = -1;
            cmbUserRole.SelectedIndex = -1;
            txtUserPassword.Text = string.Empty;
        }

        private void btnUserSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txUserSearch.Text.Trim();
            var userList = userService.SearchByFullName(keyword);
            ListUser.ItemsSource = userList;
        }


        //tab control 2

        private void ListRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListRoom.SelectedItem != null)
            {
                var selectedRoom = ListRoom.SelectedItem as Room;
                if (selectedRoom != null)
                {
                    // Display room details in UI controls
                    txtRoomId.Text = selectedRoom.RoomId.ToString();
                    txtRoomDetail.Text = selectedRoom.RoomDetail;
                    txtRoomCapacity.Text = selectedRoom.RoomCapacity.ToString();
                    txtRoomType.Text = selectedRoom.RoomType;
                    // Set selected value in ComboBox for RoomStatus
                    cmbRoomStatus.SelectedValue = selectedRoom.RoomStatus;

                    // Format decimal value for Price
                    txtRoomPrice.Text = selectedRoom.Price.ToString();
                }
            }
            else
            {
                // Clear UI controls if no room is selected
                clearRoom();
            }
        }


        private void btnRoomClear_Click(object sender, RoutedEventArgs e)
        {
            clearRoom();
        }

        private void clearRoom()
        {
            txtRoomId.Text = string.Empty;
            txtRoomDetail.Text = string.Empty;
            txtRoomCapacity.Text = string.Empty;
            txtRoomType.Text = string.Empty;
            cmbRoomStatus.SelectedIndex = -1;
            txtRoomPrice.Text = string.Empty;
            ListRoom.SelectedItem = null;
        }


        private void btnRoomAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var room = new Room();
                room.RoomId = Convert.ToInt32(txtRoomId.Text);
                room.RoomDetail = txtRoomDetail.Text;
                room.RoomCapacity = Convert.ToInt32(txtRoomCapacity.Text);
                room.RoomType = txtRoomType.Text;

                if (cmbRoomStatus.SelectedItem != null)
                    room.RoomStatus = Convert.ToByte(((ComboBoxItem)cmbRoomStatus.SelectedItem).Tag); // Assuming RoomStatus is byte

                room.Price = Convert.ToDecimal(txtRoomPrice.Text);

                bool roomIdExists = ((List<Room>)ListRoom.ItemsSource).Any(r => r.RoomId == room.RoomId);
                if (roomIdExists)
                {
                    MessageBox.Show($"Room with RoomId '{room.RoomId}' already exists!", "Duplicate RoomId", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    roomService.Add(room);
                }

                MessageBox.Show("Room added successfully!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                //clearRoom();
                pageLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add room. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




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

        private void clearRoomFields()
        {
            txtRoomId.Text = string.Empty;
            txtRoomDetail.Text = string.Empty;
            txtRoomCapacity.Text = string.Empty;
            txtRoomType.Text = string.Empty;
            cmbRoomStatus.SelectedIndex = -1;
            txtRoomPrice.Text = string.Empty;
        }

        private void btnRoomUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ListRoom.SelectedItem != null)
            {
                try
                {
                    var selectedRoom = ListRoom.SelectedItem as Room;
                    if (selectedRoom == null)
                    {
                        MessageBox.Show("Selected room is invalid.", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Validate input fields
                    if (string.IsNullOrEmpty(txtRoomDetail.Text) ||
                        string.IsNullOrEmpty(txtRoomCapacity.Text) ||
                        string.IsNullOrEmpty(txtRoomType.Text) ||
                        string.IsNullOrEmpty(txtRoomPrice.Text) ||
                        cmbRoomStatus.SelectedItem == null)
                    {
                        MessageBox.Show("All fields must be filled in.", "Admin", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Update selected room object
                    selectedRoom.RoomDetail = txtRoomDetail.Text;
                    selectedRoom.RoomCapacity = Convert.ToInt32(txtRoomCapacity.Text);
                    selectedRoom.RoomType = txtRoomType.Text;
                    selectedRoom.RoomStatus = Convert.ToByte(((ComboBoxItem)cmbRoomStatus.SelectedItem).Tag);
                    selectedRoom.Price = Convert.ToDecimal(txtRoomPrice.Text);

                    // Call service method to update room
                    roomService.Update(selectedRoom);
                    MessageBox.Show("Update successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearRoom();
                    pageLoad();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Update fail! Error: {ex.Message}", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a room to update.", "Admin", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRoomDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListRoom.SelectedItem != null)
            {
                var selectedRoom = ListRoom.SelectedItem as Room;
                if (selectedRoom != null)
                {
                    try
                    {
                        roomService.Delete(selectedRoom);
                        MessageBox.Show("Delete successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearRoom();
                        pageLoad();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Delete fail! Error: {ex.Message}", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete.", "Admin", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRoomSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get room ID from user input
                if (!int.TryParse(txtRoomSearch.Text.Trim(), out int roomId))
                {
                    MessageBox.Show("Please enter a valid Room ID.", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Call service method to search for rooms by Room ID
                var rooms = roomService.SearchByRoomID(roomId);

                if (rooms.Count > 0)
                {
                    // Bind search results to ListRoom
                    ListRoom.ItemsSource = rooms;
                }
                else
                {
                    MessageBox.Show("No rooms found with the given Room ID.", "Admin", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Clear ListRoom if no rooms found
                    ListRoom.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching rooms: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // tab 3

        private void ListService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListService.SelectedItem is Service selectedService)
            {
                txtServiceName.Text = selectedService.ServiceName;
            }
        }


        private void btnServiceAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServiceName.Text))
            {
                MessageBox.Show("Service Name cannot be empty.", "Admin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var service = new Service
            {
                ServiceId = Guid.NewGuid(),
                ServiceName = txtServiceName.Text
            };
            serviceService.Add(service);
            MessageBox.Show("Add successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            pageLoad();
        }
        private void btnServiceUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServiceName.Text))
            {
                MessageBox.Show("Service Name cannot be empty.", "Admin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ListService.SelectedItem is Service selectedService)
            {
                selectedService.ServiceName = txtServiceName.Text;
                serviceService.Update(selectedService);
                pageLoad();
                MessageBox.Show("Update Success.", "Admin", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a service to update.", "Admin", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnServiceDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListService.SelectedItem is Service selectedService)
            {
                serviceService.Delete(selectedService);
                pageLoad();
                MessageBox.Show("Delete successful", "Admin", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a service to delete.", "Admin", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnServiceClear_Click(object sender, RoutedEventArgs e)
        {
            txtServiceName.Clear();
            ListService.SelectedItem = null;
        }

        private void btnServiceSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtServicerSearch.Text.Trim();
            var services = serviceService.SearchServices(searchTerm);
            ListService.ItemsSource = services;
        }

    }
}