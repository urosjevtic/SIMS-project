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
using InitialProject.ViewModels.AccommodationViewModel;

namespace InitialProject.View.OwnerView.MyAccommodations
{
    /// <summary>
    /// Interaction logic for MyAccommodationYearlyStatisticView.xaml
    /// </summary>
    public partial class MyAccommodationYearlyStatisticView : Window
    {
        public MyAccommodationYearlyStatisticView(int accommodationId)
        {
            InitializeComponent();
            DataContext = new MyAccommodationYearlyStatisticViewModel(accommodationId);
        }
    }
}
