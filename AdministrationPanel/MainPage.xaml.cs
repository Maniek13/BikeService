using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToDoApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.ListOfUser));
        }
    }
}
