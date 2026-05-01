using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SmartPlanner.Models;

namespace SmartPlanner.ViewModels
{
    public class TaskInputViewModel : INotifyPropertyChanged
    {
        private string _newTaskName;
        private DateTime _newTaskDate;
        private string _newTaskTimeText;
        private int _newTaskDuration;
        private string _selectedPriority;

        public TaskManagerViewModel TaskManager { get; }
        public ObservableCollection<TaskItem> Tasks => TaskManager.Tasks;
        public ObservableCollection<string> PriorityOptions { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }
        public ICommand GenerateScheduleCommand { get; }

        public string NewTaskName
        {
            get => _newTaskName;
            set
            {
                if (_newTaskName == value) return;
                _newTaskName = value;
                OnPropertyChanged(nameof(NewTaskName));
            }
        }

        public DateTime NewTaskDate
        {
            get => _newTaskDate;
            set
            {
                if (_newTaskDate == value) return;
                _newTaskDate = value;
                OnPropertyChanged(nameof(NewTaskDate));
            }
        }

        public string NewTaskTimeText
        {
            get => _newTaskTimeText;
            set
            {
                if (_newTaskTimeText == value) return;
                _newTaskTimeText = value;
                OnPropertyChanged(nameof(NewTaskTimeText));
            }
        }

        public int NewTaskDuration
        {
            get => _newTaskDuration;
            set
            {
                if (_newTaskDuration == value) return;
                _newTaskDuration = value;
                OnPropertyChanged(nameof(NewTaskDuration));
            }
        }

        public string SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                if (_selectedPriority == value) return;
                _selectedPriority = value;
                OnPropertyChanged(nameof(SelectedPriority));
            }
        }

        public TaskInputViewModel(TaskManagerViewModel taskManager)
        {
            TaskManager = taskManager;
            PriorityOptions = new ObservableCollection<string> { "High", "Medium", "Low" };
            AddTaskCommand = new RelayCommand(_ => AddTask());
            RemoveTaskCommand = new RelayCommand(param => RemoveTask(param as TaskItem));
            GenerateScheduleCommand = new RelayCommand(_ => GenerateSchedule());

            NewTaskDate = DateTime.Today;
            NewTaskTimeText = DateTime.Now.ToString("HH:mm");
            NewTaskDuration = 30;
            SelectedPriority = "Medium";
        }

        public void AddTask()
        {
            if (string.IsNullOrWhiteSpace(NewTaskName))
            {
                MessageBox.Show("Please enter a task name.", "Add Task", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TimeSpan.TryParse(NewTaskTimeText, out var timeOfDay))
            {
                MessageBox.Show("Please enter a valid time value like 14:30.", "Add Task", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (NewTaskDuration <= 0)
            {
                MessageBox.Show("Duration must be greater than zero.", "Add Task", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Enum.TryParse(SelectedPriority, out PriorityLevel priority))
            {
                priority = PriorityLevel.Medium;
            }

            var taskTime = NewTaskDate.Date + timeOfDay;
            TaskManager.AddTask(NewTaskName.Trim(), taskTime, NewTaskDuration, priority);

            NewTaskName = string.Empty;
            NewTaskDuration = 30;
            NewTaskTimeText = DateTime.Now.ToString("HH:mm");
        }

        public void GenerateSchedule()
        {
            TaskManager.GenerateSchedule();
            MessageBox.Show("Schedule generated and optimized for overlapping tasks.", "Schedule Created", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void RemoveTask(TaskItem task)
        {
            TaskManager.RemoveTask(task);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
