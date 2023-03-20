using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AccommodationReservation.xaml
    /// </summary>
    public partial class AccommodationReservation : Window
    {

        public Accommodation SelectedAccommodation { get; set; }
        public List<Accommodation> Accommodations { get; set; }
       
        public AccommodationReservation(Accommodation selectedAccommodation)
        {
            InitializeComponent();
           // this.DataContext = selectedAccommodation;
           this.DataContext=this;
            SelectedAccommodation = selectedAccommodation;
            Accommodations = new List<Accommodation>();
            Accommodations.Add(SelectedAccommodation);
            reservationDataGrid.ItemsSource= Accommodations;
            //reservationDataGrid.ItemsSource = new ObservableCollection<Accommodation>(Accommodations);

        }
    }
}





