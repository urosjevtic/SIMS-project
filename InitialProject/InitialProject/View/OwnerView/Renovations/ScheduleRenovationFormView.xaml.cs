﻿using System;
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
using InitialProject.ViewModels.RenovationsViewModel;

namespace InitialProject.View.OwnerView.Renovations
{
    /// <summary>
    /// Interaction logic for ScheduleRenovationFormView.xaml
    /// </summary>
    public partial class ScheduleRenovationFormView : Window
    {
        public ScheduleRenovationFormView(User logedInUser, Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = new ScheduleRenovationFormViewModel(logedInUser, accommodation);
        }
    }
}