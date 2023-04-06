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
using System.Windows.Shapes;
using InitialProject.Serializer;
using InitialProject.Repository;
using InitialProject.DTO;
using InitialProject.Domain.Model;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ActiveTour : Window
    {
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly NotificationRepository _notificationRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly UserRepository _userRepository;
        private readonly CheckedCheckPointRepository _checkedCheckPointRepository;
        private readonly CheckPointRepository _checkPointRepository;


        public List<User> Guests { get; set; }
        List<User> TourGuests { get; set; }
        public User User { get; set; }
        public Tour SelectedTour { get; set; }
        public List<CheckPoint> CheckPoints { get; set; }
        public List<CheckedCheckPoint> CheckedCheckPoints { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<GuestDTO> CalledGuests { get; set; }
        public List<GuestDTO> UncalledGuests { get; set; }
        public CheckPoint LastCheckedCheckPoint { get; set; }   

        public List<GuestDTO> GuestsDTO { get; set; }
        public CheckedCheckPointRepository checkedCheckPointRepository { get; set; }

        public ActiveTour(Tour selectedTour, List<CheckedCheckPoint> listCCP)
        {
            InitializeComponent();

            this.DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _notificationRepository = new NotificationRepository();
            _tourReservationRepository = new TourReservationRepository();
            _userRepository = new UserRepository();
            _checkedCheckPointRepository = new CheckedCheckPointRepository();
            _checkPointRepository = new CheckPointRepository();
            GuestsDTO = new List<GuestDTO>();
            CalledGuests = new List<GuestDTO>();
            UncalledGuests = new List<GuestDTO>();
            SelectedTour = selectedTour;
            checkedCheckPointRepository = new CheckedCheckPointRepository();

            CheckedCheckPoints = listCCP;
            
            Notifications = _notificationRepository.GetAll();
            User = new User();
            SelectedTour = selectedTour;
            CheckPoints = new List<CheckPoint>(); ;
            Guests = _tourReservationRepository.GetReservationGuest(SelectedTour);
            LastCheckedCheckPoint = _checkPointRepository.FindTourLastCheckPoint(SelectedTour);
            CreateAllDTOForms();
            CreateUncheckedListCP();
            CreateUncalledGuestsDTO();
        }

        private void CreateUncalledGuestsDTO()
        {
            List<int> allGuestsIndexes = new List<int>();
            List<int> calledGuestsIndexes = new List<int>();
            foreach(var guest in Guests)
            {
                allGuestsIndexes.Add(guest.Id);
            }
            foreach(var dto in CalledGuests)
            {
                calledGuestsIndexes.Add(dto.Id);
            }
            foreach(var ind in allGuestsIndexes)
            {
                if(!calledGuestsIndexes.Contains(ind))
                {
                    GuestDTO dto = CreateOneDTOForm(Guests.Find(t => t.Id == ind));
                    UncalledGuests.Add(dto);
                }
            }
        }

        private void CreateUncheckedListCP()
        {
            List<CheckPoint> checkPoints = SelectedTour.CheckPoints;
            List<string> checkPointsNames = new List<string>();
            List<string> checkedCheckPointNames = new List<string>();
            foreach(var cp in checkPoints)
            {
                checkPointsNames.Add(cp.Name);
            }
            foreach(var ccp in CheckedCheckPoints)
            {
                checkedCheckPointNames.Add(ccp.Name);
            }
            foreach(var cp in checkPointsNames)
            {
                if(!checkedCheckPointNames.Contains(cp))
                {
                    CheckPoints.Add(checkPoints.Find(t => t.Name.Equals(cp)));
                }
            }
        }
        

        private void CreateAllDTOForms()
        {
            foreach (var guest in Guests)
            {
                foreach (var item in Notifications)
                {
                    if (guest.Id == item.GuestId && item.IsChecked)
                    {
                        GuestDTO dto = CreateOneDTOForm(guest);
                        CalledGuests.Add(dto);
                    }
                }
            }
        }

        private GuestDTO CreateOneDTOForm(User guest)
        {
            GuestDTO guestDTO = new GuestDTO(guest.Id, guest.Username, guest.Role, guest.Presence);
            return guestDTO;
        }

        private void CheckBoxCheckPoint(object sender, RoutedEventArgs e)
        {
            CheckPoint checkedCheckPoint = ((CheckBox)sender).DataContext as CheckPoint;
            checkedCheckPoint.IsChecked = true;
            CheckedCheckPoint ccp = checkedCheckPointRepository.Transform(checkedCheckPoint);
            checkedCheckPointRepository.Save(ccp);
            if(checkedCheckPoint == LastCheckedCheckPoint)
            {
                SelectedTour.IsActive = false;
                _tourRepository.Update(SelectedTour);
                foreach (User guest in Guests)
                {
                    guest.Presence = UserPresence.Unknown;
                    _userRepository.Update(guest);
                }
                _notificationRepository.DeleteAll();
                _checkedCheckPointRepository.DeleteAll();
                this.Close();
            }

        }

        private void EndTourClick(object sender, RoutedEventArgs e)
        {
            SelectedTour.IsActive = false;
            _tourRepository.Update(SelectedTour);
            foreach (User guest in Guests)
            {
                guest.Presence = UserPresence.Unknown;
                _userRepository.Update(guest);
            }
            _notificationRepository.DeleteAll();
            _checkedCheckPointRepository.DeleteAll();
            this.Close();
        }

        private void CheckedGuests(object sender, RoutedEventArgs e)
        {
            GuestDTO checkedGuest = ((CheckBox)sender).DataContext as GuestDTO;
            MakeNotification(checkedGuest);
        }

        private void MakeNotification(GuestDTO checkedGuest)
        {
            Notification notification = new Notification();
            notification.TourId = SelectedTour.Id;
            notification.IsChecked = true;
            notification.CheckPointId = CheckedCheckPoints.Last().Id;
            notification.GuestId = checkedGuest.Id;
            Notifications.Add(notification);
            _notificationRepository.Save(notification);

        }
    }
}
