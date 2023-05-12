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
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.ViewModels;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for ShowVouchers.xaml
    /// </summary>
    public partial class ShowVouchers : Window
    {
        public ShowVouchersViewModel ShowVouchersViewModel;
        public ShowVouchers()
        {
            InitializeComponent();
            ShowVouchersViewModel = new ShowVouchersViewModel();
            this.DataContext = ShowVouchersViewModel;
        }

    }
}
