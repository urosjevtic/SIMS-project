using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Service.RatingServices;
using InitialProject.Service.RenovationServices;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Renovations;
using RelayCommand = InitialProject.Utilities.RelayCommand;

namespace InitialProject.ViewModels.RenovationsViewModel
{
    public class ScheduledRenovationsListViewModel : BaseViewModel
    {

        private readonly RenovationService _renovationService;
        private readonly User _logedInUser;


        public ObservableCollection<Renovation> Renovations {get; set; }
        public Navigator Navigator { get; set; }

        public ScheduledRenovationsListViewModel(User logedInUser, Navigator navigator)
        {
            _renovationService = new RenovationService();
            _logedInUser = logedInUser;
            Renovations = new ObservableCollection<Renovation>(_renovationService.GetByOwnerId(logedInUser.Id));
            Navigator = navigator;
        }


        public ICommand CancelRenovationCommand => new RelayCommandWithParams(CancelRenovation);

        private void CancelRenovation(object parameter)
        {
            if (parameter is Renovation selectedRenovation)
            {
                Renovations.Remove(selectedRenovation);
                _renovationService.Delete(selectedRenovation);
            }
        }


        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            Navigator.NavigateTo(new RenovationsMainView(_logedInUser, Navigator));
        }
    }
}
