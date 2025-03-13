﻿using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp7
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseService _dbService;

        public MainWindow()
        {
            InitializeComponent();

            // Укажите строку подключения к вашей базе данных PostgreSQL
            string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=HabitTracker";
            _dbService = new DatabaseService(connectionString);
        }

        // Обработчик кнопки "Login"
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

            try
            {
                _dbService.RegisterUser(username, password);
                RegisterMessage.Text = "Успешная регистрация. Перейдите на вкладку входа";
            }
            catch (Exception ex)
            {
                RegisterMessage.Text = "Ошибка регистрации: " + ex.Message;
            }
        }
    }
}
