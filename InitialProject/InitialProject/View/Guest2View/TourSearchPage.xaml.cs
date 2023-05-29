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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InitialProject.Domain.Model;
using InitialProject.ViewModels;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for TourSearchPage.xaml
    /// </summary>
    public partial class TourSearchPage : Page
    {
        public TourSearchPage(NavigationService nav)
        {
            InitializeComponent();
            this.DataContext = new TourSearchViewModel(nav);
        }
        public TourSearchPage(NavigationService nav, ObservableCollection<Tour> tours)
        {
            InitializeComponent();
            this.DataContext = new TourSearchViewModel(nav, tours);
        }
    }
}
