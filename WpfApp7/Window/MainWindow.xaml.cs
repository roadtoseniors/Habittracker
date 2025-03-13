using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Windows;

namespace WpfApp7
{
    public partial class MainWindow : Window
    {
<<<<<<< HEAD:WpfApp7/MainWindow.xaml.cs
        private readonly DatabaseService _dbService;

        public MainWindow()
        {
            InitializeComponent();

            // Укажите строку подключения к вашей базе данных PostgreSQL
            string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=HabitTracker";
            _dbService = new DatabaseService(connectionString);
=======
        public MainWindow()
        {
            InitializeComponent();
            _reposUsers = new userRepository()
>>>>>>> 15b7bcb41dc786ef4380798b27df4e3a1af3be39:WpfApp7/Window/MainWindow.xaml.cs
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginUsername.Text;
            string password = LoginPassword.Password;

            if (_dbService.LoginUser(username, password))
            {
                LoginMessage.Text = "Login successful!";
                TableHabits tableHabits = new TableHabits(username);
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

<<<<<<< HEAD:WpfApp7/MainWindow.xaml.cs
            try
            {
                _dbService.RegisterUser(username, password);
                RegisterMessage.Text = "Успешная регистрация. Перейдите на вкладку входа";
            }
            catch (Exception ex)
            {
                RegisterMessage.Text = "Ошибка регистрации: " + ex.Message;
            }
=======
            users user = new users
            {
                id = random.Next(1, 52),
                name = username,
                password = password,
            }

            userRepository userRepo = new userRepository();
            users.addUser(user);

            RegisterMessage.Text = "Успешная регистрация. Перейдите на вкладку входа";
>>>>>>> 15b7bcb41dc786ef4380798b27df4e3a1af3be39:WpfApp7/Window/MainWindow.xaml.cs
        }
    }
}
