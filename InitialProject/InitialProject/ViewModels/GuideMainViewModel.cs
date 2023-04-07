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
using InitialProject.Serializer;
using InitialProject.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InitialProject.View;
using InitialProject.Service;

namespace InitialProject.ViewModel
{
    public class GuideMainViewModel
    {

        public User LoggedUser { get; set; }
        public List<Tour> ActiveTours { get; set; }
        public TourRepository _tourRepository { get; set; }
        public LocationService _locationService { get; set; } 
        public TourService _tourService { get; set; }
        public LocationRepository _locationRepository { get; set; }   
        public CheckPointRepository _checkPointRepository { get; set; }
        public List<Tour> tours;
        public List<Location> locations;

        public Tour SelectedTour { get; set; }
        public Tour SelectedTodayTour { get; set; }
        public Tour ActiveTour { get; set; }

        // DANAS TURE 
        public List<Tour> TodayTours { get; set; }

        public GuideMainViewModel(User user)
        {
            LoggedUser = user;
            _locationRepository = new LocationRepository();
            _tourRepository = new TourRepository();
            _locationService = new LocationService();
            _tourService = new TourService();
            _checkPointRepository = new CheckPointRepository();
            LoadData();
        }
        public void LoadData()
        {
            tours = _tourService.LoadGuideTours(LoggedUser);
            locations = _locationService.LoadLocations();
            _tourService.AddTourLocation(tours, locations);
            TodayTours = _tourService.GetTodayTours(LoggedUser);
            ActiveTours = _tourService.FindActiveTours(LoggedUser);
        }
       
        public void AddTour()
        {
            AddingTour addingTour = new AddingTour(LoggedUser);
            addingTour.Show();
        }


        public ObservableCollection<Tour> UpdateToursDataGrid()
        {
            var tours = _tourService.LoadGuideTours(LoggedUser);
            var locations = _locationService.LoadLocations();
            _tourService.AddTourLocation(tours, locations);
            return new ObservableCollection<Tour>(tours);
        }

        public ObservableCollection<Tour> UpdateTodayToursDataGrid()
        {
            var tours = _tourService.LoadGuideTours(LoggedUser);
            var locations = _locationService.LoadLocations();
            List<Tour> todayTours = new List<Tour>();
            _tourService.AddTourLocation(tours, locations);
            todayTours = _tourService.GetTodayTours(LoggedUser);
            return new ObservableCollection<Tour>(todayTours);
        }

        public void StartTour()
        {

            if (SelectedTodayTour != null && !SelectedTodayTour.IsActive)
            {
                SelectedTodayTour.IsActive = true;
                List<CheckPoint> checkPoints = SelectedTodayTour.CheckPoints;
                foreach(CheckPoint cp in checkPoints)
                {
                    if(cp.SerialNumber == 1)
                    {
                        cp.IsChecked = true;
                        _checkPointRepository.Update(cp);
                    }
                }
                _tourRepository.Update(SelectedTodayTour);
                StartedTour startedTour = new StartedTour(SelectedTodayTour);
                startedTour.Show();
            }
            else
            {
                MessageBox.Show("Vec postoji zapoceta tura!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void ShowTour()
        {
            if (ActiveTour != null)
            {
                StartedTour startedTour = new StartedTour(ActiveTour);
                startedTour.Show();
            }
        }
        public void CancelTour()
        {
            if (SelectedTour != null)
            {
                if (DateTime.Now.Hour <= SelectedTour.Start.Hour)
                {
                    _tourService.SendVauchers(SelectedTour);
                    _tourService.Delete(SelectedTour);
                    MessageBox.Show("Tura je uspjesno otkazana.","Information", MessageBoxButton.OK,MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Do pocetka ture je ostalo manje od 48h!","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
