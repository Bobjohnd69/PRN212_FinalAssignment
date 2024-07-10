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

namespace PROJECT
{
    /// <summary>
    /// Interaction logic for HomeCustomer.xaml
    /// </summary>
    public partial class HomeCustomer : Window
    {
        public HomeCustomer()
        {
            InitializeComponent();
            Init();
        }

        //tab control 1
        private void Init()
        {
            // tab control1 
           /* int id = CustomerSession.SessionCustomer.CustomerId;
            Customer cus = context.Customers.FirstOrDefault(p => p.CustomerId == id);
            if (cus != null)
            {
                txtProfileEmail.Text = cus.EmailAddress;
                txtProfileName.Text = cus.CustomerFullName;
                txtProfileId.Text = cus.CustomerId.ToString();
                txtProfilePhone.Text = cus.Telephone;
                if (DateTime.TryParse(cus.CustomerBirthday.ToString(), out DateTime birthday))
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
                MessageBox.Show("Customer not found!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }


       */

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
            /*CustomerSession.ClearSession();
            this.Close();*/
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


    }
}


