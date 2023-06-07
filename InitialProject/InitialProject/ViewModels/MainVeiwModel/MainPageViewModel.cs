using InitialProject.View.OwnerView.MyAccommodations;
using InitialProject.View.OwnerView.Notes;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.View.OwnerView.Renovations;
using InitialProject.View.OwnerView.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using InitialProject.Domain.Model;
using InitialProject.Utilities;
using System.Windows.Navigation;
using InitialProject.View.OwnerView.Forums;
using InitialProject.View.OwnerView.Settings;

namespace InitialProject.ViewModels.MainVeiwModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private User _loggedInUser;

        public User LogedInUser
        {
            get { return _loggedInUser; }
            set
            {
                _loggedInUser = value;
                OnPropertyChanged(nameof(LogedInUser));
            }
        }

        public string WelcomeMessage => LogedInUser.Username;


        public NavigationService NavigationService { get; set; }
        public MainPageViewModel(User user, NavigationService navigationService)
        {
            LogedInUser = user;
            NavigationService = navigationService;
        }


        public ICommand MyAccommoadionsOpenCommand => new RelayCommand(MyAccommoadionsOpen);
        private void MyAccommoadionsOpen()
        {
            NavigationService.Navigate(new MyAccommodationsMainView(_loggedInUser, NavigationService));
        }


        public ICommand RatingsOpenCommand => new RelayCommand(RatingsOpen);
        private void RatingsOpen()
        {
            NavigationService.Navigate(new RatingsMainView(_loggedInUser, NavigationService));
        }



        public ICommand ReservationsOpenCommand => new RelayCommand(ReservationsOpen);
        public void ReservationsOpen()
        {
            NavigationService.Navigate(new ReservationsMainView(_loggedInUser, NavigationService));
        }


        public ICommand RenovationsOpenCommand => new RelayCommand(RenovationsOpen);
        public void RenovationsOpen()
        {
            NavigationService.Navigate(new RenovationsMainView(_loggedInUser, NavigationService));
        }

        public ICommand SettingsOpenCommand => new RelayCommand(SettingsOpen);
        public void SettingsOpen()
        {
            NavigationService.Navigate(new OwnerSettingsView(_loggedInUser, NavigationService));
        }

        public ICommand ForumsOpenCommand => new RelayCommand(ForumsOpen);
        public void ForumsOpen()
        {
            NavigationService.Navigate(new ForumSelcetionView(_loggedInUser, NavigationService));
        }

        public ICommand NotesOpenCommand => new RelayCommand(NotesOpen);

        private void NotesOpen()
        {
            NotesView notesView = new NotesView(_loggedInUser);
            notesView.Show();
        }

        public ICommand LogOutCommand => new RelayCommand(LogOut);

        private void LogOut()
        {
            SignInForm signInForm = new SignInForm();
            CloseCurrentWindow();
            signInForm.Show();
        }
    }
}
