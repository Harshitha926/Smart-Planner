using System.Windows;
using System.Windows.Controls;
using SmartPlanner.ViewModels;

namespace SmartPlanner.Pages
{
    public partial class TaskInputPage : Page
    {
        public TaskInputPage()
        {
            InitializeComponent();
            if (Application.Current.Resources["AppViewModel"] is AppViewModel appViewModel)
            {
                DataContext = appViewModel.TaskInput;
            }
        }
    }
}
