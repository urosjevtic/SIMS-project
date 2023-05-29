using InitialProject.Domain.Model;
using InitialProject.ViewModels;
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

namespace InitialProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for TourGuestRatings.xaml
    /// </summary>
    public partial class TourGuestRating : Window
    {
       
        

        public TourGuestRating(Tour tour)
        {
            InitializeComponent();


            this.DataContext = new TourGuestRatingsViewModel(tour);

        }

    }
}

