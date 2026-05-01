using System;
using System.Windows;
using SmartPlanner.Models;

namespace SmartPlanner.Windows
{
    public partial class AlarmWindow : Window
    {
        public TaskItem Task { get; }

        public AlarmWindow(TaskItem task)
        {
            InitializeComponent();
            Task = task;
            DataContext = Task;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Snooze_Click(object sender, RoutedEventArgs e)
        {
            Task.Time = DateTime.Now.AddMinutes(5);
            DialogResult = false;
        }
    }
}
