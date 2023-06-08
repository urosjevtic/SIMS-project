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
using InitialProject.ViewModels.GuideViewModel;

namespace InitialProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for ComplexRequestWindow.xaml
    /// </summary>
    public partial class ComplexRequestWindow : Window
    {
        public ComplexRequestWindow(User user,List<ShortTourRequest> requests)
        {
            InitializeComponent();
            DataContext = new ComplexRequestsViewModel(user,requests);   
        }
    }
}
