using SmartPlanner.Models;

namespace SmartPlanner.ViewModels
{
    public class AppViewModel
    {
        public TaskManagerViewModel TaskManager { get; }
        public TaskInputViewModel TaskInput { get; }

        public AppViewModel()
        {
            TaskManager = new TaskManagerViewModel();
            TaskInput = new TaskInputViewModel(TaskManager);
        }

        public AppViewModel(TaskManagerViewModel taskManager)
        {
            TaskManager = taskManager;
            TaskInput = new TaskInputViewModel(taskManager);
        }
    }
}
