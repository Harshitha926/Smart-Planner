using System;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Threading;
using SmartPlanner.Models;
using SmartPlanner.ViewModels;

namespace SmartPlanner
{
    public partial class MainWindow : Window
    {
        private readonly TaskManagerViewModel _taskManager;
        private readonly DispatcherTimer _alarmTimer;
        private TaskItem _lastAlarmTask;

        public MainWindow()
        {
            InitializeComponent();

            if (Application.Current.Resources["AppViewModel"] is AppViewModel appViewModel)
            {
                DataContext = appViewModel;
                _taskManager = appViewModel.TaskManager;
            }
            else
            {
                _taskManager = new TaskManagerViewModel();
                DataContext = new AppViewModel(_taskManager);
            }

            MainFrame.Navigate(new Pages.TaskInputPage());

            _alarmTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _alarmTimer.Tick += AlarmTimer_Tick;
            _alarmTimer.Start();
        }

        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            _taskManager.RefreshActiveTask(DateTime.Now);

            var activeTask = _taskManager.ActiveTask;
            if (activeTask != null && activeTask != _lastAlarmTask)
            {
                _lastAlarmTask = activeTask;
                ShowAlarm(activeTask);
            }
            else if (activeTask == null)
            {
                _lastAlarmTask = null;
            }
        }

        private void ShowAlarm(TaskItem task)
        {
            SystemSounds.Exclamation.Play();

            var alarmWindow = new Windows.AlarmWindow(task)
            {
                Owner = this
            };

            var result = alarmWindow.ShowDialog();
            if (result == false)
            {
                _taskManager.SnoozeTask(task);
            }
        }

        private void TaskInputButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.TaskInputPage());
        }

        private void VisualScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.VisualSchedulePage());
        }
    }
}
