using System;
using System.Collections.ObjectModel;
using ToDoApp.Controller;
using ToDoApp.HelperClasses;
using ToDoApp.Models;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.ModelViews
{
    public class ListOfUserViewModel : PropertyChange
    {
        private ObservableCollection<User> _users { get; set; }
        public ObservableCollection<User> Users { get { return _users; } }


        public ListOfUserViewModel()
        {
            GetUsers();
        }

        private Page _mainPageContent { get; set; }

        public Page MainPageContent
        {
            get { return _mainPageContent; }
            set
            {
                _mainPageContent = value;
                OnPropertyChanged(nameof(MainPageContent));
            }

        }

        public void GetUsers()
        {
            _users = UserController.Get();
        }

        public void ShowContent(Frame frame, Type typeOf)
        {
            frame.Navigate(typeOf);
        }
    }
}
