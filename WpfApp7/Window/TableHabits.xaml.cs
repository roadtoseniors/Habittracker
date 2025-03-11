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
        public ObservableCollection<Habit> Habits { get; set; }
        public TableHabits()
        {
            InitializeComponent();
            Habits = new ObservableCollection<Habit>();
            lstHabits.ItemsSource = Habits;
        }

        private void btnAddHabit_Click(object sender, RoutedEventArgs e)
        {
            string habitName = txtNewHabit.Text.Trim();
            string habitDescription = txtHabitDescription.Text.Trim();

            if (!string.IsNullOrEmpty(habitName))
            {
                Habits.Add(new Habit
                {
                    Name = habitName,
                    Description = habitDescription,
                    CompletionDate = null // По умолчанию дата выполнения не задана
                });
                txtNewHabit.Clear();
                txtHabitDescription.Clear();
            }
        }
    }


    public class Habit
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
