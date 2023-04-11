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
        public ICommand ShowVouchersCommand { get; set; }

        public ShowTourViewModel(User user)
        {
            LoggedUser = user;
            SearchCommand = new RelayCommand(Search);
            ShowMyToursCommand = new RelayCommand(ShowMyTours);
            ShowVouchersCommand = new RelayCommand(ShowVouchers);
           
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
        private void ShowVouchers()
        {
            ShowVouchers showVouchers = new ShowVouchers();
            showVouchers.Show();
        }
       
    }
}
