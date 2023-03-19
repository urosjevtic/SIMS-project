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
using System.Xaml.Schema;

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
        //=======================================
        private List<Location> LoadLocations()
        {
            return _locationRepository.GetAll();
        }


       public void UpdateDataGrid()
        {
            var accommodations = _accommodationRepository.GetAll();
            var locations = LoadLocations();
            AddAccommodationLocation(accommodations, locations);
            AccommodationList.ItemsSource = new ObservableCollection<Accommodation>(accommodations);
        }
        
        public void AddAccommodationLocation(List<Accommodation>accommodations, List<Location> locations)   //veza smestaja i lokacije
        {
            foreach(var accommodation in accommodations)
            {
                foreach(var location in locations)
                {
                    if (location.Id == accommodation.Location.Id)
                    {
                        accommodation.Location = location;
                        break;
                    }
                }
            }
        }
        private void loadData()
        {
            accommodations = _accommodationRepository.GetAll();
            locations = LoadLocations();
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

        //=======================================

       /* private void loadData()
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
       */


        private void SerchButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationList.ItemsSource = null;
        
        
            //List<Accommodation>accommodations= new List<Accommodation>();
            //accommodations = _accommodationRepository.GetAll();  //OVO MENI IZBACI LOKACIJU U ISPISU

          /*   foreach(Accommodation accommodation in accommodations)
             {
                 foreach(Location location in locations)
                 {
                    if (tbAccommodationName.Text != null)
                    {
                        var searchedAccommodation = accommodations.Where(accommodation => accommodation.Name.ToLower().Contains((tbAccommodationName.Text).ToLower()));

                        if (tbCityName.Text != null)
                        {
                            searchedAccommodation = accommodations.Where(accommodation => accommodation.Location.City.ToLower().Contains(tbCityName.Text.ToLower()));

                            if (tbCountryName != null)
                            {
                               searchedAccommodation = accommodations.Where(accommodation => accommodation.Location.Country.ToLower().Contains(tbCountryName.Text));
                               
                            }
                        }
                        AccommodationList.ItemsSource = searchedAccommodation;
                    }
       

                 }
             }*/

             if (tbAccommodationName.Text != null)
                 {
                     foreach (Accommodation accommodation in accommodations)
                     {
                         foreach (Location location in locations)
                         {
                            var searchedAccommodation = accommodations.Where(accommodation => accommodation.Name.ToLower().Contains((tbAccommodationName.Text).ToLower()));
                             AccommodationList.ItemsSource = searchedAccommodation;
                          }
                     }
                 }



            /*if(tbCityName.Text != null)
            {
                foreach (Accommodation accommodation in accommodations)
                {
                    var searchedAccommodation = accommodations.location.Where(location => location.City.ToLower().Contains(tbCityName.Text));
                }
            }*/
            /* if (tbCityName.Text != null)
             {
                 foreach (Accommodation accommodation in accommodations)
                 {
                     foreach (Location location in locations)
                     {
                         var searchedAccommodation = accommodations.Where(accommodation => accommodation.Location.City.ToLower().Contains(tbCityName.Text.ToLower()));
                          AccommodationList.ItemsSource = searchedAccommodation;

                     }
                 }
             }

                     if (tbCountryName.Text != null)
                     {
                         foreach (Accommodation accommodation in accommodations)
                         {
                             foreach (Location location in locations)
                             {
                                 var searchedAccommodation = accommodations.Where(accommodation => accommodation.Location.Country.ToLower().Contains(tbCountryName.Text));
                               // AccommodationList.ItemsSource = searchedAccommodation;
                             }
                         }
                     }*/


            /*   if(cbAccommodationType.Text != null)
               {
                   foreach (Accommodation accommodation in accommodations) 
                   {
                       foreach (Location location in locations)
                       {
                           if (accommodation.Type == AccommodationType.appartment)
                           {
                               if (accommodation.Type.ToString() == cbAccommodationType.SelectedItem)
                               {
                                   var searchedAccommodation = accommodations.Where(accommodation => accommodation.Location.City.ToLower().Contains(tbCityName.Text.ToLower()));
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
               }

               if (tbGuestNumber.Text != "")
               {
                   foreach (Accommodation accommodation in accommodations)
                   {
                       var searchedAccommodation = accommodations.Where(accommodation => accommodation.MaxGuests >= int.Parse(tbGuestNumber.Text));
                       AccommodationList.ItemsSource = searchedAccommodation;
                   }
               }
               if (tbReservationDays.Text != "")
               {
                   foreach (Accommodation accommodation in accommodations)
                   {
                       var searchedAccommodation = accommodations.Where(accommodation => accommodation.MinReservationDays >= int.Parse(tbReservationDays.Text));
                   }
               }*/
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
               // CommentForm viewCommentForm = new CommentForm(SelectedComment);
               // viewCommentForm.Show();
                AccommodationReservation accommodationReservation = new AccommodationReservation();
                accommodationReservation.Show();
            }
        }
    }
}
