using InitialProject.Domain.Model.Reservations;
using InitialProject.Service;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Notification.Wpf;
using System.Threading.Tasks;
using System.Windows.Navigation;
using InitialProject.Repository.AccommodationRepo;
using InitialProject.Repository.ReservationRepo;
using System.Collections.ObjectModel;
using System.Windows.Input;
using InitialProject.Utilities;
using GalaSoft.MvvmLight.Messaging;
using InitialProject.Service.ReservationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Notification.Wpf;

namespace InitialProject.ViewModels
{
    public class MoveRequestViewModel : BaseViewModel
    {
        public readonly AccommodationReservationRescheduleRequestService _accommodationReservationRescheduleRequestService;
        private AccommodationReservation _selectedReservation;
        public NavigationService _navigationService;
        private readonly ImageService _imageService;
        private readonly Image _images;
        private int ImageCounter = 0;

        public AccommodationReservation SelectedReservation
        {
            get
            {
                return _selectedReservation;
            }
            set
            {
                if(value != _selectedReservation)
                {
                    _selectedReservation = value;
                    OnPropertyChanged(nameof(SelectedReservation));
                }
            }
        }


        public MoveRequestViewModel(AccommodationReservation selectedReservation, NavigationService navigationService)
        {
            _accommodationReservationRescheduleRequestService = new AccommodationReservationRescheduleRequestService();
            _navigationService= navigationService;
            SelectedReservation = selectedReservation;

            _imageService = new ImageService();
            if (_imageService.GetById(SelectedReservation.Accommodation.Images.Id) != null)
            {
                _images = _imageService.GetById(SelectedReservation.Accommodation.Images.Id);
                _imageUrl = _images.Url[ImageCounter];
            }
            //_images = _imageService.GetById(SelectedAccommodation.Images.Id);
            //  _imageUrl = _images.Url[ImageCounter];

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
        string status = "pending";
        public ICommand MoveRequestCommand => new RelayCommand(MoveRequest);
        public void MoveRequest()
        {
            _accommodationReservationRescheduleRequestService.Create(SelectedReservation, _startDate, _endDate, status);
            Messenger.Default.Send<ToastNotification>(new ToastNotification("Success", "You have successfully send request for move reservation.", NotificationType.Success));
        }


        public ICommand NextImageCommand => new RelayCommand(NextImage);

        private void NextImage()
        {
            ImageCounter++;
            if (_images != null)
            {
                if (ImageCounter > _images.Url.Count - 1)
                {
                    ImageCounter = 0;
                }
                ImageUrl = _images.Url[ImageCounter];
            }
            //OnPropertyChanged(nameof(ImageUrl));
        }
        public ICommand PreviousImageCommand => new RelayCommand(PreviousImage);
        private void PreviousImage()
        {
            ImageCounter--;
            if (_images != null)
            {
                if (ImageCounter < 0)
                {
                    ImageCounter = _images.Url.Count - 1;
                }
                ImageUrl = _images.Url[ImageCounter];
            }
            //OnPropertyChanged(nameof(ImageUrl));
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

    

      
    }
}
