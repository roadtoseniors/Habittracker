using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp7
{
    public partial class MainWindow : Window
    {
        // Словарь для хранения пользователей (в реальном приложении используйте базу данных)
        private Dictionary<string, string> users = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик кнопки "Login"
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

        // Обработчик кнопки "Register"
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

            if (users.ContainsKey(username))
            {
                RegisterMessage.Text = "Username already exists.";
                return;
            }

            // Добавление нового пользователя
            users[username] = password;
            RegisterMessage.Text = "Успешная регистрация. Перейдите на вкладку входа";
        }
    }
}
