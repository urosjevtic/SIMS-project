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
    /// Interaction logic for RejectedRequests.xaml
    /// </summary>
    public partial class RejectedRequests : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private readonly DeclinedAccommodationReservationRescheduleRequestService _declinedAccommodationReservationRescheduleRequestService;
        private ObservableCollection<DeclinedAccommodationReservationRescheduleRequest> _request;

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

        public RejectedRequests()
        {
            InitializeComponent();
            this.DataContext = this;
            _declinedAccommodationReservationRescheduleRequestService=new DeclinedAccommodationReservationRescheduleRequestService();
            Request = new ObservableCollection<DeclinedAccommodationReservationRescheduleRequest>(_declinedAccommodationReservationRescheduleRequestService.GetAll());
        }

        
    }
}
