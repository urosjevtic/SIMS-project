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

namespace InitialProject.ViewModels.Guest2ViewModel
{
    public class ShowComplexRequestsViewModel : BaseViewModel
    {
        public ICommand GoBackCommand { get; set; }
        public ICommand FullViewCommand { get; set; }
        public NavigationService navigationService { get; set; }
        public ComplexTourRequestService _complexTourRequestService { get; set; }

        private ObservableCollection<ComplexTourRequest> _complexRequests;
        public ObservableCollection<ComplexTourRequest> ComplexRequests
        {
            get { return _complexRequests; }
            set
            {
                if (value != _complexRequests)
                    _complexRequests = value;
                OnPropertyChanged(nameof(ComplexRequests));
            }
        }
        private ComplexTourRequest _selectedRequest;
        public ComplexTourRequest SelectedRequest
        {
            get { return _selectedRequest; }
            set
            {
                if (value != _selectedRequest)
                    _selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }
        public ShowComplexRequestsViewModel(NavigationService navService)
        {
            this.navigationService = navService;
            GoBackCommand = new RelayCommand(GoBack);
            FullViewCommand = new RelayCommand<ComplexTourRequest>(FullView);
            _complexTourRequestService = new ComplexTourRequestService();
            ComplexRequests = new ObservableCollection<ComplexTourRequest>(_complexTourRequestService.GetAllRequests());
        }
        public void GoBack()
        {
            navigationService.Navigate(new ShowTourPage(navigationService));
        }
        public void FullView(ComplexTourRequest complexRequest)
        {
            SelectedRequest = complexRequest;
            navigationService.Navigate(new FullViewComplexRequest(navigationService, SelectedRequest));
        }
    }
}
