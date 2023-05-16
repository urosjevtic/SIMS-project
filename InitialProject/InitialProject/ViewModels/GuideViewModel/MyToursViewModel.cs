using InitialProject.Domain.Model;
using InitialProject.Repository;
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

namespace InitialProject.ViewModels.GuideViewModel
{
    public class MyToursViewModel :BaseViewModel
    {
        public User LoggedUser { get; set; }

        private readonly ImageRepository imageRepository;
        private LocationService _locationService;
        private TourService _tourService;

        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<ShowingTour> MyTours { get; set; }
        public List<Location> locations { get; set; }

        public ShowingTour SelectedTour { get; set; }

        public Dictionary<string, List<string>> Locations { get; set; }

        public ICommand AddTourCommand => new RelayCommand(AddTour);
        public ICommand CancelTourCommand => new RelayCommand(CancelTour);
        public MyToursViewModel(User user)
        {
            LoggedUser = user;
            _locationService = new LocationService();
            _tourService = new TourService();
            imageRepository = new ImageRepository();
            Locations = _locationService.GetCountriesAndCities();

            Tours = UpdateToursDataGrid();
            MyTours = new ObservableCollection<ShowingTour>();
            GetAllShowingTours();
        }

        private void GetAllShowingTours()
        {
            foreach(Tour tour in _tourService.GetAll())
            {
               foreach(DateTime start in tour.StartDates)
                {
                    if(start.DayOfYear >= DateTime.Now.DayOfYear)
                    {
                        ShowingTour showingTour = new ShowingTour();
                        showingTour.Start = start;
                        MakeShowingTour(tour,showingTour);
                        MyTours.Add(showingTour);
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

       

        private string MakeCoverImage(Tour tour)
        {
            Image tourImage = imageRepository.GetById(tour.CoverImageUrl.Id);

            string url = tourImage.Url[0];
           
            return url;
        }
        public ObservableCollection<Tour> UpdateToursDataGrid()
        {
            var tours = _tourService.LoadGuideTours(LoggedUser);
            var locations = _locationService.GetLocations();
            _tourService.AddTourLocation(tours, locations);
            return new ObservableCollection<Tour>(tours);
        }

        public void CancelTour()
        {
            if (SelectedTour != null)
            {
                
                    if(DateTime.Now.DayOfYear +2 <= SelectedTour.Start.DayOfYear)
                    {
                    Tour tour = GetTourByShowingTour(SelectedTour);
                        _tourService.SendVauchers(tour);
                        _tourService.Delete(tour);
                        Tours.Remove(tour);
                        MessageBox.Show("Tura je uspjesno otkazana.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Do pocetka ture je ostalo manje od 48h!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                
                
                
            }
        }
        private Tour GetTourByShowingTour(ShowingTour showingTour)
        {
            Tour Tour = new Tour();
            foreach(Tour tour in _tourService.GetAll())
            {
                if(tour.Name == showingTour.Name && tour.Duration == showingTour.Duration)
                {
                    Tour = tour;
                }
            }
            return Tour;
        }
        public void AddTour()
        {
            ShortTourRequest request = new ShortTourRequest();
            AddingTour addingTour = new AddingTour(LoggedUser,request, true);
            addingTour.Show();
            //CreatingTourByStatistics creatingTourByStatistic = new CreatingTourByStatistics(LoggedUser);
            //creatingTourByStatistic.ShowDialog();
        }
    }
}
