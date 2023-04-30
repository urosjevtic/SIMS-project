using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    public class ShowTourViewModel
    {
        public User LoggedUser { get; set; }
        
        public ICommand SearchCommand { get; set; }
        public ICommand ShowMyToursCommand { get; set; }
        public ICommand MakingTourRequestsCommand { get; set; }
        public ICommand ShowRequestsCommand { get; set; }
        public ICommand ShowVouchersCommand { get; set; }
        public ICommand ShowNotificationsCommand { get; set; }

        public ShowTourViewModel(User user)
        {
            LoggedUser = user;
            SearchCommand = new RelayCommand(Search);
            ShowMyToursCommand = new RelayCommand(ShowMyTours);
            MakingTourRequestsCommand = new RelayCommand(OpenMakingRequests);
            ShowRequestsCommand = new RelayCommand(ShowRequests);
            ShowVouchersCommand = new RelayCommand(ShowVouchers);
            ShowNotificationsCommand = new RelayCommand(ShowNotifications);
        }
        private void Search()
        {
            TourSearch tourSearch = new TourSearch(LoggedUser);
            tourSearch.Show();
        }
        private void ShowMyTours()
        {
            MyTours myTours = new MyTours(LoggedUser);
            myTours.Show();
        }
        private void OpenMakingRequests()
        {
            MakeRequests makeRequests = new MakeRequests(LoggedUser);
            makeRequests.Show();
        }
        private void ShowRequests()
        {
            ShowRequests showRequests = new ShowRequests();
            showRequests.Show();
        }
        private void ShowVouchers()
        {
            ShowVouchers showVouchers = new ShowVouchers();
            showVouchers.Show();
        }
        private void ShowNotifications()
        {
            ShowNotifications showNotifications = new ShowNotifications();
            showNotifications.Show();
        }
       
    }
}
