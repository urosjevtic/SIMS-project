using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.ViewModels;
using Notification.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationShow.xaml
    /// </summary>
    public partial class AccommodationShow : Window //, INotifyPropertyChanged
    {
        public User LoggedUser { get; set; }

       
        public AccommodationShow(User user)
        {
            InitializeComponent();
            App.LoggedUser = user;
            this.DataContext = new MainWindowViewModel(NavigationFrame.NavigationService);
        }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            App.LoggedUser = null;
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var childControl in SideMenuButtonsGrid.Children)
            {
                if(childControl is Button button)
                {
                    if(button == sender)
                    {
                        button.Background = Brushes.Teal;
                        button.BorderBrush= Brushes.Teal;
                    }
                    else
                    {
                        button.Background = Brushes.LightGray;
                        button.BorderBrush = Brushes.LightGray;
                    }
                }
            }
        }
    }
}
