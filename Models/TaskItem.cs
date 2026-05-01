using System;
using System.ComponentModel;

namespace SmartPlanner.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string _name;
        private DateTime _time;
        private int _durationMinutes;
        private PriorityLevel _priority;
        private bool _isActive;
        private bool _isCompleted;

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public DateTime Time
        {
            get => _time;
            set
            {
                if (_time == value) return;
                _time = value;
                OnPropertyChanged(nameof(Time));
                OnPropertyChanged(nameof(DisplayTime));
                OnPropertyChanged(nameof(EndTime));
            }
        }

        public int DurationMinutes
        {
            get => _durationMinutes;
            set
            {
                if (_durationMinutes == value) return;
                _durationMinutes = value;
                OnPropertyChanged(nameof(DurationMinutes));
                OnPropertyChanged(nameof(DurationLabel));
                OnPropertyChanged(nameof(EndTime));
            }
        }

        public PriorityLevel Priority
        {
            get => _priority;
            set
            {
                if (_priority == value) return;
                _priority = value;
                OnPropertyChanged(nameof(Priority));
                OnPropertyChanged(nameof(PriorityLabel));
            }
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive == value) return;
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted == value) return;
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        public DateTime EndTime => Time.AddMinutes(DurationMinutes);

        public int PriorityOrder => Priority switch
        {
            PriorityLevel.High => 1,
            PriorityLevel.Medium => 2,
            PriorityLevel.Low => 3,
            _ => 3
        };

        public string DisplayTime => Time.ToString("hh:mm tt");
        public string DurationLabel => $"{DurationMinutes} min";
        public string PriorityLabel => Priority.ToString();

        public TaskItem(string name, DateTime time, int durationMinutes, PriorityLevel priority)
        {
            _name = name;
            _time = time;
            _durationMinutes = durationMinutes;
            _priority = priority;
            _isActive = false;
            _isCompleted = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
