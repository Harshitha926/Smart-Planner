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

        public TaskManagerViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            TasksView = CollectionViewSource.GetDefaultView(Tasks);
            TasksView.SortDescriptions.Add(new SortDescription(nameof(TaskItem.Time), ListSortDirection.Ascending));
            TasksView.SortDescriptions.Add(new SortDescription(nameof(TaskItem.PriorityOrder), ListSortDirection.Ascending));
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
            var sorted = Tasks.OrderBy(item => item.Time).ThenBy(item => item.PriorityOrder).ToList();
            if (!sorted.Any())
            {
                return;
            }

            DateTime earliest = sorted.First().Time;
            if (earliest.TimeOfDay < TimeSpan.Zero)
            {
                earliest = DateTime.Today;
            }

            DateTime currentEnd = sorted[0].Time;
            for (var index = 1; index < sorted.Count; index++)
            {
                var next = sorted[index];
                if (next.Time < currentEnd)
                {
                    next.Time = currentEnd;
                }

                currentEnd = next.EndTime;
            }

            Tasks.Clear();
            foreach (var task in sorted)
            {
                Tasks.Add(task);
            }

            RefreshScheduleView();
            RefreshActiveTask(DateTime.Now);
        }

        public void RefreshActiveTask(DateTime now)
        {
            var active = Tasks.FirstOrDefault(task => now >= task.Time && now < task.EndTime);
            foreach (var task in Tasks)
            {
                task.IsActive = task == active;
            }

            ActiveTask = active;
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
