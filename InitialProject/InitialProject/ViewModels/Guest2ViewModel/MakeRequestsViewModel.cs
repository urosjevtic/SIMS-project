using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class MakeRequestsViewModel : BaseViewModel
    {
        public ICommand MakeShortTourRequestCommand { get; private set; }
        public ICommand MakeComplexTourRequestCommand { get; private set; }
        public NavigationService navService { get; }

        public MakeRequestsViewModel(NavigationService navigationService)
        {
            navService = navigationService;
            MakeShortTourRequestCommand = new RelayCommand(MakeShortRequest);
            MakeComplexTourRequestCommand = new RelayCommand(MakeComplexRequest);
        }
        public void MakeShortRequest()
        {
            navService.Navigate(new MakeShortTourRequestPage(navService));
        }
        public void MakeComplexRequest()
        {
            navService.Navigate(new MakeComplexTourRequestPage(navService));
        }
    }
}
