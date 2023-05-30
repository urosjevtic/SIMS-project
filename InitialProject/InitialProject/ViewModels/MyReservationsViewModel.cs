﻿using GalaSoft.MvvmLight.Views;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository.ReservationRepo;
using InitialProject.Service.ReservationServices;
using InitialProject.Utilities;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace InitialProject.ViewModels
{
    public class MyReservationsViewModel : BaseViewModel
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged(string name)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
        //    }
        //}

        private NavigationService _navigationService;

        private readonly CanceledResrvationsRepository _canceledResrvationsRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationReservationService _accommodationReservationService;
        private ObservableCollection<Domain.Model.Reservations.AccommodationReservation> _reservations;

        private readonly AccommodationReservationRescheduleRequestService _accommodationReservationRescheduleRequestService;
        private ObservableCollection<AccommodationReservationRescheduleRequest> _pending;
        private ObservableCollection<AccommodationReservationRescheduleRequest> _approved;

        private readonly DeclinedAccommodationReservationRescheduleRequestService _declinedAccommodationReservationRescheduleRequestService;
        private ObservableCollection<DeclinedAccommodationReservationRescheduleRequest> _request;

       // public AccommodationReservation SelectedAccommodation { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }

        public ObservableCollection<Domain.Model.Reservations.AccommodationReservation> Reservations
        {
            get
            {
                return _reservations;
            }

            set
            {
                if (value != _reservations)
                {
                    _reservations = value;
                    OnPropertyChanged("Reservations");
                }
            }
        }

        public ObservableCollection<AccommodationReservationRescheduleRequest> Pending
        {
            get
            {
                return _pending;
            }

            set
            {
                if (value != _pending)
                {
                    _pending = value;
                    OnPropertyChanged("Pending");
                }
            }
        }

        public ObservableCollection<AccommodationReservationRescheduleRequest> Approved
        {
            get
            {
                return _approved;
            }

            set
            {
                if (value != _approved)
                {
                    _approved = value;
                    OnPropertyChanged("Approved");
                }
            }
        }


        public ObservableCollection<DeclinedAccommodationReservationRescheduleRequest> Request
        {
            get
            {
                return _request;
            }

            set
            {
                if (value != _request)
                {
                    _request = value;
                    OnPropertyChanged("Request");
                }
            }
        }


        public MyReservationsViewModel(NavigationService navigationService)
        {
            _accommodationReservationService = new AccommodationReservationService();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _canceledResrvationsRepository = new CanceledResrvationsRepository();
            LoadAllReservations();
            _navigationService = navigationService;
            _accommodationReservationRescheduleRequestService = new AccommodationReservationRescheduleRequestService();
            Pending = new ObservableCollection<AccommodationReservationRescheduleRequest>(_accommodationReservationRescheduleRequestService.GetPending());
            Approved = new ObservableCollection<AccommodationReservationRescheduleRequest>(_accommodationReservationRescheduleRequestService.GetApproved());

            _declinedAccommodationReservationRescheduleRequestService = new DeclinedAccommodationReservationRescheduleRequestService();
            Request = new ObservableCollection<DeclinedAccommodationReservationRescheduleRequest>(_declinedAccommodationReservationRescheduleRequestService.GetAll());
        }
        public UnratedOwner UnratedOwner { get; set; }

        private void LoadAllReservations()
        {
            Reservations = new ObservableCollection<Domain.Model.Reservations.AccommodationReservation>(_accommodationReservationService.GetFutureReservations());
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                if (SelectedReservation.StartDate.AddDays(SelectedReservation.Accommodation.CancelationPeriod) >= DateTime.Now)
                {
                    _canceledResrvationsRepository.SaveCanceledReservation(SelectedReservation);
                    _accommodationReservationRepository.Delete(SelectedReservation);
                    LoadAllReservations();
                    MessageBox.Show("You have successfully canceled your reservation");
                }
                else
                {
                    MessageBox.Show("The cancellation period has expired");
                }
            }
            else
            {
                MessageBox.Show("You must select the reservation you want to delete");
            }
        }



        public ICommand MoveReservationCommand => new RelayCommand(OnMoveReservation);

        private void OnMoveReservation()
        {
            _navigationService.Navigate(new ReservationMoveRequestPage(SelectedReservation));
        }

    }
}
