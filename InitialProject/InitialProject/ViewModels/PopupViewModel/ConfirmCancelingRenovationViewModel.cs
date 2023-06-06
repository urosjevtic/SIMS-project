using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using InitialProject.Service.StatisticService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Utilities;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Service.RenovationServices;

namespace InitialProject.ViewModels.PopupViewModel
{
    public class ConfirmCancelingRenovationViewModel : BaseViewModel
    {
        private readonly RenovationService _renovationService;
        private readonly AccommodationReservationService _accommodationReservationService;

        private readonly Renovation _renovation;
        private readonly ObservableCollection<Renovation> _renovations;
        public ConfirmCancelingRenovationViewModel(Renovation renovation, ObservableCollection<Renovation> renovations)
        {
            _renovationService = new RenovationService();
            _accommodationReservationService = new AccommodationReservationService();
            _renovation = renovation;
            _renovations = renovations;
        }


        public ICommand YesCommand => new RelayCommand(Yes);
        private void Yes()
        {

            _renovations.Remove(_renovation);
            _renovationService.Delete(_renovation);
            CloseCurrentWindow();
        }



        public ICommand NoCommand => new RelayCommand(No);

        private void No()
        {
            CloseCurrentWindow();
        }
    }
}
