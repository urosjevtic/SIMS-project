using System;
using System.Collections.Generic;
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
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.View
{
    
    public partial class StartedTour : Window
    {
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly CheckPointRepository _checkPointRepository;
        private readonly CheckPointGuestsRepository _checkPointGuestRepository;
        
        public CheckBox checkBox { get; set; }
        public int firstSerialNumber { get; set; }
        public bool isChecked { get; set; } 

        public Tour SelectedTour { get; set; }
        public List<CheckPoint> CheckPoints { get; set; }
        
        public StartedTour(Tour selectedTour)
        {
            InitializeComponent();
            this.DataContext = this;
            _checkPointRepository = new CheckPointRepository();
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _checkPointGuestRepository = new CheckPointGuestsRepository();
            SelectedTour = selectedTour;

            CheckPoints = SelectedTour.CheckPoints;

            CheckPoint cp = _tourRepository.GetTourFirstCheckPoint(selectedTour);
            firstSerialNumber = cp.SerialNumber;
            cp.IsChecked = true;
            //checkBox.Checked += CheckBoxChecked;   //ovako pukne samo pri startovanju ture
            //CheckBox checkBox;
            // Označi CheckBox ako je pronađen
           

        }
        //Lista gostiju iz reservationRepository.
        //TJ lista Id 

        public void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            
            CheckPoints = SelectedTour.CheckPoints;
            CheckPoint checkedCheckPoint = ((CheckBox)sender).DataContext as CheckPoint;

            checkedCheckPoint.IsChecked = true;
            MessageBox.Show("DAJANA DEBILL"); // zasto na dugme start tour ovo otvara????? mozda zati sto je prva cekirana automtski???
            //treba mi lista gostiju za tu tacku
            CheckPointGuests checkPointGuests = new CheckPointGuests();
            checkPointGuests.CheckPointId = checkedCheckPoint.Id;
             
        }

        private void CheckBoxUnchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
