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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InitialProject.View;
using InitialProject.Service;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Utilities;
using InitialProject.ViewModels;

namespace InitialProject.ViewModel
{
    public class GuideMainViewModel : BaseViewModel
    {

        public User LoggedUser { get; set; }
        
        private LocationService _locationService;
        private TourService _tourService;
        private CheckPointService _checkPointService;

        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Tour> TodayTours { get; set; }
        public List<Tour> ActiveTours { get; set; }
        public List<Location> locations { get; set; }

        public Tour SelectedTour { get; set; }
        public Tour SelectedTodayTour { get; set; }
        public Tour ActiveTour { get; set; }

        public ICommand ShowAllRatingsCommand => new RelayCommand(ShowRatings);
        public ICommand AddTourCommand => new RelayCommand(AddTour);

        public ICommand StartTourCommand => new RelayCommand(StartTour);
        public ICommand ShowTourCommand => new RelayCommand(ShowTour);
        public ICommand CancelTourCommand => new RelayCommand(CancelTour);
        public ICommand ShowStatisticCommand => new RelayCommand(ShowStatistic);

        public GuideMainViewModel(User user)
        {
            LoggedUser = user;
            _locationService = new LocationService();
            _tourService = new TourService();
            _checkPointService = new CheckPointService();
            LoggedUser = user;
            Tours = UpdateToursDataGrid();
            TodayTours = UpdateTodayToursDataGrid();
            ActiveTours = _tourService.FindActiveTours(user);
            
        }
     
       public void LoadData()
        {
            Tours = UpdateToursDataGrid();
            TodayTours = UpdateTodayToursDataGrid();
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
            var locations = _locationService.GetLocations();
            _tourService.AddTourLocation(tours, locations);
            return new ObservableCollection<Tour>(tours);
        }

        public ObservableCollection<Tour> UpdateTodayToursDataGrid()
        {
            var tours = _tourService.LoadGuideTours(LoggedUser);
            var locations = _locationService.GetLocations();
            List<Tour> todayTours = new List<Tour>();
            _tourService.AddTourLocation(tours, locations);
            todayTours = _tourService.GetTodayTours(LoggedUser);
            return new ObservableCollection<Tour>(todayTours);
        }

        public void StartTour()   
        {

            if (SelectedTodayTour != null && _tourService.FindActiveTours(LoggedUser).Count == 0)
            {
                SelectedTodayTour.IsActive = true;
                List<CheckPoint> checkPoints = SelectedTodayTour.CheckPoints;
                _checkPointService.CheckFirstCheckPoint(checkPoints);
                _tourService.Update(SelectedTodayTour);
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
                if (DateTime.Now.DayOfYear +2 <= SelectedTour.Start.DayOfYear )
                {
                    _tourService.SendVauchers(SelectedTour);
                    _tourService.Delete(SelectedTour);
                    Tours.Remove(SelectedTour);
                    MessageBox.Show("Tura je uspjesno otkazana.","Information", MessageBoxButton.OK,MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Do pocetka ture je ostalo manje od 48h!","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ShowStatistic()  
        {
            TourStatistic tourStatistic = new TourStatistic();
            tourStatistic.Show();
        }

        

        public void ShowRatings()
        {
            EndedTourRatings endedTourRatings = new EndedTourRatings();
            endedTourRatings.Show();
        }
      

       


    }
}
