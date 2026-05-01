# Smart Planner

A modern, feature-rich WPF desktop application for task management and scheduling with real-time reminders.

## Features

### 🧠 Smart Scheduling System
- Automatically generates daily schedules from user-added tasks
- Sorts tasks by priority (High → Medium → Low) and duration
- Allocates time slots dynamically starting from a configurable start time (default: 8 AM)
- Prevents overlapping tasks with intelligent adjustment

### 📅 Visual Schedule Page
- Clean timeline view showing the full daily schedule
- Time blocks display task names with priority-based color coding:
  - 🔴 Red: High priority
  - 🟠 Orange: Medium priority
  - 🟢 Green: Low priority
- Highlights the current active task
- Shows task completion status

### ✅ Task Completion & Progress Tracking
- Mark tasks as completed with a single click
- Real-time progress bar showing completion percentage
- Visual indicators for completed vs. pending tasks
- Summary statistics on the schedule page

### ⏰ Alarm & Reminder System
- Real-time task notifications using DispatcherTimer
- Popup window with task details when time starts
- Sound alerts for reminders
- "Done" and "Snooze (5 min)" options

### 🎨 Modern UI Design
- Dark theme with clean, minimal aesthetic
- Sidebar navigation between Task Input and Schedule View
- Card-style UI with rounded corners and shadows
- Hover effects and smooth transitions
- Consistent Segoe UI font family
- Responsive layout

### 🔍 Filtering & Sorting
- Filter tasks by priority level
- Sort by time, priority, or completion status
- Real-time updates as you change filters

### ⚙️ Technical Features
- MVVM architecture for clean separation of concerns
- ObservableCollection for automatic UI updates
- Data binding throughout the application
- Custom value converters for UI formatting
- Page navigation with smooth animations

## Getting Started

1. Open `SmartPlanner.sln` in Visual Studio
2. Build and run the application
3. Add tasks using the Task Input page
4. Generate your daily schedule
5. View and manage your schedule on the Visual Schedule page

## Architecture

- **Models**: `TaskItem`, `PriorityLevel`
- **ViewModels**: `AppViewModel`, `TaskManagerViewModel`, `TaskInputViewModel`
- **Views**: `MainWindow`, `TaskInputPage`, `VisualSchedulePage`, `AlarmWindow`
- **Converters**: Priority colors, task counts, boolean formatting

## Key Classes

### TaskItem
Represents a single task with properties:
- Name, Time, Duration, Priority
- IsActive, IsCompleted status
- Computed properties for display formatting

### TaskManagerViewModel
Manages the collection of tasks:
- Add, remove, and complete tasks
- Generate optimized schedules
- Track active tasks and progress
- Handle alarm notifications

### TaskInputViewModel
Handles user input for task creation:
- Form validation and data binding
- Filtering and sorting controls
- Commands for task operations

## UI Themes

The application uses a dark color palette:
- Background: `#FF1F2937`
- Cards: `#FF374151`
- Text: `#FFF9FAFB`
- Accents: `#FF3B82F6` (blue), `#FF10B981` (green), etc.

## Requirements

- .NET Framework 4.8
- Windows Presentation Foundation (WPF)
- Visual Studio 2019 or later

## Future Enhancements

- Save/load schedules to/from file
- Recurring tasks
- Calendar integration
- Custom sound files for alarms
- Multiple schedule templates
- Task categories and tags