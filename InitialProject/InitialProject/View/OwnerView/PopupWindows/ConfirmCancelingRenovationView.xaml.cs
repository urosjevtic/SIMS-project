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
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.ViewModels.PopupViewModel;

namespace InitialProject.View.OwnerView.PopupWindows
{
    /// <summary>
    /// Interaction logic for ConfirmCancelingRenovationView.xaml
    /// </summary>
    public partial class ConfirmCancelingRenovationView : Window
    {
        public ConfirmCancelingRenovationView(Renovation renovation, ObservableCollection<Renovation> renovations) 
        {
            InitializeComponent();
            DataContext = new ConfirmCancelingRenovationViewModel(renovation, renovations);
        }
    }
}
