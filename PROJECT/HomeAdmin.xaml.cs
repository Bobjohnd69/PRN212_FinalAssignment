using Microsoft.EntityFrameworkCore;
using Repo;
using Repo.Hotel;
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
    public partial class HomeAdmin : Window
    {
        private HotelContext context;
        public HomeAdmin(HotelContext _context)
        {
            InitializeComponent();
            context = _context;
            pageLoad();

        }

        private void pageLoad()
        {
            //tab 1
            ListCustomer.ItemsSource = context.Customers.ToList();
            //tab 2
            cmbRoomType.ItemsSource = context.RoomTypes.ToList();
            cmbRoomType.DisplayMemberPath = "RoomTypeName";
            cmbRoomType.SelectedValuePath = "RoomTypeId";
            ListRoom.ItemsSource = context.RoomInformations
            .Select(p => new Room
            {
                RoomId = p.RoomId,
                RoomNumber = p.RoomNumber,
                RoomDetailDescription = p.RoomDetailDescription,
                RoomMaxCapacity = p.RoomMaxCapacity,
                RoomStatus = p.RoomStatus,
                RoomPricePerDay = p.RoomPricePerDay,
                RoomTypeId = p.RoomTypeId,
                RoomTypeName = p.RoomType.RoomTypeName,
            })
            .ToList();
            ListRoom.DataContext = ListRoom.ItemsSource;

        }

        //btnlogout
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerSession.ClearSession();
            this.Close();
        }

        //tabcontrol 1
        private void btnCustomerAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cus = new Customer();
                if (DateOnly.TryParse(txtCustomerBirth.Text, out DateOnly datetime))
                {

                }
                else
                {
                    MessageBox.Show("Wrong format Date!", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (Byte.TryParse(txtCustomerStatus.Text, out byte status))
                {
                }
                else
                {
                    MessageBox.Show("Wrong format Status!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                cus.CustomerFullName = txtCustomerFullName.Text;
                cus.Telephone = txtCustomerTelePhone.Text;
                cus.EmailAddress = txtCustomerEmail.Text;
                cus.CustomerBirthday = datetime;
                cus.CustomerStatus = Convert.ToByte(txtCustomerStatus.Text);
                cus.Password = txtCustomerPass.Text;
                context.Customers.Add(cus);
                context.SaveChanges();
                MessageBox.Show("Add successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                clearCustomer();
                pageLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add fail! Error: Clear before add new cusstomer.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCustomerUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cus = context.Customers.FirstOrDefault(p => p.CustomerId == Convert.ToInt32(txtCustomerID.Text));
                if (Byte.TryParse(txtCustomerStatus.Text, out byte status))
                {
                }
                else
                {
                    MessageBox.Show("Wrong format Status!", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (DateOnly.TryParse(txtCustomerBirth.Text, out DateOnly datetime))
                {
                }
                else
                {
                    MessageBox.Show("Wrong format Date!", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                cus.CustomerFullName = txtCustomerFullName.Text;
                cus.Telephone = txtCustomerTelePhone.Text;
                cus.CustomerBirthday = datetime;
                cus.EmailAddress = txtCustomerEmail.Text;
                cus.Password = txtCustomerPass.Text;
                context.Customers.Update(cus);
                context.SaveChanges();
                MessageBox.Show("Update successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                clearCustomer();
                pageLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update fail!", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCustomerDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cus = context.Customers.FirstOrDefault(p => p.CustomerId == Convert.ToInt32(txtCustomerID.Text));
                context.Customers.Remove(cus);
                context.SaveChanges();
                MessageBox.Show("Delete successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                clearCustomer();
                pageLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete fail!", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCustomerClear_Click(object sender, RoutedEventArgs e)
        {
            clearCustomer();
        }
        private void clearCustomer()
        {
            //txtCustomerID.Text = string.Empty;
            //txtCustomerFullName.Text = string.Empty;
            //txtCustomerTelePhone.Text = string.Empty;
            //txtCustomerEmail.Text = string.Empty;
            //txtCustomerStatus.Text = string.Empty;
            //txtCustomerPass.Text = string.Empty;
            //txtCustomerPass.Text = string.Empty;
            ListCustomer.SelectedIndex = -1;
        }

        private void btnCustomerSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtCustomerSearch.Text;
            ListCustomer.ItemsSource = context.Customers
                .Where(p => p.CustomerFullName.Contains(search))
                .ToList();
        }

        //tab control 2

        private void btnRoomClear_Click(object sender, RoutedEventArgs e)
        {
            clearRoom();
        }

        private void clearRoom()
        {
            txtRoomId.Text = string.Empty;
            txtRoomNumber.Text = string.Empty;
            txtRoomDetail.Text = string.Empty;
            txtRoomCapacity.Text = string.Empty;
            txtRoomStatus.Text = string.Empty;
            txtRoomPrice.Text = string.Empty;
            ListRoom.SelectedIndex = -1;
            cmbRoomType.SelectedIndex = -1;
        }

        private void btnRoomDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cus = context.RoomInformations.FirstOrDefault(p => p.RoomId == Convert.ToInt32(txtRoomId.Text));
                context.RoomInformations.Remove(cus);
                context.SaveChanges();
                MessageBox.Show("Delete successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                clearRoom();
                pageLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete fail!", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRoomUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var rom = context.RoomInformations.FirstOrDefault(p => p.RoomId == Convert.ToInt32(txtRoomId.Text));
                rom.RoomNumber = txtRoomNumber.Text;
                rom.RoomDetailDescription = txtRoomDetail.Text;
                rom.RoomMaxCapacity = Convert.ToInt32(txtRoomCapacity.Text);
                rom.RoomTypeId = Convert.ToInt32(cmbRoomType.SelectedValue);
                rom.RoomStatus = Convert.ToByte(txtRoomStatus.Text);
                rom.RoomPricePerDay = Convert.ToDecimal(txtRoomPrice.Text);
                context.RoomInformations.Update(rom);
                context.SaveChanges();
                MessageBox.Show("Update successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                clearCustomer();
                pageLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update fail!", "Admin", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListRoom.SelectedItem != null)
            {
                Room selected = (Room)ListRoom.SelectedItem;
                txtRoomId.Text = selected.RoomId.ToString();
                txtRoomNumber.Text = selected.RoomNumber;
                txtRoomDetail.Text = selected.RoomDetailDescription;
                txtRoomCapacity.Text = selected.RoomMaxCapacity.ToString();
                txtRoomStatus.Text = selected.RoomStatus.ToString();
                txtRoomPrice.Text = selected.RoomPricePerDay.ToString();
                cmbRoomType.SelectedIndex = checkSelectedComboBox(selected.RoomTypeName.ToString());
            }
        }

        private int checkSelectedComboBox(string roomname)
        {
            for (int i = 0; i < cmbRoomType.Items.Count; i++)
            {
                var comboBoxItem = cmbRoomType.Items[i] as RoomType;
                if (comboBoxItem != null && comboBoxItem.RoomTypeName.Equals(roomname))
                {
                    return i;
                }
            }
            return -1;
        }

        private void btnRoomSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtRoomrSearch.Text;
            ListRoom.ItemsSource = context.RoomInformations
             .Where(p => p.RoomNumber.Contains(search))
            .Select(p => new Room
            {
                RoomId = p.RoomId,
                RoomNumber = p.RoomNumber,
                RoomDetailDescription = p.RoomDetailDescription,
                RoomMaxCapacity = p.RoomMaxCapacity,
                RoomStatus = p.RoomStatus,
                RoomPricePerDay = p.RoomPricePerDay,
                RoomTypeId = p.RoomTypeId,
                RoomTypeName = p.RoomType.RoomTypeName,
            })
            .ToList();
        }

        private void btnRoomAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var rom = new RoomInformation();
                rom.RoomNumber = txtRoomNumber.Text;
                rom.RoomDetailDescription = txtRoomDetail.Text;
                rom.RoomMaxCapacity = Convert.ToInt32(txtRoomCapacity.Text);
                rom.RoomTypeId = Convert.ToInt32(cmbRoomType.SelectedValue);
                rom.RoomStatus = Convert.ToByte(txtRoomStatus.Text);
                rom.RoomPricePerDay = Convert.ToDecimal(txtRoomPrice.Text);
                context.RoomInformations.Add(rom);
                context.SaveChanges();
                MessageBox.Show("Add successful", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                clearCustomer();
                pageLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add fail! Error: Clear before add new Room.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
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
                CustomerSession.ClearSession();
            }
        }
    }
}

