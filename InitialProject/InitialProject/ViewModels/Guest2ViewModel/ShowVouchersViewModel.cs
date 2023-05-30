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
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class ShowVouchersViewModel : Page
    {
        public List<Voucher> vouchers { get; set; }
        public ICommand GoBackCommand { get; private set; }

        private ObservableCollection<Voucher> _vouchers;
        public ObservableCollection<Voucher> Vouchers
        {
            get { return _vouchers; }
            set
            {
                _vouchers = value;
                OnPropertyChanged("Vouchers");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public NavigationService navService;
        public User LoggedUser { get; set; } = App.LoggedUser;

        private readonly VoucherService _voucherService;
        private readonly TourReservationService _tourReservationService;
        public ShowVouchersViewModel(NavigationService nav)
        {
            _voucherService = new VoucherService();
            _tourReservationService = new TourReservationService();
            this.navService = nav;
            _tourReservationService.CheckPresenceForNewVouchers(LoggedUser);
            vouchers = _voucherService.GetAllCreated();
            Vouchers = new ObservableCollection<Voucher>(vouchers);
            GoBackCommand = new RelayCommand(GoBack);
        }

        private void GoBack()
        {
            if (navService.CanGoBack)
            {
                navService.GoBack();
            }
        }
    }
}
