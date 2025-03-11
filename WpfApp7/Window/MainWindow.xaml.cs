using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Windows;

namespace WpfApp7
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _reposUsers = new userRepository()
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginUsername.Text;
            string password = LoginPassword.Password;

            if (users.ContainsKey(username) && users[username] == password)
            {
                LoginMessage.Text = "Login successful!";
                // Здесь можно открыть новое окно или выполнить другие действия
                TableHabits tableHabits = new TableHabits();
                tableHabits.Show();
                this.Close();
            }
            else
            {
                LoginMessage.Text = "Invalid username or password.";
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = RegisterUsername.Text;
            string password = RegisterPassword.Password;
            string confirmPassword = RegisterConfirmPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                RegisterMessage.Text = "Username and password cannot be empty.";
                return;
            }

            if (password != confirmPassword)
            {
                RegisterMessage.Text = "Passwords do not match.";
                return;
            }
            Random random = new Random();

            users user = new users
            {
                id = random.Next(1, 52),
                name = username,
                password = password,
            }

            userRepository userRepo = new userRepository();
            users.addUser(user);

            RegisterMessage.Text = "Успешная регистрация. Перейдите на вкладку входа";
        }
    }
}
