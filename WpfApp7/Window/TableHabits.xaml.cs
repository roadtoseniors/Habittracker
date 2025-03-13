using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp7
{
    /// <summary>
    /// Логика взаимодействия для TableHabits.xaml
    /// </summary>
    public partial class TableHabits : Window
    {
        private readonly DatabaseService _dbService;
        private readonly string _username;

        public ObservableCollection<Habit> Habits { get; set; }

        public TableHabits(string username)
        {
            InitializeComponent();

            _username = username;
            string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=HabitTracker";
            _dbService = new DatabaseService(connectionString);

            Habits = new ObservableCollection<Habit>();
            lstHabits.ItemsSource = Habits;

            LoadHabits();
        }

        private void LoadHabits()
        {
            // Получаем ID пользователя
            int? userId = _dbService.GetUserId(_username);

            if (userId.HasValue)
            {
                // Получаем привычки для текущего пользователя
                var habits = _dbService.GetHabits(userId.Value);
                foreach (var habit in habits)
                {
                    Habits.Add(habit);
                }
            }
            else
            {
                MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddHabit_Click(object sender, RoutedEventArgs e)
        {
            string habitName = txtNewHabit.Text.Trim();
            string habitDescription = txtHabitDescription.Text.Trim();

            if (!string.IsNullOrEmpty(habitName))
            {
                // Получаем ID пользователя
                int? userId = _dbService.GetUserId(_username);

                if (userId.HasValue)
                {
                    _dbService.AddHabit(userId.Value, habitName, habitDescription);
                    Habits.Add(new Habit
                    {
                        Name = habitName,
                        Description = habitDescription,
                        CompletionDate = null
                    });
                    txtNewHabit.Clear();
                    txtHabitDescription.Clear();
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    public class Habit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}