using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service;
using InitialProject.Domain.Model;
using InitialProject.View;
using InitialProject.Utilities;
using System.Windows.Input;
using InitialProject.View.GuideView;
using System.Windows.Controls;
using LiveCharts;
using System.Collections.ObjectModel;
using System.Globalization;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Windows.Forms;

namespace InitialProject.ViewModels
{
    public class TourStatisticViewModel : BaseViewModel
    {
        public TourStatisticService _tourStatisticService;
        public TourService _tourService;
        private readonly LocationService _locationService;
        private readonly ShortTourRequestService _shortTourRequestService;  

        public List<Tour> EndedTours { get; set; }
        public Tour SelectedTour { get; set; }
        public Tour MostVisitedTour { get; set; }
       
        public Tour MostVisitedInYear { get; set; }
        public Dictionary<string, List<string>> Locations { get; set; }
        public ICommand SearchCommand { get; private set; }
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public List<int> Years { get; set; }
        public List<int> VisitationYears { get; set; }
        public List<ShortTourRequest> SearchResult { get; set; }    



        public TourStatisticViewModel()
        {
            _shortTourRequestService = new ShortTourRequestService();
            _tourService = new TourService();
            _tourStatisticService = new TourStatisticService();
            EndedTours = _tourService.FindAllEndedTours();
            MostVisitedTour = _tourService.FindMostVisited();


            ShowVisitation = new RelayCommand(ShowVisitatons);
            _locationService = new LocationService();
            Locations = _locationService.GetCountriesAndCities();
            SearchCommand = new RelayCommand(Search);
            Years = new List<int>() { 2023,2022,2021,2020,0};
            VisitationYears = new List<int>() { 2023, 2022, 2021, 2020 };

            SetLineSeriesForYears();
            Year = 0;
            MostVisitedInYear = _tourService.GetMostVisitedInYear(2023);
            SearchResult = new List<ShortTourRequest>();
        }


        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }

