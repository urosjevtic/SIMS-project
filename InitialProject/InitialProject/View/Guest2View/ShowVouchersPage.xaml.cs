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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InitialProject.ViewModels;
using InitialProject.ViewModels.Guest2ViewModel;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for ShowVouchersPage.xaml
    /// </summary>
    public partial class ShowVouchersPage : Page
    {
        public ShowVouchersPage(ShowVouchersViewModel sv)
        {
            InitializeComponent();
            this.DataContext = sv;
        }
    }
}
