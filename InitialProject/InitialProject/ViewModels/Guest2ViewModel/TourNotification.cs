﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.ViewModels.Guest2ViewModel
{
    public class TourNotification
    {
        public Tour tour { get; set; }
        public Domain.Model.Notification notification { get; set; }
        public TourNotification(Tour t, Domain.Model.Notification n)
        {
            tour = t;
            notification = n;
        }
    }
}
