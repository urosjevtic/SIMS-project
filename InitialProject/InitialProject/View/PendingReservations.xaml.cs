using InitialProject.Domain.Model.Reservations;
using InitialProject.Service.ReservationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for PendingReservations.xaml
    /// </summary>
    public partial class PendingReservations : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private readonly AccommodationReservationRescheduleRequestService _accommodationReservationRescheduleRequestService;
        private ObservableCollection<AccommodationReservationRescheduleRequest> _pending;

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


        public PendingReservations()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationReservationRescheduleRequestService = new AccommodationReservationRescheduleRequestService();
            Pending = new ObservableCollection<AccommodationReservationRescheduleRequest>(_accommodationReservationRescheduleRequestService.GetPending()); 
        }

        private void BackButton3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
