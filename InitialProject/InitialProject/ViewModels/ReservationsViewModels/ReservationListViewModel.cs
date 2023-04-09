using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;

namespace InitialProject.ViewModels.ReservationsViewModels
{
    public class ReservationListViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        private readonly AccommodationReservationService _accommodationReservationService;
        public ReservationListViewModel(User logedInUser)
        {
            _accommodationReservationService = new AccommodationReservationService();
            Reservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetReservationByOwnerId(logedInUser.Id));
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
