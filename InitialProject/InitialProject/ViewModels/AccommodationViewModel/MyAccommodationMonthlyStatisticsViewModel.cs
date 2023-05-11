using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.Service.StatisticService;
using InitialProject.Utilities;
using InitialProject.View.OwnerView.MyAccommodations;

namespace InitialProject.ViewModels.AccommodationViewModel
{
    public class MyAccommodationMonthlyStatisticsViewModel : BaseViewModel
    {

        private readonly MonthlyAccommodationStatisticService _monthlyAccommodationStatisticService;
        private readonly AccommodationService _accommodationService;
        private readonly DateTime _selectedYear;
        private readonly int _accommodationId;
        private readonly User _logedInUser;
        private readonly List<DateTime> _availableMonths;
        public Accommodation Accommodation { get; }
        public NavigationService NavigationService { get; set; }
        public MyAccommodationMonthlyStatisticsViewModel(DateTime selectedYear, int accommodationId, User logedInUser, NavigationService navigationService)
        {
            _selectedYear = selectedYear;
            _accommodationId = accommodationId;
            _logedInUser = logedInUser;
            _monthlyAccommodationStatisticService = new MonthlyAccommodationStatisticService();
            _accommodationService = new AccommodationService();
            Accommodation = _accommodationService.GetById(_accommodationId);
            NavigationService = navigationService;
        }


        public string SelectedYearText
        {
            get { return _selectedYear.ToString("yyyy"); }
        }

        public List<string> AvailableMonths
        {
            get
            {
                List<string> returnList = new List<string>();
                foreach(var availableMonth in _monthlyAccommodationStatisticService.GetAvailableMonths(_accommodationId, _selectedYear.Year))
                {
                    switch (availableMonth.Month)
                    {
                        case 1:
                            returnList.Add("January");
                            break;
                        case 2:
                            returnList.Add("February");
                            break;
                        case 3:
                            returnList.Add("March");
                            break;
                        case 4:
                            returnList.Add("April");
                            break;
                        case 5:
                            returnList.Add("May");
                            break;
                        case 6:
                            returnList.Add("Jun");
                            break;
                        case 7:
                            returnList.Add("July");
                            break;
                        case 8:
                            returnList.Add("August");
                            break;
                        case 9:
                            returnList.Add("September");
                            break;
                        case 10:
                            returnList.Add("October");
                            break;
                        case 11:
                            returnList.Add("November");
                            break;
                        case 12:
                            returnList.Add("December");
                            break;
                    }


                }
                return returnList;
            }
        }

        public string MostOccupiedMonth
        {
            get
            {
                int mostOccupiedMonth = _monthlyAccommodationStatisticService.GetMostOccupiedMonth(_accommodationId, _selectedYear.Year);

                if (mostOccupiedMonth >= 1 && mostOccupiedMonth <= 12)
                {
                    return new DateTime(_selectedYear.Year, mostOccupiedMonth, 1).ToString("MMMM");
                }

                return "No reservations";
            }
        }


        private int _reservationCount;

        public int ReservationCount
        {
            get { return _reservationCount; }
            set
            {
                _reservationCount = value;
                OnPropertyChanged("ReservationCount");
            }
        }

        private int _renovationCount;

        public int RenovationCount
        {
            get { return _renovationCount; }
            set
            {
                _renovationCount = value;
                OnPropertyChanged("RenovationCount");
            }
        }

        private int _rescheduleCount;

        public int RescheduleCount
        {
            get { return _rescheduleCount; }
            set
            {
                _rescheduleCount = value;
                OnPropertyChanged("RescheduleCount");
            }
        }



        private int _cancelationCount;

        public int CancelationCount
        {
            get { return _cancelationCount; }
            set
            {
                _cancelationCount = value;
                OnPropertyChanged("CancelationCount");
            }
        }
        private string _month;

        public string Month
        {
            get { return _month; }
            set
            {
                _month = value;
                ReservationCount = _monthlyAccommodationStatisticService.GetMonthlyStatistics(_month, _selectedYear.Year, _accommodationId).ReservationsCount;
                CancelationCount = _monthlyAccommodationStatisticService.GetMonthlyStatistics(_month, _selectedYear.Year, _accommodationId).CancelationsCount;
                RenovationCount = _monthlyAccommodationStatisticService.GetMonthlyStatistics(_month, _selectedYear.Year, _accommodationId).RenovationsCount;
                RescheduleCount = _monthlyAccommodationStatisticService.GetMonthlyStatistics(_month, _selectedYear.Year, _accommodationId).ReschedulesCount;

                OnPropertyChanged("Month");
            }
        }


        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            NavigationService.Navigate(new MyAccommodationYearlyStatisticView(_accommodationId, _logedInUser, NavigationService));
        }

    }
}
