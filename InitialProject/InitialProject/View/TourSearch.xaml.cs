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
using InitialProject.Model;
using InitialProject.Repository;
using System.Windows.Forms;
using System.Data;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourSearch.xaml
    /// </summary>
    public partial class TourSearch : Window
    {
        public ObservableCollection<Tour> toursObservable { get; set; }

        private readonly TourRepository _tourRepository;
        public List<Tour> tours { get; set; }

        public ShowTour showTour { get; set; }

        public User LoggedInUser { get; set; }
        public TourSearch(User user)
        {
            LoggedInUser= user;
            InitializeComponent();
            this.DataContext = this;
            _tourRepository = new TourRepository();
        }

        private void searchButtonClick(object sender, RoutedEventArgs e)
        {
            ShowTour showTour = new ShowTour(LoggedInUser);
            List<Tour> tours = new List<Tour>();
            tours = _tourRepository.GetAll();

            if(stateTextBox.Text != String.Empty)
            {
                foreach(Tour tour in tours)
                {
                    if (tour.Location.Country.ToLower().Contains(stateTextBox.Text.ToLower()))
                    {
                        tours.Add(tour);
                    }
                }
            }
            if(cityTextBox.Text != String.Empty)
            {
                foreach(Tour tour in tours)
                {
                    if (tour.Location.City.ToLower().Contains(cityTextBox.Text.ToLower()))
                    {
                        tours.Add(tour);
                    }
                }
            }
            if (durationTextBox.Text != String.Empty)
            {
                foreach (Tour tour in tours)
                {
                    if (tour.Duration >= int.Parse(durationTextBox.Text))
                    {
                        tours.Add(tour);
                    }
                }
            }
            if (languageTextBox.Text != String.Empty)
            {
                foreach (Tour tour in tours)
                {
                    if (tour.Language.ToLower().Contains(languageTextBox.Text.ToLower()))
                    {
                        tours.Add(tour);
                    }
                }
            }
            if (numberTextBox.Text != String.Empty)
            {
                foreach (Tour tour in tours)
                {
                    if (tour.MaxGuests >= int.Parse(cityTextBox.Text))
                    {
                        tours.Add(tour);
                    }
                }
            }
            showTour.SearchUpdateDataGrid(tours);
            showTour.Show();
            this.Close();
        }

    }
}
