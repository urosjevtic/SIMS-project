﻿using InitialProject.Forms;
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
        public AccommodationType SelectedType;
        /*------------------------------------------------*/

        public AccommodationSearch()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationRepository= new AccommodationRepository();
            _locationRepository = new LocationRepository();
            loadData();
            AccommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(accommodations);
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
            AccommodationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(accommodations);
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
            loadData();
            //AccommodationList.ItemsSource = null;

            var filteredList = new ObservableCollection<Accommodation>();

           
           foreach(Accommodation accommodation in accommodations)
            {
                if (tbAccommodationName.Text != "")
                {
                    if (accommodation.Name.ToLower().Contains((tbAccommodationName.Text).ToLower()))
                    {
                        filteredList.Add(accommodation);
                    }
                }
                if (tbCityName.Text != "")
                {
                    if (accommodation.Location.City.ToLower().Contains(tbCityName.Text.ToLower()))
                    {
                        if(!filteredList.Contains(accommodation))
                        filteredList.Add(accommodation);
                    }
                }
                if (tbCountryName.Text != "")
                {
                    if (accommodation.Location.Country.ToLower().Contains(tbCountryName.Text))
                    {
                        if (!filteredList.Contains(accommodation))
                            filteredList.Add(accommodation);
                    }
                }
                if(cbAccommodationType.Text != "")
                {
                    if (accommodation.Type.ToString() ==cbAccommodationType.Text)
                    {
                        if (!filteredList.Contains(accommodation))
                            filteredList.Add(accommodation);
                    }
                }
                if (tbGuestNumber.Text != "")
                {
                    if(accommodation.MaxGuests >= int.Parse(tbGuestNumber.Text))
                    {
                        if (!filteredList.Contains(accommodation))
                            filteredList.Add(accommodation);
                    }
                }
                if (tbReservationDays.Text != "")
                {
                    if (accommodation.MinReservationDays >= int.Parse(tbReservationDays.Text))
                    {
                        if (!filteredList.Contains(accommodation))
                            filteredList.Add(accommodation);
                    }
                }
            }
            AccommodationDataGrid.ItemsSource = filteredList;       
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
               // CommentForm viewCommentForm = new CommentForm(SelectedComment);
               // viewCommentForm.Show();
                AccommodationReservation accommodationReservation = new AccommodationReservation(SelectedAccommodation);
                accommodationReservation.Show();
            }
        }
    }
}
