using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service;
using InitialProject.Domain.Model;


namespace InitialProject.ViewModels.GuideViewModel
{
    public class MyAccountViewModel
    {
        private SuperGuideStatusService _superGuideStatusService;
        public List<SuperGuideStatus> SuperGuideStatus { get; set; }    
        public MyAccountViewModel()
        {
            _superGuideStatusService = new SuperGuideStatusService();
            SuperGuideStatus = new List<SuperGuideStatus>();
            SuperGuideStatus = _superGuideStatusService.GetAll();
        }

        


    }
}
