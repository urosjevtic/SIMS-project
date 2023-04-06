using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Service;

namespace InitialProject.ViewModels
{
    public class RescheduleRequestViewModel
    {
        private readonly RescheduleRequestService _rescheduleRequestService;
        private readonly User _logedInUser;

        public ObservableCollection<RescheduleRequest> RescheduleRequests { get; set; }
    }
}
