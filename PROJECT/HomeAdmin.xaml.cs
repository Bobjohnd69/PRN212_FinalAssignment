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
            listService.ItemsSource = listService;

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
            /* if (ListRoom.SelectedItem != null)
            {
                Room selected = (Room)ListRoom.SelectedItem;
              
            }*/
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

        }

        private void btnRoomDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRoomSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        // control tab 3



    }
}