using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Repository.ReservationRepo;
using InitialProject.Service;
using InitialProject.Service.RenovationServices;
using InitialProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationRatingFormPage.xaml
    /// </summary>
    public partial class AccommodationRatingFormPage : Page
    {
        //public UnratedOwner UnratedOwner{ get; set; }
        //public AccommodationReservation Reservation { get; set; }
        //public Domain.Model.User LoggedUser { get; set; } = App.LoggedUser;
        //private readonly AccommodationReservationRepository _accommodationReservationRepository;
        //private readonly AccommodationRepository _accommodationRepository;
        //public readonly OwnerRatingService _ownerRatingService;
        //public readonly RenovationRecommendationService _renovationRecommendationService;
    
        public AccommodationRatingFormPage(UnratedOwner unratedOwner, NavigationService navigationService)
        {
            InitializeComponent();
            //UnratedOwner=unratedOwner;
            // _accommodationRepository = new AccommodationRepository();
            // _accommodationReservationRepository = new AccommodationReservationRepository();
            //   Reservation=unratedOwner.Reservation;
            DataContext = new AccommodationRatingFormViewModel(unratedOwner, navigationService);
        }
       
    }
}
