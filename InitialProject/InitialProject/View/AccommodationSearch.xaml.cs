using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationSearch.xaml
    /// </summary>
    public partial class AccommodationSearch : Window
    {


        
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;

        public List<Accommodation> accommodations;
        public List<Location> locations;

        /*-------------------------------------------------*/
        //OVO MI TREBA ZA SELEKTOVANJE!!!!!!!!!
        public Accommodation SelectedAccommodation { get; set; }

        /*------------------------------------------------*/

        public AccommodationSearch()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationRepository= new AccommodationRepository();
            _locationRepository = new LocationRepository();
            loadData();
            AccommodationList.ItemsSource = new ObservableCollection<Accommodation>(accommodations);
        }


        private void loadData()
        {
            accommodations = loadAccommodations();
            locations = new List<Location>();
            locations = _locationRepository.GetAll();
            foreach (Accommodation accommodation in accommodations)
            {
                foreach (Location location in locations)
                {
                    if (location.Id == accommodation.Location.Id)
                    {
                        accommodation.Location = location;
                        break;
                    }
                }
            }
        }

        private List<Accommodation> loadAccommodations()
        {
            List<Accommodation> allAccommodations = new List<Accommodation>();
            allAccommodations = _accommodationRepository.GetAll();
            accommodations = new List<Accommodation>();
            foreach (Accommodation accommodation in allAccommodations)
            {
                    accommodations.Add(accommodation);
            }
            return accommodations;
        }


        /*private void loadIntoListView(Accommodation accommodation)
        {
            AccommodationList.Items.Add(new {Name = accommodation.Name, accommodation.Location, accommodation.Location, accommodation.Type, accommodation.MaxGuests, accommodation.MinReservationDays});
        }

        private List<Accommodation> getAccommodation()
        {
            AccommodationList.Items.Clear();
            return AccommodationSearch;
        }*/



        private void SerchButton_Click(object sender, RoutedEventArgs e)
        {
            //AccommodationList.Items.Clear();
            AccommodationList.ItemsSource = null;
            List<Accommodation>accommodations= new List<Accommodation>();
            accommodations = _accommodationRepository.GetAll();  //GRESKAAAA

            if (tbAccommodationName.Text != null)
            {
                foreach(Accommodation accommodation in accommodations) 
                { 
                    if(accommodation.Name.ToLower().Contains(tbAccommodationName.Text.ToLower()))
                    {
                        //AccommodationList.Items.Add(new { Name = accommodation.Name, accommodation.Location.City, accommodation.Location.Country, accommodation.Type, accommodation.MaxGuests, accommodation.MinReservationDays }); ;
                        //accommodations.Add(accommodation);
                        //AccommodationList.Items.Add(accommodation);
                       // var a=accommodation;
                        AccommodationList.ItemsSource = accommodations;
                    }
                }

            }
            if(tbCityName.Text != null)
            {
                foreach (Accommodation accommodation in accommodations)
                {
                    if (accommodation.Location.City.ToLower().Contains(tbCityName.Text.ToLower()))
                    {
                        // AccommodationList.Items.Add(new { Name = accommodation.Name, accommodation.Location.City, accommodation.Location.Country, accommodation.Type, accommodation.MaxGuests, accommodation.MinReservationDays }); ;
                        AccommodationList.ItemsSource = accommodations;
                    }
                }
            }
            if (tbCountryName.Text != null)
            {
                foreach (Accommodation accommodation in accommodations)
                {
                    if (accommodation.Location.Country.ToLower().Contains(tbCountryName.Text.ToLower()))
                    {
                        AccommodationList.Items.Add(new { Name = accommodation.Name, accommodation.Location.City, accommodation.Location.Country, accommodation.Type, accommodation.MaxGuests, accommodation.MinReservationDays });
                    }
                }
            }
            if(cbAccommodationType.Text != null)
            {
                foreach (Accommodation accommodation in accommodations) 
                {
                    if(accommodation.Type==AccommodationType.appartment)
                    {
                        if (accommodation.Type.ToString() == cbAccommodationType.SelectedItem)
                        {
                            AccommodationList.Items.Add(new { Name = accommodation.Name, accommodation.Location.City, accommodation.Location.Country, accommodation.Type, accommodation.MaxGuests, accommodation.MinReservationDays });
                        }
                    }
                    if (accommodation.Type == AccommodationType.house)
                    {
                        if (accommodation.Type.ToString() == cbAccommodationType.SelectedItem)
                        {
                            AccommodationList.Items.Add(new { Name = accommodation.Name, accommodation.Location.City, accommodation.Location.Country, accommodation.Type, accommodation.MaxGuests, accommodation.MinReservationDays });
                        }
                    }
                    if (accommodation.Type == AccommodationType.cabin)
                    {
                        if (accommodation.Type.ToString() == cbAccommodationType.SelectedItem)
                        {
                            AccommodationList.Items.Add(new { Name = accommodation.Name, accommodation.Location.City, accommodation.Location.Country, accommodation.Type, accommodation.MaxGuests, accommodation.MinReservationDays });
                        }
                    }
                }
            }

            if (tbGuestNumber.Text != null)
            {
                foreach (Accommodation accommodation in accommodations)
                {
                    if (accommodation.MaxGuests>=Convert.ToInt32(tbGuestNumber.Text))
                    {
                        AccommodationList.Items.Add(new { Name = accommodation.Name, accommodation.Location.City, accommodation.Location.Country, accommodation.Type, accommodation.MaxGuests, accommodation.MinReservationDays }); ;
                    }
                }
            }
            if (tbReservationDays.Text != null)
            {
                foreach (Accommodation accommodation in accommodations)
                {
                    if (accommodation.MinReservationDays <= Convert.ToInt32(tbReservationDays.Text))
                    {
                        AccommodationList.Items.Add(new { Name = accommodation.Name, accommodation.Location.City, accommodation.Location.Country, accommodation.Type, accommodation.MaxGuests, accommodation.MinReservationDays }); ;
                    }
                }
            }
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
           /* if (SelectedAccommodation != null)
            {
                CommentForm viewCommentForm = new CommentForm(SelectedComment);
                viewCommentForm.Show();
            }*/
        }
    }
}
