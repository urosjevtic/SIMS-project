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
using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for MyTours.xaml
    /// </summary>
    public partial class MyTours : Window
    {
        private readonly VoucherRepository _voucherRepository;
        private List<Voucher> Vouchers { get; set; }
        public User LoggedUser { get; set; } 
        public MyTours(User user)
        {
            InitializeComponent();
            this.DataContext = this;
            LoggedUser = user;
            _voucherRepository = new VoucherRepository();
            Vouchers = _voucherRepository.GetAll();
            activeTours.ItemsSource = new ObservableCollection<Voucher>(Vouchers);
        }

        private void GoBackButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewCheckpointsButton(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitRateButton(object sender, RoutedEventArgs e)
        {

        }

        private void OpenCommentFormButton(object sender, RoutedEventArgs e)
        {
            CommentForm commentForm = new CommentForm(LoggedUser);
            commentForm.Show();
        }
    }
}
