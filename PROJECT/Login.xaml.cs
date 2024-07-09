using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repo.Hotel;
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
        private HotelContext _context;
        public Login(HotelContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void txtLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmailAddress.Text;
            string password = txtPassWord.Password;
            Customer customer = new Customer();
            customer = (Customer)_context.Customers
                .Where(p => p.EmailAddress.Equals(email) && p.Password.Equals(password))
                .FirstOrDefault();
            bool isAdmin = checkAdmin(email, password);
            if (email.Equals("") || password.Equals(""))
            {
                MessageBox.Show("Please input your Email Address or Password!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (isAdmin)
            {
                MessageBox.Show("Welcome Admin!", "Infomation", MessageBoxButton.OK, MessageBoxImage.Information);
                clearInfo();
                HomeAdmin home = new HomeAdmin(_context);
                home.ShowDialog();
            }
            else if (customer != null)
            {
                MessageBox.Show("Welcome to our hotel!", "Infomation", MessageBoxButton.OK, MessageBoxImage.Information);
                clearInfo();
                CustomerSession.SetSessionCustomer(customer);
                HomeCustomer home = new HomeCustomer(_context);
                home.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong Email Address or Password, please try again!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool checkAdmin(string email, string password)
        {
            // Đọc tệp appsettings.json và tạo Configuration object
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            // Lấy thông tin tài khoản admin từ Configuration object

            string adminEmail = configuration.GetSection("AdminAccount")["Email"];

            string adminPassword = configuration.GetSection("AdminAccount")["Password"];

            if (adminEmail.Equals(email) && adminPassword.Equals(password))
            {
                return true;
            }
            return false;
        }

        private void clearInfo()
        {
            txtEmailAddress.Clear();
            txtPassWord.Clear();
        }

        private void txtCancel_Click(object sender, RoutedEventArgs e)
        {
            clearInfo();
        }
    }
}
