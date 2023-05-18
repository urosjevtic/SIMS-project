using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;


namespace InitialProject.ViewModels
{
    public class GuideCommentsOverview
    {
        public User User { get; set; }
        public CheckPoint CheckPoint { get; set; }
        public string Comment { get; set; }
        public bool IsReported { get; set; }

        public GuideCommentsOverview()
        {
            CheckPoint = new CheckPoint();  
        }

        public GuideCommentsOverview(User user, CheckPoint checkPoint, string comment,bool isReported)
        {
            User = user;
            CheckPoint = checkPoint;
            Comment = comment;
            IsReported = isReported;
        }
    }
}
