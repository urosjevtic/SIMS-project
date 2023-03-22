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
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.DTO;

namespace InitialProject.View
{

    public partial class StartedTour : Window
    {
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly NotificationRepository _notificationRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly UserRepository _userRepository;
        private readonly CheckedCheckPointRepository _checkedCheckPointRepository;
        public List<GuestDTO> GuestsDTO{ get; set;}//ovo treba biti binding
        public GuideMainWindow _guideMainWindow { get; set; }
        public CheckBox checkBox { get; set; }
        public int firstSerialNumber { get; set; }
        public bool isChecked { get; set; }
        public List<User> Guests { get; set; }
        public List<User> allUsersInfo { get; set; } 
        public User User { get; set; }
        public Tour SelectedTour { get; set; }
        public List<CheckPoint> CheckPoints { get; set; }
        public List<CheckPoint> CheckedCheckPoints { get; set; }
        public List<Notification> Notifications { get; set; }  
                
        public StartedTour(Tour selectedTour,GuideMainWindow guideMainWindow)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _notificationRepository = new NotificationRepository();
            _tourReservationRepository = new TourReservationRepository();
            _userRepository = new UserRepository();
            _checkedCheckPointRepository = new CheckedCheckPointRepository();
            GuestsDTO = new List<GuestDTO>();

            CheckedCheckPoints = new List<CheckPoint>();
            Notifications = _notificationRepository.GetAll();   
            User = new User();
            _guideMainWindow = guideMainWindow;
            SelectedTour = selectedTour;

            CheckPoints = SelectedTour.CheckPoints;
            Guests = _tourReservationRepository.GetReservationGuest(SelectedTour);
            allUsersInfo = _userRepository.GetAll(); 
            CreateAllDTOForms();
        }
        
        private void CreateAllDTOForms()
        {
            foreach(var user in Guests)
            {
                GuestDTO dto = CreateOneDTOForm(user);
                GuestsDTO.Add(dto);
            }   
        }

        private GuestDTO CreateOneDTOForm(User guest)
        {
            GuestDTO guestDTO = new GuestDTO(guest.Id, guest.Username, guest.Role, guest.Presence);
            return guestDTO;
        }
        public void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            CheckPoints = SelectedTour.CheckPoints;
            CheckPoint checkedCheckPoint = ((CheckBox)sender).DataContext as CheckPoint;
            checkedCheckPoint.IsChecked = true;
            CheckedCheckPoints.Add(checkedCheckPoint); // cuva mi cekirane checkPointe
            CheckedCheckPoint ccp = _checkedCheckPointRepository.Transform(checkedCheckPoint);
            _checkedCheckPointRepository.Save(ccp);
        }
     

        private void EndTour(object sender, RoutedEventArgs e)
        {
            SelectedTour.IsActive = false;
            _tourRepository.Update(SelectedTour);
            foreach(User guest in Guests)
            {
                guest.Presence = UserPresence.Unknown;
                _userRepository.Update(guest);
            }
            this.Close();
        }

        
        private void CheckedGuests(object sender, RoutedEventArgs e)
        {
            Notification notification = new Notification();
            notification.TourId = SelectedTour.Id;
            notification.IsChecked = true;
            notification.CheckPointId = CheckedCheckPoints.Last().Id;          //cuva zadnju cekiranu checkPoint
            GuestDTO checkedGuest = ((CheckBox)sender).DataContext as GuestDTO;
            notification.GuestId = checkedGuest.Id;    
            Notifications.Add(notification);
            _notificationRepository.Save(notification);
            

        }
    }
}
