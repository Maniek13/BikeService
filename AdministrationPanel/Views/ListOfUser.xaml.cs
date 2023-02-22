using ToDoApp.ModelViews;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.Views
{
    internal sealed partial class ListOfUser : Page
    {
        internal ListOfUserViewModel viewModel { get; set; }
        internal ListOfUser()
        {
            InitializeComponent();
            viewModel = new ListOfUserViewModel();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowContent(MainContent ,typeof(AddUserView));
        }
    }
}
