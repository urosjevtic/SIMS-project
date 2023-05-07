using InitialProject.Model;
using InitialProject.Utilities;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.View.OwnerView.MyAccommodations;
using System.Linq;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;
using InitialProject.Repository.StatisticRepo;
using InitialProject.View.OwnerView.Notes;
using InitialProject.View.OwnerView.Ratings;
using InitialProject.View.OwnerView.Reservations;
using InitialProject.ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using InitialProject.View.OwnerView.Renovations;

namespace InitialProject.ViewModel
{
    public class OwnerMainViewModel : SideScreenViewModel
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



        public OwnerMainViewModel(User user)
        {
            LogedInUser = user;
        }

        private void BurgerBarOpen()
        {
            SideScreenVisibility = Visibility.Visible;
            MainScreenVisibility = Visibility.Collapsed;
        }

        private void BurgerBarClosed()
        {
            SideScreenVisibility = Visibility.Collapsed;
            MainScreenVisibility = Visibility.Visible;
        }


        protected override void MyAccommoadionsOpen()
        {
            MyAccommodationsMainWindow myAccommodationsMainWindow = new MyAccommodationsMainWindow(LogedInUser);
            CloseCurrentWindow();
            myAccommodationsMainWindow.Show();
           
        }


        protected override void RatingsOpen()
        {
            RatingsMainWindow ratingsMain = new RatingsMainWindow(LogedInUser);
            CloseCurrentWindow();
            ratingsMain.Show();
        }


        protected override void ReservationsOpen()
        {
            ReservationsMainWindow reservationsMain = new ReservationsMainWindow(LogedInUser);
            CloseCurrentWindow();
            reservationsMain.Show();
        }

        protected override void RenovationsOpen()
        {
            RenovationsMainView renovationsMainView = new RenovationsMainView(LogedInUser);
            CloseCurrentWindow();
            renovationsMainView.Show();
        }

        public ICommand NotesOpenCommand => new RelayCommand(NotesOpen);

        private void NotesOpen()
        {
            NotesView notesView = new NotesView(_loggedInUser);
            notesView.Show();
        }


    }
}