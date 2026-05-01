using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using SmartPlanner.Models;

namespace SmartPlanner.ViewModels
{
    public class TaskManagerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskItem> Tasks { get; }
        public ICollectionView TasksView { get; }

        private TaskItem _activeTask;
        private DateTime _scheduleStartTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0); // Default 8 AM

        public TaskItem ActiveTask
        {
            get => _activeTask;
            private set
            {
                if (_activeTask == value)
                {
                    return;
                }

                _activeTask = value;
                OnPropertyChanged(nameof(ActiveTask));
            }
        }

        public DateTime ScheduleStartTime
        {
            get => _scheduleStartTime;
            set
            {
                if (_scheduleStartTime == value) return;
                _scheduleStartTime = value;
                OnPropertyChanged(nameof(ScheduleStartTime));
            }
        }

        public double ProgressPercentage => Tasks.Count > 0 ? (double)Tasks.Count(t => t.IsCompleted) / Tasks.Count * 100 : 0;

        public string ProgressText => $"{Tasks.Count(t => t.IsCompleted)} / {Tasks.Count} tasks completed";

        public TaskManagerViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.SortDescriptions.Add(new SortDescription(nameof(TaskItem.Time), ListSortDirection.Ascending));
            TasksView.SortDescriptions.Add(new SortDescription(nameof(TaskItem.PriorityOrder), ListSortDirection.Ascending));

            Tasks.CollectionChanged += (s, e) => OnPropertyChanged(nameof(ProgressPercentage));
        }

        public void AddTask(string name, DateTime time, int duration, PriorityLevel priority)
        {
            var task = new TaskItem(name, time, duration, priority);
            Tasks.Add(task);
            RefreshScheduleView();
            RefreshActiveTask(DateTime.Now);
        }

        public void RemoveTask(TaskItem task)
        {
            if (task == null)
            {
                return;
            }

            Tasks.Remove(task);
            RefreshScheduleView();
            RefreshActiveTask(DateTime.Now);
        }

        public void GenerateSchedule()
        {
            var sorted = Tasks
                .Where(t => !t.IsCompleted)
                .OrderBy(item => item.PriorityOrder)
                .ThenBy(item => item.DurationMinutes)
                .ToList();

            if (!sorted.Any())
            {
                return;
            }

            DateTime currentTime = ScheduleStartTime;
            foreach (var task in sorted)
            {
                task.Time = currentTime;
                currentTime = task.EndTime;
            }

            RefreshScheduleView();
            RefreshActiveTask(DateTime.Now);
        }

        public void RefreshActiveTask(DateTime now)
        {
            var active = Tasks.FirstOrDefault(task => !task.IsCompleted && now >= task.Time && now < task.EndTime);
            foreach (var task in Tasks)
            {
                task.IsActive = task == active;
            }

            ActiveTask = active;
        }

        public void MarkTaskCompleted(TaskItem task)
        {
            if (task == null) return;
            task.IsCompleted = true;
            OnPropertyChanged(nameof(ProgressPercentage));
            OnPropertyChanged(nameof(ProgressText));
            RefreshActiveTask(DateTime.Now);
        }

        public void SnoozeTask(TaskItem task)
        {
            if (task == null)
            {
                return;
            }

            task.Time = DateTime.Now.AddMinutes(5);
            RefreshScheduleView();
            RefreshActiveTask(DateTime.Now);
        }

        private void RefreshScheduleView()
        {
            TasksView.Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
