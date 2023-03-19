using InitialProject.Model;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for UnratedGuestNotification.xaml
    /// </summary>
    public partial class UnratedGuestNotification : Window
    {

        public ObservableCollection<UnratedGuest> UnratedGuests { get; set; }
        public UnratedGuestNotification(List<UnratedGuest> unratedGuests)
        {
            InitializeComponent();
            this.DataContext = this;
            UnratedGuests = new ObservableCollection<UnratedGuest>(unratedGuests);
        }

        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
