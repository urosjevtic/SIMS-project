﻿using System;
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
using InitialProject.Serializer;
using InitialProject.Repository;
using System.Collections.ObjectModel;


namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GuideMainWindow : Window
    {
        public User LoggedUser { get; set; }
        public List<Tour> ActiveTours { get; set; }
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        public List<Tour> tours;
        public List<Location> locations;

        public Tour SelectedTodayTour { get; set; }
        public Tour ActiveTour { get; set; }

        // DANAS TURE 
        public List<Tour> TodayTours { get; set; }

        public GuideMainWindow(User user)
        {
            InitializeComponent();
            this.DataContext = this;

            LoggedUser = user;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            LoadData();
            ToursDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
            ActiveTour = SelectedTodayTour;
            TodayToursDataGrid.ItemsSource = new ObservableCollection<Tour>(TodayTours);
            ActiveToursDataGrid.ItemsSource = new ObservableCollection<Tour>(ActiveTours);

        }

        private void LoadData()
        {
            tours = LoadGuideTours();
            locations = LoadLocations();
            AddTourLocation(tours, locations);
            TodayTours = GetTodayTours();
            ActiveTours = FindActiveTours();
        }
        public List<Tour> GetTodayTours()
        {
            var tours = LoadGuideTours();
            var locations = LoadLocations();
            List<Tour> todayTours = new List<Tour>();
            AddTourLocation(tours, locations);
            foreach (Tour tour in tours)
            {
                if (tour.Start.Date == DateTime.Today.Date)
                {
                    todayTours.Add(tour);
                }
            }
            return todayTours;
        }
        private List<Location> LoadLocations()
        {
            return _locationRepository.GetAll();
        }


        private List<Tour> LoadGuideTours() // za svakog vodica da ucita njegove ture
        {
            List<Tour> allTours = new List<Tour>();
            allTours = _tourRepository.GetAll();
            tours = new List<Tour>();
            foreach (Tour tour in allTours)
            {
                if (tour.Guide.Id == LoggedUser.Id)
                {
                    tours.Add(tour);
                }
            }
            return tours;
        }
        private void AddTourClick(object sender, RoutedEventArgs e)
        {
            AddingTour addingTour = new AddingTour(this, LoggedUser);
            addingTour.Show();

        }


        public void UpdateToursDataGrid()
        {
            var tours = LoadGuideTours();
            var locations = LoadLocations();
            AddTourLocation(tours, locations);
            ToursDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
        }

        public void UpdateTodayToursDataGrid()
        {
            var tours = LoadGuideTours();
            var locations = LoadLocations();
            List<Tour> todayTours = new List<Tour>();
            AddTourLocation(tours, locations);
            todayTours = GetTodayTours();
            TodayToursDataGrid.ItemsSource = new ObservableCollection<Tour>(todayTours);
        }


        public void AddTourLocation(List<Tour> tours, List<Location> locations)  //veza lokacije i ture
        {
            foreach (var tour in tours)
            {
                foreach (var location in locations)
                {
                    if (location.Id == tour.Location.Id)
                    {
                        tour.Location = location;

                        break;
                    }
                }
            }
        }
        public List<Tour> FindActiveTours()
        {

            var tours = LoadGuideTours();
            var locations = LoadLocations();
            List<Tour> active = new List<Tour>();
            AddTourLocation(tours, locations);
            foreach (Tour tour in tours)
            {
                if (tour.IsActive)
                {
                    active.Add(tour);
                }
            }
            return active;
        }

        private bool FindToursActivity(List<Tour> tours)
        {
            foreach (Tour tour in tours)
            {
                if (tour.IsActive == true)
                {
                    return true;
                }
            }
            return false;
        }
        public void StartTourClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTodayTour != null && (FindToursActivity(LoadGuideTours()) == false))
            {
                SelectedTodayTour.IsActive = true;
                _tourRepository.Update(SelectedTodayTour);
                StartedTour startedTour = new StartedTour(SelectedTodayTour, this);
                startedTour.Show();
            }
            else
            {
                MessageBox.Show("Vec postoji zapoceta tura!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowTourClick(object sender, RoutedEventArgs e)
        {
            CheckedCheckPointRepository ccpr = new CheckedCheckPointRepository();
            List<CheckedCheckPoint> listCCP = ccpr.GetAll();
            ActiveTour activeTour = new ActiveTour(ActiveTour, listCCP);
            activeTour.Show();
        }

    }
}
