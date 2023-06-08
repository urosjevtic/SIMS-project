using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service;
using InitialProject.View.GuideView;
using InitialProject.Utilities;
using InitialProject.Domain.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using InitialProject.Repository;

namespace InitialProject.ViewModels.GuideViewModel
{
    public class GuideTourReport : BaseViewModel
    {
        private TourService _tourService;
        private LocationService _locationService;
        private ImageRepository imageRepository;
        public ObservableCollection<Tour> Tours { get; set; }   
        public ObservableCollection<ShowingTour> ShowingTours { get; set; }
        public User LoggedInUser { get; set; } = App.LoggedUser; 
        public Tour SelectedTour { get; set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand GenerateReportCommand { get; private set; }
        public GuideTourReport()
        {
            _tourService = new TourService();
            _locationService = new LocationService();
            imageRepository = new ImageRepository();
            Tours = new ObservableCollection<Tour>();
            ShowingTours = new ObservableCollection<ShowingTour>();
            SearchCommand = new RelayCommand(SearchTours);
            GenerateReportCommand = new RelayCommand(GenerateReport);
            From = DateTime.Now;
            To = DateTime.Now;
            //GetAllShowingTours();
        }
        private DateTime _from;
        public DateTime From
        {
            get => _from;
            set
            {
                if (value != _from)
                {
                    _from = value;
                    OnPropertyChanged(nameof(From));
                }
            }
        }

        private DateTime _to;
        public DateTime To
        {
            get => _to;
            set
            {
                if (value != _to)
                {
                    _to = value;
                    OnPropertyChanged(nameof(To));
                }
            }
        }


        private void SearchTours()
        {
            if(From != null && To != null)
            {
                if(From.DayOfYear <= To.DayOfYear)
                {
                    GetAllShowingTours();
                }
                else
                {
                    MessageBox.Show("Date range is not valid!");
                }
                
                GetAllShowingTours();
            }
            else
            {
                MessageBox.Show("Please, choose date range!");
            } 
            
        }

        private void GetAllShowingTours()
        {
            ShowingTours.Clear();
            foreach (Tour tour in _tourService.GetAll())
            {
                foreach (DateTime start in tour.StartDates)
                {
                    if (start.Year >= From.Year && start.Year <= To.Year)
                    {
                        if (start.DayOfYear >= From.DayOfYear && start.DayOfYear <= To.DayOfYear)
                        {
                            ShowingTour showingTour = new ShowingTour();
                            showingTour.Start = start;
                            MakeShowingTour(tour, showingTour);
                            ShowingTours.Add(showingTour);
                        }
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
            showingTour.MaxGuests = tour.MaxGuests;
        }



        private string MakeCoverImage(Tour tour)
        {
            Image tourImage = imageRepository.GetById(tour.CoverImageUrl.Id);

            string url = tourImage.Url[0];

            return url;
        }
      
        private void GenerateReport()
        {
            List<ShowingTour> tours = new List<ShowingTour>();        
            foreach(ShowingTour showingTour in ShowingTours)
            {
                tours.Add(showingTour);
            }
            PDFService.GenerateGuideReportPDF(tours,From,To);
        }

    }
}
