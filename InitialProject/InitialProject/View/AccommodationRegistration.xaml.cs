using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationRegistration.xaml
    /// </summary>
    public partial class AccommodationRegistration : Window
    {
        private readonly AccommodationRepository _repository;

        private string _accommodationName;
        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        /* private AccommodationType _accommodationType;

         private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             string nesto = accommodationType.Text;
             switch (nesto)
             {
                 case "Appartment":
                     _accommodationType = AccommodationType.appartment;
                     break;
                 case "House":
                     _accommodationType = AccommodationType.house;
                     break;
                 case "Cabin":
                     _accommodationType = AccommodationType.cabin;
                     break;

             }
         }*/
        private string _accommodationType;
        public string AccommodationTypes
        {
            get { return _accommodationType; }
            set
            {
                if (value != _accommodationType)
                {
                    _accommodationType = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxGuests;
        public string MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _minReservationDays;
        public string MinReservationDays
        {
            get => _minReservationDays;
            set
            {
                if (value != _minReservationDays)
                {
                    _minReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _cancelationPeriod;
        public string CancelationPeriod
        {
            get => _cancelationPeriod;
            set
            {
                if (value != _cancelationPeriod)
                {
                    _cancelationPeriod = value;
                    OnPropertyChanged();
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public AccommodationRegistration()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new AccommodationRepository();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Accommodation accommodation = new Accommodation();
            accommodation.Name = _accommodationName;
            accommodation.Location = _location;
            switch(_accommodationType)
            {
                case "House":
                    accommodation.Type = AccommodationType.house;
                    break;
                case "Cabin":
                    accommodation.Type = AccommodationType.cabin;
                    break;
                case "Appartment":
                    accommodation.Type = AccommodationType.appartment;
                    break;
            }
            accommodation.MaxGuests = Convert.ToInt32(_maxGuests);
            accommodation.MinReservationDays = Convert.ToInt32(_minReservationDays);
            accommodation.CancelationPeriod = Convert.ToInt32(_cancelationPeriod);
            _repository.Save(accommodation);
            this.Close();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
