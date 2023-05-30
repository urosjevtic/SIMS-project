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
    public class FullViewComplexRequestViewModel : BaseViewModel
    {
        public ICommand GoBackCommand { get; private set; }
        public ComplexTourRequestService _complexRequestService { get; set; }
        public List<ShortTourRequest> allRequests { get; set; }
        public List<ShortTourRequest> acceptedRequests { get; set; }
        public NavigationService NavigationService { get; }
        public FullViewComplexRequestViewModel(NavigationService navService, ComplexTourRequest request)
        {
            NavigationService = navService;
            GoBackCommand = new RelayCommand(GoBack);
            _complexRequestService = new ComplexTourRequestService();
            allRequests = request.Requests;
            acceptedRequests = request.Requests.FindAll(x => x.Status == RequestStatus.Accepted);
        }
        public void GoBack()
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
