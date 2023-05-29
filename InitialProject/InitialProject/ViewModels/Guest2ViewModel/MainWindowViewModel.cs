using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels.Guest2ViewModel
{
    public class StartWindowViewModel : BaseViewModel
    {
        public NavigationService NavigationService { get; set; }

        private Frame _selectedPage = new Frame();
        public Frame SelectedPage
        {
            get => _selectedPage;
            set
            {
                _selectedPage = value;
                OnPropertyChanged(nameof(SelectedPage));
            }
        }
        public User LoggedUser { get; set; }
        public StartWindowViewModel(User user)
        {
            App.LoggedUser = user;
            NavigationService = SelectedPage.NavigationService;
            SelectedPage.Content = new ShowTourPage(NavigationService);
        }
    }
}
