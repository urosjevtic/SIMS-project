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
using InitialProject.Service;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TourStatistic : Window
    {
        public TourService _tourService;
        public TourStatistic()
        {
            InitializeComponent();
            _tourService = new TourService();
        }

        private void GodineComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
               // Dohvati odabranu godinu
                string year = ((ComboBoxItem)YearsComboBox.SelectedItem).Content.ToString();

                // Dohvati ime najposjećenije ture za odabranu godinu
                string Name = _tourService.GetMostVisitedEver(year);

                // Postavi tekst u TextBlock elementu
                mostVisitedTourTextBlock.Text = "Najposjećenija tura za " + year + " je " + Name + ".";
            

        }
    }
}
