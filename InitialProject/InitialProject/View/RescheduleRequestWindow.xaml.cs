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
using InitialProject.Model;
using InitialProject.Service;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for RescheduleRequestWindow.xaml
    /// </summary>
    public partial class RescheduleRequestWindow : Window
    {
        public ObservableCollection<RescheduleRequest> RescheduleRequests { get; set; }

        private readonly RescheduleRequestService _rescheduleRequestService;

        private readonly User LogedInUser;
        public RescheduleRequestWindow(User loggedInUser)
        {
            InitializeComponent();
            this.DataContext = this;
            _rescheduleRequestService = new RescheduleRequestService();
            LogedInUser = loggedInUser;
            RescheduleRequests = new ObservableCollection<RescheduleRequest>(_rescheduleRequestService.GetAllByOwnerId(LogedInUser.Id));
            
        }

        private void RescheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var rescheduleRequest = button?.DataContext as RescheduleRequest;
            if (rescheduleRequest != null)
            {
                _rescheduleRequestService.ApproveReschedule(rescheduleRequest);
                RescheduleRequests.Remove(rescheduleRequest);
            }
        }
    }
}
