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
    /// Interaction logic for ApprovedReservations.xaml
    /// </summary>
    public partial class ApprovedReservations : Window
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
        private ObservableCollection<AccommodationReservationRescheduleRequest> _approved;

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

        public ApprovedReservations()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationReservationRescheduleRequestService=new AccommodationReservationRescheduleRequestService();
            Approved = new ObservableCollection<AccommodationReservationRescheduleRequest>(_accommodationReservationRescheduleRequestService.GetApproved());
        }

        private void BackButton2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BackButton3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
