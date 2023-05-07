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


        public Navigator Navigator { get; set; }
        public MainPageViewModel(User user, Navigator navigator)
        {
            LogedInUser = user;
            Navigator = navigator;
        }


        public ICommand MyAccommoadionsOpenCommand => new RelayCommand(MyAccommoadionsOpen);
        private void MyAccommoadionsOpen()
        {
            Navigator.NavigateTo(new MyAccommodationsMainView(_loggedInUser, Navigator));
        }


        public ICommand RatingsOpenCommand => new RelayCommand(RatingsOpen);
        private void RatingsOpen()
        {
            Navigator.NavigateTo(new RatingsMainView(_loggedInUser, Navigator));
        }



        public ICommand ReservationsOpenCommand => new RelayCommand(ReservationsOpen);
        public void ReservationsOpen()
        {
            Navigator.NavigateTo(new ReservationsMainView(_loggedInUser));
        }


        public ICommand RenovationsOpenCommand => new RelayCommand(RenovationsOpen);
        public void RenovationsOpen()
        {
            Navigator.NavigateTo(new RenovationsMainView(_loggedInUser, Navigator));
        }

        public ICommand NotesOpenCommand => new RelayCommand(NotesOpen);

        private void NotesOpen()
        {
            NotesView notesView = new NotesView(_loggedInUser);
            notesView.Show();
        }
    }
}
