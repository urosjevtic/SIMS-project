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
using InitialProject.ViewModels.AccommodationViewModel;

namespace InitialProject.View.OwnerView.MyAccommodations
{
    /// <summary>
    /// Interaction logic for MyAccommodationImagesView.xaml
    /// </summary>
    public partial class MyAccommodationImagesView : Window
    {
        public MyAccommodationImagesView(Accommodation accomodation, User logedInUser)
        {
            InitializeComponent();
            DataContext = new MyAccommodationImagesViewModel(logedInUser, accomodation);
        }
    }
}
