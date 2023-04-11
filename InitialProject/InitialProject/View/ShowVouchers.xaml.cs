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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ShowVouchers.xaml
    /// </summary>
    public partial class ShowVouchers : Window
    {
        public ObservableCollection<Voucher> vouchersObservable;

        private readonly VoucherService _voucherService;
        public List<Voucher> Vouchers { get; set; }
        public ShowVouchers()
        {
            InitializeComponent();
            this.DataContext = this;
            _voucherService = new VoucherService();
            Vouchers = _voucherService.GetAllCreated();
            vouchersDataGrid.ItemsSource = new ObservableCollection<Voucher>(Vouchers);
        }

        private void DataGrid_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
