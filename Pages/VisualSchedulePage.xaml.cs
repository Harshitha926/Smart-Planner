using System.Windows;
using System.Windows.Controls;
using SmartPlanner.ViewModels;

namespace SmartPlanner.Pages
{
    public partial class VisualSchedulePage : Page
    {
        public VisualSchedulePage()
        {
            InitializeComponent();
            if (Application.Current.Resources["AppViewModel"] is AppViewModel appViewModel)
            {
                DataContext = appViewModel;
                appViewModel.TaskManager.RefreshActiveTask(System.DateTime.Now);
            }
        }
    }
}
