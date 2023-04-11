using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Forms;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.ViewModels;

namespace InitialProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for MyTours.xaml
    /// </summary>
    public partial class MyTours : Window
    {
        public MyToursViewModel MyToursViewModel { get; set; }
        public User LoggedUser { get; set; }
        public MyTours(User user)
        {
            InitializeComponent();
            LoggedUser = user;
            MyToursViewModel = new MyToursViewModel(LoggedUser);
            this.DataContext = MyToursViewModel;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
