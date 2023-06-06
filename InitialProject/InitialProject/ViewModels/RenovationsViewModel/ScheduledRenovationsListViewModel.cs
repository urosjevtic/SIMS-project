using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Service.RatingServices;
using InitialProject.Service.RenovationServices;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.PopupWindows;
using InitialProject.View.OwnerView.Renovations;
using RelayCommand = InitialProject.Utilities.RelayCommand;

namespace InitialProject.ViewModels.RenovationsViewModel
{
    public class ScheduledRenovationsListViewModel : BaseViewModel
    {

        private readonly RenovationService _renovationService;
        private readonly User _logedInUser;


        public ObservableCollection<Renovation> Renovations {get; set; }
        public NavigationService NavigationService { get; set; }

        public ScheduledRenovationsListViewModel(User logedInUser, NavigationService navigationService)
        {
            _renovationService = new RenovationService();
            _logedInUser = logedInUser;
            Renovations = new ObservableCollection<Renovation>(_renovationService.GetByOwnerId(logedInUser.Id));
            NavigationService = navigationService;
        }


        public ICommand CancelRenovationCommand => new RelayCommandWithParams(CancelRenovation);

        private void CancelRenovation(object parameter)
        {
            if (parameter is Renovation selectedRenovation)
            {
                ConfirmCancelingRenovationView confirmCancelingRenovationView =
                    new ConfirmCancelingRenovationView(selectedRenovation, Renovations);
                confirmCancelingRenovationView.ShowDialog();
            }
        }


        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            NavigationService.Navigate(new RenovationsMainView(_logedInUser, NavigationService));
        }
    }
}
