using InitialProject.Domain.Model;
using InitialProject.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationShow.xaml
    /// </summary>
    public partial class AccommodationShow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;
        private ObservableCollection<Accommodation> _accommodations;

        public User User { get; set; }
        public User LoggedUser { get; set; }

        public Accommodation SelectedAccommodation { get; set; }
        public ObservableCollection<Accommodation> Accommodations
        {
            get
            {
                return _accommodations;
            }

            set
            {
                if (value != _accommodations)
                {
                    _accommodations = value;
                    OnPropertyChanged("Accommodations");
                }
            }
        }

        public AccommodationShow(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            _accommodationRepository = new AccommodationRepository();
            LoggedUser = user;
            LoadData();

            // Proveri za notifikacije - Ako postoji, prikazi neki novi prozor sa porukom

        }

        private void LoadData()
        {
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationSearch accommodationSearch = new AccommodationSearch(LoggedUser);
            accommodationSearch.Show();
        }

        private void MyReservation_Click(object sender, RoutedEventArgs e)
        {
            MyReservations myReservation = new MyReservations();
            myReservation.Show();
        }
    }
}
