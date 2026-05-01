using System;
using System.Media;
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

            // Play alarm sound
            try
            {
                SystemSounds.Exclamation.Play();
                // For a more prominent sound, you could load a custom WAV file:
                // var player = new SoundPlayer("alarm.wav");
                // player.Play();
            }
            catch
            {
                // Fallback if sound fails
            }
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
