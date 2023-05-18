using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.ViewModels;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for ShowTour.xaml
    /// </summary>
    public partial class ShowTour : Window
    {
        public ShowTourViewModel ShowTourViewModel { get; set; }
        public User LoggedUser { get; set; }
        private readonly NotificationRepository _notificationRepository;
        private readonly UserRepository _userRepository;
        private readonly TourGuestRepository _tourGuestsRepository;
        private readonly CheckPointRepository _checkPointRepository;
        private readonly GuestsCheckPointRepository _guestsCheckPointRepository;

        public List<Notification> Notifications { get; set; }
        public Model.TourGuest Guest { get; set; }

        public ShowTour(User user)
        {
            InitializeComponent();
            //ShowTourViewModel = new ShowTourViewModel();
            this.DataContext = ShowTourViewModel;
            LoggedUser = user;
            _notificationRepository = new NotificationRepository();
            _tourGuestsRepository = new TourGuestRepository();
            _checkPointRepository = new CheckPointRepository();
            _guestsCheckPointRepository = new GuestsCheckPointRepository();

            _userRepository = new UserRepository();
            Guest = _tourGuestsRepository.GetById(LoggedUser.Id);
            Notifications = new List<Notification>(_notificationRepository.GetAll());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
