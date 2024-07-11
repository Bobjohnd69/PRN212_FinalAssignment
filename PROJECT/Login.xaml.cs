using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Models;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Login.xaml
    /// </summary>



    public partial class Login : Window
    {
        UserServices userService;
        RoomServices roomService;
        ServiceServices service;
        public Login()
        {
            InitializeComponent();
            userService = new UserServices();
        }

        private void txtLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmailAddress.Text;
            string password = txtPassWord.Password;
            User user = userService.GetByEmailAndPassword(email, password);
            if (email.Equals("") || password.Equals(""))
            {
                MessageBox.Show("Please input your Email Address or Password!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(user == null)
            {
                MessageBox.Show("Wrong Email Address or Password, please try again!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user.Role.Equals("Admin"))
            {
                MessageBox.Show("Welcome Admin!", "Infomation", MessageBoxButton.OK, MessageBoxImage.Information);
                clearInfo();
                HomeAdmin home = new HomeAdmin();
                this.Close();
                home.ShowDialog();
            }
            else if (user != null)
            {
                MessageBox.Show("Welcome back " + user.FullName, "Infomation", MessageBoxButton.OK, MessageBoxImage.Information);
                clearInfo();
                UserSession.SetSessionUser(user);
                HomeStaff home = new HomeStaff();
                this.Close();
                home.ShowDialog();
            }
        }

        private void clearInfo()
        {
            txtEmailAddress.Clear();
            txtPassWord.Clear();
            txtRoomNumber.Clear();

        }

        private void txtCancel_Click(object sender, RoutedEventArgs e)
        {
            clearInfo();
        }

        private void txtRoom_Click(object sender, RoutedEventArgs e)
        {
            roomService = new RoomServices();
            service = new ServiceServices();
            var listRoom = roomService.GetAll();
            if(txtRoomNumber.Text.Length <= 0)
            {
                MessageBox.Show("Room id cannot be null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int roomId = Convert.ToInt32(txtRoomNumber.Text);
                var room = listRoom.FirstOrDefault(r => r.RoomId == roomId);

                if (room != null)
                {
                    RoomInfoWindow roomInfoWindow = new RoomInfoWindow();
                    roomInfoWindow.LoadRoomDetails(room.RoomId, room.RoomDetail, service.GetAll());
                    roomInfoWindow.Show();

                    // Close the login window after showing the RoomInfoWindow
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Room not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }
    }
}
