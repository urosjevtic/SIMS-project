using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class ShowShortTourStatisticsViewModel : BaseViewModel
    {
        public ICommand GoBackCommand { get; set; }

        private ObservableCollection<DataPoint> _toursByLanguage;
        public ObservableCollection<DataPoint> ToursByLanguage
        {
            get { return _toursByLanguage; }
            set
            {
                if (_toursByLanguage != value)
                {
                    _toursByLanguage = value;
                    OnPropertyChanged(nameof(ToursByLanguage));
                }
            }
        }
        private ObservableCollection<DataPoint> _toursByLocation;
        public ObservableCollection<DataPoint> ToursByLocation
        {
            get { return _toursByLocation; }
            set
            {
                if (_toursByLocation != value)
                {
                    _toursByLocation = value;
                    OnPropertyChanged(nameof(ToursByLocation));
                }
            }
        }

        public TourStatisticService _tourStatisticService;

        private string _comboBoxSelection;
        public string ComboBoxSelection
        {
            get { return _comboBoxSelection; }
            set
            {
                if (_comboBoxSelection != value)
                {
                    _comboBoxSelection = value;
                    OnPropertyChanged(nameof(ComboBoxSelection));
                    Refresh(_comboBoxSelection);
                }
            }
        }
        //"System.Windows.Controls.ComboBoxItem: 2023"
        private double _acceptedToursPercentage;
        public double AcceptedToursPercentage
        {
            get { return _acceptedToursPercentage; }
            set
            {
                if (_acceptedToursPercentage != value)
                {
                    _acceptedToursPercentage = value;
                    OnPropertyChanged(nameof(AcceptedToursPercentage));
                }
            }
        }
        private double _unacceptedToursPercentage;
        public double UnacceptedToursPercentage
        {
            get { return _unacceptedToursPercentage; }
            set
            {
                if (_unacceptedToursPercentage != value)
                {
                    _unacceptedToursPercentage = value;
                    OnPropertyChanged(nameof(UnacceptedToursPercentage));
                }
            }
        }
        private double _averageInAccepted;
        public double AverageInAccepted
        {
            get { return _averageInAccepted; }
            set
            {
                if (_averageInAccepted != value)
                {
                    _averageInAccepted = value;
                    OnPropertyChanged(nameof(AverageInAccepted));
                }
            }
        }
        public NavigationService navigationService { get; }
        public ShowShortTourStatisticsViewModel(NavigationService nav)
        {
            this.navigationService = nav;
            GoBackCommand = new RelayCommand(GoBack);
            _tourStatisticService = new TourStatisticService();
            ToursByLanguage = new ObservableCollection<DataPoint>();
            ToursByLocation = new ObservableCollection<DataPoint>();
        }
        public void GoBack()
        {
            navigationService.Navigate(new ShowTourPage(navigationService));
        }
        public void Refresh(string Year)
        {
            ToursByLanguage.Clear();
            ToursByLocation.Clear();
            int year;
            switch (Year)
            {
                case "System.Windows.Controls.ComboBoxItem: 2023":
                    year = 2023;
                    FindStatistics(year);
                    break;
                case "System.Windows.Controls.ComboBoxItem: 2022":
                    year = 2022;
                    FindStatistics(year);
                    break;
                case "System.Windows.Controls.ComboBoxItem: 2021":
                    year = 2021;
                    FindStatistics(year);
                    break;
                case "System.Windows.Controls.ComboBoxItem: 2020":
                    year = 2020;
                    FindStatistics(year);
                    break;
                case "System.Windows.Controls.ComboBoxItem: 2019":
                    year = 2019;
                    FindStatistics(year);
                    break;
                case "System.Windows.Controls.ComboBoxItem: Sva vremena":
                    FindStatisticsAllTimes();
                    break;
            }
        }
        public void FindStatistics(int Year)
        {
            foreach (DataPoint point in _tourStatisticService.FindToursByLanguage(Year))
            {
                ToursByLanguage.Add(point);
            }
            foreach (DataPoint point in _tourStatisticService.FindToursByLocation(Year))
            {
                ToursByLocation.Add(point);
            }
            AcceptedToursPercentage = _tourStatisticService.FindAcceptedToursPercentage(Year);
            UnacceptedToursPercentage = 100 - _tourStatisticService.FindAcceptedToursPercentage(Year);
            AverageInAccepted = _tourStatisticService.FindAverageInAccepted(Year);
        }
        public void FindStatisticsAllTimes()
        {
            foreach (DataPoint point in _tourStatisticService.FindToursByLanguageAllTimes())
            {
                ToursByLanguage.Add(point);
            }
            foreach (DataPoint point in _tourStatisticService.FindToursByLocationAllTimes())
            {
                ToursByLocation.Add(point);
            }
            AcceptedToursPercentage = _tourStatisticService.FindAcceptedToursPercentageAllTimes();
            UnacceptedToursPercentage = 100 - _tourStatisticService.FindAcceptedToursPercentageAllTimes();
            AverageInAccepted = _tourStatisticService.FindAverageInAcceptedAllTimes();
        }
    }
}
