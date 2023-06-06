using InitialProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.ViewModels
{
    public abstract class SideScreenViewModel : BaseViewModel
    {


        private Visibility _sideScreenVisibility = Visibility.Collapsed;

        public Visibility SideScreenVisibility
        {
            get { return _sideScreenVisibility; }
            set
            {
                _sideScreenVisibility = value;
                OnPropertyChanged(nameof(SideScreenVisibility));
            }
        }

        private Visibility _mainScreenVisibility = Visibility.Visible;

        public Visibility MainScreenVisibility
        {
            get { return _mainScreenVisibility; }
            set
            {
                _mainScreenVisibility = value;
                OnPropertyChanged(nameof(MainScreenVisibility));
            }
        }
        public ICommand BurgerBarClosedCommand => new RelayCommand(BurgerBarClosed);
        public ICommand BurgerBarOpenCommand => new RelayCommand(BurgerBarOpen);
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
        public ICommand MyAccommoadionsOpenCommand => new RelayCommand(MyAccommoadionsOpen);
        public ICommand RatingsOpenCommand => new RelayCommand(RatingsOpen);

        public ICommand ReservationsOpenCommand => new RelayCommand(ReservationsOpen);

        public ICommand RenovationsOpenCommand => new RelayCommand(RenovationsOpen);
        public ICommand NotificationOpenCommand => new RelayCommand(NotificationOpen);
        public ICommand NotesOpenCommand => new RelayCommand(NotesOpen);

        public ICommand SettingsOpenCommand => new RelayCommand(SettingsOpen);
        public ICommand ForumOpenCommand => new RelayCommand(ForumOpen);

        public ICommand LogOutCommand => new RelayCommand(LogOut);
        public ICommand HelpCommand => new RelayCommand(Help);


        protected abstract void MyAccommoadionsOpen();
        protected abstract void RatingsOpen();

        protected abstract void ReservationsOpen();
        protected abstract void RenovationsOpen();
        protected abstract void NotificationOpen();
        protected abstract void NotesOpen();
        protected abstract void SettingsOpen();
        protected abstract void ForumOpen();

        protected abstract void LogOut();

        protected abstract void Help();
    }
}
