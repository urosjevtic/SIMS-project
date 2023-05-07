﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.RenovationServices;
using InitialProject.Service.ReservationServices;
using InitialProject.Utilities;
using InitialProject.Validation;

namespace InitialProject.ViewModels.RenovationsViewModel
{
    public class ScheduleRenovationFormViewModel : BaseViewModel
    {

        private readonly RenovationService _renovationService;
        private readonly Accommodation _accommodation;
        private readonly AccommodationReservationService _accommodationReservationService;

        public ScheduleRenovationFormViewModel(User logedInUser, Accommodation accommodation)
        {
            _renovationService = new RenovationService();
            _accommodationReservationService = new AccommodationReservationService();
            _accommodation = accommodation;
        }

        private List<DateTime> _availableDates;
        public List<DateTime> AvailableDates
        {
            get { return _availableDates; }
            set
            {
                _availableDates = value;
                OnPropertyChanged("AvailableDates");
            }
        }

        private DateTime _fromDate = DateTime.Now;

        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                _fromDate = value;
                AvailableDates = _renovationService.FindAvailableDates(_accommodationReservationService.GetReservationsByAccommodationId(_accommodation.Id), _fromDate, _toDate, _renovationLength);
                OnPropertyChanged("FromDate");
            }
        }

        private DateTime _toDate = DateTime.Now.AddDays(1);

        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                _toDate = value;
                AvailableDates = _renovationService.FindAvailableDates(_accommodationReservationService.GetReservationsByAccommodationId(_accommodation.Id), _fromDate, _toDate, _renovationLength);
                OnPropertyChanged("ToDate");
            }
        }

        private int _renovationLength = 0;

        public int RenovationLength
        {
            get { return _renovationLength; }
            set
            {
                _renovationLength = value;
                AvailableDates = _renovationService.FindAvailableDates(_accommodationReservationService.GetReservationsByAccommodationId(_accommodation.Id), _fromDate, _toDate, _renovationLength);
                OnPropertyChanged("RenovationLength");
            }
        }

        public DateTime _selectedStartDate;

        public DateTime SelectedStartDate
        {
            get { return _selectedStartDate; }
            set
            {
                _selectedStartDate = value;
                OnPropertyChanged("SelectedStartDate");
            }
        }

        private string _renovationDescription = "";

        public string RenovationDescription
        {
            get { return _renovationDescription; }
            set
            {
                _renovationDescription = value;
                OnPropertyChanged("RenovationDescription");
            }
        }


        public ICommand ScheduleRenovationCommand => new RelayCommand(ScheduleRenovation, canExecuteCommand);

        private void ScheduleRenovation()
        {
            _renovationService.ScheduleRenovation(_accommodation, _selectedStartDate, _renovationLength, _renovationDescription);
        }

        private bool canExecuteCommand()
        {
            return RenovationDescription != "" && FromDate < ToDate && SelectedStartDate != DateTime.Parse("01/01/0001 00:00:00");
        }
    }
}
