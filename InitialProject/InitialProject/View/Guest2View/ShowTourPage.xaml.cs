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
using InitialProject.Domain.Model;
using InitialProject.ViewModels;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for ShowTourPage.xaml
    /// </summary>
    public partial class ShowTourPage : Page
    {
        public ShowTourViewModel showTourViewModel;
        public ShowTourPage(User user, NavigationService ns)
        {
            InitializeComponent();
            showTourViewModel = new ShowTourViewModel(ns);
            this.DataContext = showTourViewModel;
        }
        private void AddStudentBtn_Click(object sender, RoutedEventArgs e)
        {
            /*ShowTourRequestsViewModel vm = new ShowTourRequestsViewModel(this.NavigationService);
            ShowAllRequests addStudentPage = new ShowAllRequests(vm);
            this.NavigationService.Navigate(addStudentPage);*/
        }
        private void OpenVouchersBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowVouchersViewModel vm = new ShowVouchersViewModel(this.NavigationService);
            ShowVouchersPage openVouchers = new ShowVouchersPage(vm);
            this.NavigationService.Navigate(openVouchers);
        }
    }
}
