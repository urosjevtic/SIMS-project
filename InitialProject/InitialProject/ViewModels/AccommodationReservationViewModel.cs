using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Repository.ReservationRepo;
using InitialProject.Service;
using InitialProject.Service.RenovationServices;
using InitialProject.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    class AccommodationReservationViewModel : BaseViewModel
    {
        public Accommodation Accommodation { get; set; }
        public NavigationService _navigationService;
        private readonly ImageService _imageService;
        private readonly Image _images;
        private int ImageCounter = 0;
        public AccommodationReservationViewModel(Accommodation accommodation)
        {
            SelectedAccommodation = accommodation;

            _imageService = new ImageService();
            _images = _imageService.GetById(SelectedAccommodation.Images.Id);
            _imageUrl = _images.Url[ImageCounter];

            _accommodationRepository = new AccommodationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
          //  ReservationDates = new ObservableCollection<AccommodationReservation>();
        }

        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;

        public Accommodation SelectedAccommodation { get; set; }
        public Domain.Model.User LoggedUser { get; set; } = App.LoggedUser;

        public List<Accommodation> Accommodations { get; set; }
        public List<AccommodationReservation> reservations;
        public ObservableCollection<Accommodation> accommodations { get; set; }
        public ObservableCollection<AccommodationReservation> ReservationDates { get; set; }
        public AccommodationReservation SelectedDate { get; set; }
        // public DateTime StartDate { get; set; } = DateTime.Now.Date;
        // public DateTime EndDate { get; set; } = DateTime.Now.Date;


        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }


        public ICommand NextImageCommand => new RelayCommand(NextImage);

        private void NextImage()
        {
            ImageCounter++;
            if (ImageCounter > _images.Url.Count - 1)
                ImageCounter = 0;
            _imageUrl = _images.Url[ImageCounter];
            OnPropertyChanged(nameof(ImageUrl));
        }
        public ICommand PreviousImageCommand => new RelayCommand(PreviousImage);
        private void PreviousImage()
        {
            ImageCounter--;
            if (ImageCounter < 0)
                ImageCounter = _images.Url.Count - 1;
            _imageUrl = _images.Url[ImageCounter];
            OnPropertyChanged(nameof(ImageUrl));
        }



        private DateTime _startDate = DateTime.Now;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                //AvailableDates = _renovationService.FindAvailableDates(_accommodation.Id, _fromDate, _toDate, _renovationLength);
                OnPropertyChanged("StartDate");
            }
        }


        private DateTime _endDate = DateTime.Now;

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                //AvailableDates = _renovationService.FindAvailableDates(_accommodation.Id, _fromDate, _toDate, _renovationLength);
                OnPropertyChanged("EndDate");
            }
        }

        private int _reservationDays = 0;

        public int ReservationDays
        {
            get { return _reservationDays; }
            set
            {
                _reservationDays = value;
                //AvailableDates = _renovationService.FindAvailableDates(_accommodation.Id, _fromDate, _toDate, _renovationLength);
                OnPropertyChanged("ReservationDays");
            }
        }

        private int _guestNumber = 0;

        public int GuestNumber
        {
            get { return _guestNumber; }
            set
            {
                _guestNumber = value;
                //AvailableDates = _renovationService.FindAvailableDates(_accommodation.Id, _fromDate, _toDate, _renovationLength);
                OnPropertyChanged("GuestNumber");
            }
        }

    }
}
