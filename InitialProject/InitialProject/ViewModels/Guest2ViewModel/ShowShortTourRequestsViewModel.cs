using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class ShowShortTourRequestsViewModel : BaseViewModel
    {
        public ICommand GoBackCommand { get; private set; }
        public ShortTourRequestService _shortRequestService { get; set; }

        private ObservableCollection<ShortTourRequest> _allRequests;
        public ObservableCollection<ShortTourRequest> AllRequests
        {
            get { return _allRequests; }
            set
            {
                if (value != _allRequests)
                    _allRequests = value;
                OnPropertyChanged(nameof(AllRequests));
            }
        }
        private ObservableCollection<ShortTourRequest> _acceptedRequests;
        public ObservableCollection<ShortTourRequest> AcceptedRequests
        {
            get { return _acceptedRequests; }
            set { if(value != _acceptedRequests)
                    _acceptedRequests = value;
                    OnPropertyChanged(nameof(AcceptedRequests));
            }
        }
        public List<ShortTourRequest> allRequests { get; set; }
        public List<ShortTourRequest> acceptedRequests { get; set; }
        public NavigationService NavigationService { get; }
        public ShowShortTourRequestsViewModel(NavigationService navService)
        {
            NavigationService = navService;
            GoBackCommand = new RelayCommand(GoBack);
            _shortRequestService = new ShortTourRequestService();
            allRequests = _shortRequestService.GetAll();
            acceptedRequests = _shortRequestService.GetAcceptedRequests();
            AllRequests = new ObservableCollection<ShortTourRequest>(allRequests);
            AcceptedRequests = new ObservableCollection<ShortTourRequest>(acceptedRequests);
        }
        public void GoBack()
        {
            NavigationService.Navigate(new ShowTourPage(NavigationService));
        }
    }
}
