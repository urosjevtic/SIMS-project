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
using InitialProject.ViewModels.Guest2ViewModel;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for SelectedTour.xaml
    /// </summary>
    public partial class SelectedTour : Window
    {
        public SelectedTourViewModel selectedTourViewModel;
        public SelectedTour(Tour tour,User LoggedUser)
        {
            InitializeComponent();
            selectedTourViewModel = new SelectedTourViewModel(tour,LoggedUser);
            this.DataContext = selectedTourViewModel;
        }
    }
}
