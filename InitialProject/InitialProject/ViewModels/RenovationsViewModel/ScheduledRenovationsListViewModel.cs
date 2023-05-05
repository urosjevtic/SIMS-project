using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Service.RatingServices;
using InitialProject.Service.RenovationServices;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.Ratings;

namespace InitialProject.ViewModels.RenovationsViewModel
{
    public class ScheduledRenovationsListViewModel : BaseViewModel
    {

        private readonly RenovationService _renovationService;


        public ObservableCollection<Renovation> Renovations {get; set; }
        public ScheduledRenovationsListViewModel(User logedInUser)
        {
            _renovationService = new RenovationService();
            Renovations = new ObservableCollection<Renovation>(_renovationService.GetByOwnerId(logedInUser.Id));
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
    }
}
