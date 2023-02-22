using ToDoApp.Models;
using ToDoApp.ModelViews;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.Views
{
    public sealed partial class AddUserView : Page
    {
        public UserViewModel viewModel { get; set; }
        public AddUserView()
        {
            InitializeComponent();
            viewModel = new UserViewModel();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            User user = new User()
            {
                Login = loginInput.Text,
                Password = paswordInput.Text

            };

            viewModel.User = user;
            viewModel.AddUser(user);

        }
    }
}
