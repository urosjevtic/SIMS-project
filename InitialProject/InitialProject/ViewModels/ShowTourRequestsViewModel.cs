﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class ShowTourRequestsViewModel
    {
        public ICommand ShowShortTourRequestsCommand { get; private set; }
        public ICommand ShowShortTourStatisticsCommand { get; private set; }
        public ICommand ShowComplexTourRequestsCommand { get; private set; }

        public ShowTourRequestsViewModel()
        {
            ShowShortTourRequestsCommand = new RelayCommand(ShowShortRequests);
            ShowShortTourStatisticsCommand = new RelayCommand(ShowShortStatistics);
            ShowComplexTourRequestsCommand = new RelayCommand(ShowShortRequests);
        }
        public void ShowShortRequests()
        {
            ShowShortTourRequests showShort = new();
            showShort.Show();
        }
        public void ShowShortStatistics()
        {
            ShowShortTourStatistics showShortStatistics = new();
            showShortStatistics.Show();
        }
    }
}
