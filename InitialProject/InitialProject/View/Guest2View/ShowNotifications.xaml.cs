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
using InitialProject.ViewModels.Guest2ViewModel;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for ShowNotifications.xaml
    /// </summary>
    public partial class ShowNotifications : Window
    {
        public ShowNotificationsViewModel showNotificationsViewModel { get; set; }
        public ShowNotifications(User user)
        {
            InitializeComponent();
            showNotificationsViewModel = new ShowNotificationsViewModel(user);
            this.DataContext = showNotificationsViewModel;
        }
    }
}
