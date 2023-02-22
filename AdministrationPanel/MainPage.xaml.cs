using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToDoApp
{
    internal sealed partial class MainPage : Page
    {
        internal MainPage()
        {
            this.InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.ListOfUser));
        }
    }
}