            set
            {
                _seriesCollection = value;
                OnPropertyChanged("SeriesCollection");

            }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
                UpdateGraph();
            }

        }
        private int _selectedYear;
        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
                MostVisitedInYear = _tourService.GetMostVisitedInYear(SelectedYear);
            }

        }
        private int FindRequestNumber(int year)
        {
            List<ShortTourRequest> allTourRequests = _shortTourRequestService.GetAll();
            int i = 0;
            foreach(ShortTourRequest request in allTourRequests)
            {
                if(request.From.Year == year)
                {
                    i++;
                }
            }
            return i;
        }
        private void SetLineSeriesForYears()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(2023,FindRequestNumber(2023)),
                        new ObservablePoint(2022,FindRequestNumber(2022)),
                        new ObservablePoint(2021,FindRequestNumber(2021)),
                        new ObservablePoint(2020,FindRequestNumber(2020))
                    }
                }
            };
        }
        private void UpdateLineSeriesForYear(int year)
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values =new ChartValues<ObservablePoint>
                    {
                        
                       new ObservablePoint(1,_shortTourRequestService.FindMonthRequestNumber(1,year)),
                       new ObservablePoint(2,_shortTourRequestService.FindMonthRequestNumber(2,year)),
                       new ObservablePoint(3,_shortTourRequestService.FindMonthRequestNumber(3,year)),
                       new ObservablePoint(4,_shortTourRequestService.FindMonthRequestNumber(4,year)),
                       new ObservablePoint(5,_shortTourRequestService.FindMonthRequestNumber(5,year)),
                       new ObservablePoint(6,_shortTourRequestService.FindMonthRequestNumber(6,year)),
                       new ObservablePoint(7,_shortTourRequestService.FindMonthRequestNumber(7,year)),
                       new ObservablePoint(8,_shortTourRequestService.FindMonthRequestNumber(8,year)),
                       new ObservablePoint(9,_shortTourRequestService.FindMonthRequestNumber(9,year)),
                       new ObservablePoint(10,_shortTourRequestService.FindMonthRequestNumber(10,year)),
                       new ObservablePoint(11,_shortTourRequestService.FindMonthRequestNumber(11,year)),
                       new ObservablePoint(12,_shortTourRequestService.FindMonthRequestNumber(12,year)),

                    }
                }
                
            };
        }


        private void UpdateLineSeries(int year,List<ShortTourRequest> requests)
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values =new ChartValues<ObservablePoint>
                    {

                       new ObservablePoint(1,_shortTourRequestService.FindSearchMonthRequestNumber(1,year,requests)),
                       new ObservablePoint(2,_shortTourRequestService.FindSearchMonthRequestNumber(2,year,requests)),
                       new ObservablePoint(3,_shortTourRequestService.FindSearchMonthRequestNumber(3,year,requests)),
                       new ObservablePoint(4,_shortTourRequestService.FindSearchMonthRequestNumber(4,year,requests)),
                       new ObservablePoint(5,_shortTourRequestService.FindSearchMonthRequestNumber(5,year,requests)),
                       new ObservablePoint(6,_shortTourRequestService.FindSearchMonthRequestNumber(6,year,requests)),
                       new ObservablePoint(7,_shortTourRequestService.FindSearchMonthRequestNumber(7,year,requests)),
                       new ObservablePoint(8,_shortTourRequestService.FindSearchMonthRequestNumber(8,year,requests)),
                       new ObservablePoint(9,_shortTourRequestService.FindSearchMonthRequestNumber(9,year,requests)),
                       new ObservablePoint(10,_shortTourRequestService.FindSearchMonthRequestNumber(10,year,requests)),
                       new ObservablePoint(11,_shortTourRequestService.FindSearchMonthRequestNumber(11,year,requests)),
                       new ObservablePoint(12,_shortTourRequestService.FindSearchMonthRequestNumber(12,year,requests)),

                    }
                }

            };
        }

        
        private void UpdateGraph()
        {
            if(Year != 0)
            {
                UpdateLineSeriesForYear(Year);
            }
            else
            {
                SetLineSeriesForYears();
            }
        }

        public class TourRequest
        {
            public string Month { get; set; }
            public int Count { get; set; }
        }
        public ICommand ShowVisitation { get; private set; }


        public void ShowVisitatons()
        {
            if (SelectedTour != null)
            {
                Visitation visitation = new Visitation(SelectedTour);
                visitation.Show();
            }
            else
            {
                MessageBox.Show("Izaberite turu za koju zelite da prikazete statistiku!","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
       


        private string _country ="";
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                Cities = Locations[_country];
                OnPropertyChanged(nameof(Country));
            }

        }

        private IEnumerable<string> _cities;

        public IEnumerable<string> Cities
        {
            get { return _cities; }
            set
            {
                _cities = Locations[Country];
                OnPropertyChanged(nameof(Cities));
            }
        }
        private string _city = "";
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }


        private string _language = "";


        public string Languagee
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Languagee));
                }
            }
        }


        private void Search()
        {
            SearchResult.Clear();
            ShortTourRequest shortTourRequest = new ShortTourRequest(Country,City,Languagee);
            foreach (ShortTourRequest request in _shortTourRequestService.GetAll())
            {
                if(request.Country.Equals(shortTourRequest.Country) || /*shortTourRequest.Country.Equals("")*/ shortTourRequest.Country == null)
                {
                    if(request.City.Equals(shortTourRequest.City) || /*shortTourRequest.City.Equals("")*/ shortTourRequest.City == null)
                    {
                        if(request.Language.ToLower().Contains(shortTourRequest.Language.ToLower()) || shortTourRequest.Language.Equals(""))
                        {
                            SearchResult.Add(request);

                        }
                    }
                }
            }
            UpdateLineSeries(Year, SearchResult);
        }


        



    }
}
