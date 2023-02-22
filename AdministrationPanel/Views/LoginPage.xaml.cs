using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.Views
{
    internal sealed partial class LoginPage : Page
    {
        internal LoginPage()
        {
            this.InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.ListOfUser));
        }
    }
}
