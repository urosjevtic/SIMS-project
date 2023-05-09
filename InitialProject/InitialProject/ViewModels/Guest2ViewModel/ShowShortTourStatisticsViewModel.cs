using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Service;

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
        //public List<DataPoint> toursByLanguage { get; set; }
        //public List<DataPoint> toursByLocation { get; set; }

        public TourStatisticService _tourStatisticService;

        private int _comboBoxSelection;
        public int ComboBoxSelection
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
        public ShowShortTourStatisticsViewModel()
        {
            GoBackCommand = new RelayCommand(CloseCurrentWindow);
            _tourStatisticService = new TourStatisticService();
            ToursByLanguage = new ObservableCollection<DataPoint>();
            ToursByLocation = new ObservableCollection<DataPoint>();
        }
        public void Refresh(int Year)
        {
            ToursByLanguage.Clear();
            ToursByLocation.Clear(); 
            foreach(DataPoint point in _tourStatisticService.FindToursByLanguage(Year))
            {
                ToursByLanguage.Add(point);
            }
            foreach(DataPoint point in _tourStatisticService.FindToursByLocation(Year))
            {
                ToursByLocation.Add(point);
            }
            AcceptedToursPercentage = _tourStatisticService.FindAcceptedToursPercentage(Year);
            UnacceptedToursPercentage = 100 - _tourStatisticService.FindAcceptedToursPercentage(Year);
            AverageInAccepted = _tourStatisticService.FindAverageInAccepted(Year);
        }
    }
}
