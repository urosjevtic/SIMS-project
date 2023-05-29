using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class ShowTourViewModel : Page
    {
        public User LoggedUser { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ShowMyToursCommand { get; set; }
        public ICommand MakingTourRequestsCommand { get; set; }
        public ICommand ShowRequestsCommand { get; set; }
        public ICommand ShowVouchersCommand { get; set; }
        public ICommand ShowNotificationsCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public NavigationService navigationService { get; set; }
        public ShowTourViewModel(NavigationService nav)
        {
            this.navigationService = nav;
            SearchCommand = new RelayCommand(Search);
            ShowMyToursCommand = new RelayCommand(ShowMyTours);
            MakingTourRequestsCommand = new RelayCommand(OpenMakingRequests);
            ShowRequestsCommand = new RelayCommand(ShowRequests);
            ShowVouchersCommand = new RelayCommand(ShowVouchers);
            ShowNotificationsCommand = new RelayCommand(ShowNotifications);
            ExitCommand = new RelayCommand(Exit);
        }
        private void Search()
        {
            navigationService.Navigate(new TourSearchPage(navigationService));
        }
        private void ShowMyTours()
        {
            navigationService.Navigate(new MyToursPage(navigationService));
        }
        private void OpenMakingRequests()
        {
            navigationService.Navigate(new MakeRequestsPage(navigationService));
        }
        private void ShowRequests()
        {
            navigationService.Navigate(new ShowAllRequests(navigationService));
        }
        private void ShowVouchers()
        {
            navigationService.Navigate(new ShowVouchersPage(navigationService));
        }
        private void ShowNotifications()
        {
            navigationService.Navigate(new ShowNotificationsPage(navigationService));
        }
        private void Exit()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        } 
    }
}
