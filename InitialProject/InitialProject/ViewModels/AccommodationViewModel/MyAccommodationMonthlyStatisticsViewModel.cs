using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public Accommodation Accommodation { get; }
        public MyAccommodationMonthlyStatisticsViewModel(DateTime selectedYear, int accommodationId, User logedInUser)
        {
            _selectedYear = selectedYear;
            _accommodationId = accommodationId;
            _logedInUser = logedInUser;
            _monthlyAccommodationStatisticService = new MonthlyAccommodationStatisticService();
            _accommodationService = new AccommodationService();
            Accommodation = _accommodationService.GetById(_accommodationId);
        }


        public string SelectedYearText
        {
            get { return _selectedYear.ToString("yyyy"); }
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
        private string _month = "January";

        public string Month
        {
            get { return _month; }
            set
            {
                _month = value;
                ReservationCount = _monthlyAccommodationStatisticService.GetMonthlyStatistics(_month, _selectedYear, _accommodationId).ReservationsCount;
                CancelationCount = _monthlyAccommodationStatisticService.GetMonthlyStatistics(_month, _selectedYear, _accommodationId).CancelationsCount;
                RenovationCount = _monthlyAccommodationStatisticService.GetMonthlyStatistics(_month, _selectedYear, _accommodationId).RenovationsCount;
                RescheduleCount = _monthlyAccommodationStatisticService.GetMonthlyStatistics(_month, _selectedYear, _accommodationId).ReschedulesCount;

                OnPropertyChanged("Month");
            }
        }

        public ICommand GoBackCommand => new RelayCommand(GoBack);
        private void GoBack()
        {
            MyAccommodationYearlyStatisticView myAccommodationsStatistic = new MyAccommodationYearlyStatisticView(_accommodationId, _logedInUser);
            CloseCurrentWindow();
            myAccommodationsStatistic.Show();
        }

    }
}
