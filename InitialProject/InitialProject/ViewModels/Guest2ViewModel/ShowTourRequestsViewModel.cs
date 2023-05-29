using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class ShowTourRequestsViewModel : BaseViewModel
    {
        public ICommand ShowShortTourRequestsCommand { get; private set; }
        public ICommand ShowShortTourStatisticsCommand { get; private set; }
        public ICommand ShowComplexTourRequestsCommand { get; private set; }
        public NavigationService NavigationService { get; private set; }
        public ShowTourRequestsViewModel(NavigationService navigation)
        {
            this.NavigationService = navigation;
            ShowShortTourRequestsCommand = new RelayCommand(ShowShortRequests);
            ShowShortTourStatisticsCommand = new RelayCommand(ShowShortStatistics);
            ShowComplexTourRequestsCommand = new RelayCommand(ShowComplexRequests);
        }
        public void ShowShortRequests()
        {
            NavigationService.Navigate(new ShowShortTourRequestsPage(NavigationService));
        }
        public void ShowShortStatistics()
        {
            NavigationService.Navigate(new ShowShortTourStatisticsPage(NavigationService));
        }
        public void ShowComplexRequests()
        {
            NavigationService.Navigate(new ShowComplexTourRequests(NavigationService));
        }
    }
}
