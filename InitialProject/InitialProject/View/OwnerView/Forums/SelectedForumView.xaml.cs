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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Forums;
using InitialProject.ViewModels.ForumsViewModel;

namespace InitialProject.View.OwnerView.Forums
{
    /// <summary>
    /// Interaction logic for SelectedForumView.xaml
    /// </summary>
    public partial class SelectedForumView : Page
    {
        public SelectedForumView(User logedUser, NavigationService navigationService, Forum selectedForum)
        {
            InitializeComponent();
            DataContext = new SelectedForumViewModel(logedUser, navigationService, selectedForum);
        }
    }
}
