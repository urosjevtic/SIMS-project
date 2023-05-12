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
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.ViewModels;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TourGuestRating : Window
    {
        public Comment selectedComment { get; set; }
       
        public TourGuestRating(Tour tour)
        {
            InitializeComponent();
          

            this.DataContext = new TourGuestRatingsViewModel(tour);
          
        }

        

    }
}
