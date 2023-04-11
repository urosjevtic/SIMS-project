using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository.ReservationRepo;
using InitialProject.Service.ReservationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private readonly CanceledResrvationsRepository _canceledResrvationsRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationReservationService _accommodationReservationService;
        private ObservableCollection<Domain.Model.Reservations.AccommodationReservation> _reservations;

        public Domain.Model.Reservations.AccommodationReservation SelectedReservation { get; set; }

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

        public UnratedOwner UnratedOwner { get; set; }

        public MyReservations()
        {
            InitializeComponent();
            this.DataContext = this;

            _accommodationReservationService = new AccommodationReservationService();
            _accommodationReservationRepository= new AccommodationReservationRepository();
            _canceledResrvationsRepository=new CanceledResrvationsRepository();
            LoadAllReservations();
        }

        private void LoadAllReservations()
        {
            Reservations = new ObservableCollection<Domain.Model.Reservations.AccommodationReservation>(_accommodationReservationService.GetFutureReservations());
        }

        private void RatedButton_Click(object sender, RoutedEventArgs e)
        {
            //   AccommodationRatingForm accommodationRating = new AccommodationRatingForm(LoggedUser);
            //  accommodationRating.Show();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                if (SelectedReservation.StartDate.AddDays(-SelectedReservation.Accommodation.CancelationPeriod) >= DateTime.Now)
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
    }
}
