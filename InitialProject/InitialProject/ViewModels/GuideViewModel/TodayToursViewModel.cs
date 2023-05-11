using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.Utilities;
using InitialProject.View;
using InitialProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InitialProject.View.GuideView.Pages;
using InitialProject.Repository;

namespace InitialProject.ViewModels.GuideViewModel
{
    public class TodayToursViewModel : BaseViewModel
    {
        public User LoggedUser { get; set; }

        private LocationService _locationService;
        private TourService _tourService;
        private CheckPointService _checkPointService;
        private readonly ImageRepository _imageRepository;

        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<ShowingTour> TodayTours { get; set; }
        public List<Location> locations { get; set; }

        public ShowingTour SelectedTour { get; set; }

        public ICommand StartTourCommand => new RelayCommand(StartTour);

        public MainWindow _guideMain { get; set; }

        public TodayToursViewModel(User user,MainWindow guideMainWindow)
        {

            LoggedUser = user;
            _locationService = new LocationService();
            _imageRepository = new ImageRepository();
            _tourService = new TourService();
            _checkPointService = new CheckPointService();
            LoggedUser = user;
            Tours = UpdateTodayToursDataGrid();
            _guideMain = guideMainWindow;
            TodayTours = new ObservableCollection<ShowingTour>();
            GetAllShowingTours();
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

        private void GetAllShowingTours()
        {
            foreach (Tour tour in _tourService.GetAll())
            {
                foreach (DateTime start in tour.StartDates)
                {
                    if (start.DayOfYear == DateTime.Now.DayOfYear)
                    {
                        ShowingTour showingTour = new ShowingTour();
                        showingTour.Start = start;
                        MakeShowingTour(tour, showingTour);
                        TodayTours.Add(showingTour);
                    }
                }
            }
        }
        private void MakeShowingTour(Tour tour, ShowingTour showingTour)
        {
            showingTour.Name = tour.Name;
            showingTour.Description = tour.Description;
            showingTour.Location = _locationService.GetById(tour.Location.Id).ToString();
            showingTour.Duration = tour.Duration;
            showingTour.CheckPoints = tour.CheckPoints;
            showingTour.CoverImageUrl = MakeCoverImage(tour);
            showingTour.Language = tour.Language;
        }

        public void StartTour()
        {

            if (SelectedTour != null && _tourService.FindActiveTours(LoggedUser).Count == 0)
            {
                var navigationService = _guideMain.MainFrame.NavigationService;
                Tour tour = GetTourByShowingTour(SelectedTour);
                tour.IsActive = true;
                List<CheckPoint> checkPoints = SelectedTour.CheckPoints;
                _checkPointService.CheckFirstCheckPoint(checkPoints);
                _tourService.Update(tour);
                navigationService.Navigate(new ActiveTourPage(LoggedUser));    
            }
            else
            {
                MessageBox.Show("Vec postoji zapoceta tura!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Tour GetTourByShowingTour(ShowingTour showingTour)
        {
            Tour Tour = new Tour();
            foreach (Tour tour in _tourService.GetAll())
            {
                if (tour.Name == showingTour.Name && tour.Duration == showingTour.Duration)
                {
                    Tour = tour;
                }
            }
            return Tour;
        }

        private string MakeCoverImage(Tour tour)
        {
            Image tourImage = _imageRepository.GetById(tour.CoverImageUrl.Id);

            string url = tourImage.Url[0];

            return url;
        }
    }
}
