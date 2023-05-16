using InitialProject.Domain.Model;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    internal class GuestRatingViewModel : BaseViewModel
    {
        public ObservableCollection<RatedGuest>Ratings { get; set; }
        private readonly RatedGuestService _ratedGuestService;
        private  User LoggedUser { get; set; } = App.LoggedUser;

        //public Accommodation Accommodation { get; set; }
        public NavigationService NavigationService { get; set; }
        
        public GuestRatingViewModel(NavigationService navigationService)
        {
           _ratedGuestService = new RatedGuestService();
            Ratings = new ObservableCollection<RatedGuest>(_ratedGuestService.GetRatedGuests());
            NavigationService = navigationService;
        } 
    }
}
