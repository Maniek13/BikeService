using ToDoApp.ModelViews;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.Views
{
    public sealed partial class ListOfUser : Page
    {
        public ListOfUserViewModel viewModel { get; set; }

        public ListOfUser()
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
