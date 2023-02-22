using Newtonsoft.Json;
using ToDoApp.Models;
using ToDoApp.ModelViews;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ToDoApp.Views
{
    internal sealed partial class EditUserView : Page
    {
        internal UserViewModel viewModel { get; set; }
        internal EditUserView()
        {
            InitializeComponent();
            viewModel = new UserViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel.User = e.Parameter as User;
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(loginInput.Text))
            {
                viewModel.User.Login = loginInput.Text;
            }
            if(!string.IsNullOrEmpty(paswordInput.Text))
            {
                viewModel.User.Password = paswordInput.Text;
            }
            viewModel.EditUser();
        }
    }
}
