using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using InitialProject.Domain.Model;
using InitialProject.Service;
using InitialProject.View;
using InitialProject.View.Guest2View;

namespace InitialProject.ViewModels
{
    public class ShowVouchersViewModel
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
                RaisePropertyChanged("Vouchers");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private readonly VoucherService _voucherService;
        public ShowVouchersViewModel()
        {
            _voucherService = new VoucherService();
            vouchers = _voucherService.GetAllCreated();
            Vouchers = new ObservableCollection<Voucher>(vouchers);
            GoBackCommand = new RelayCommand(GoBack);
        }

        private void GoBack()
        {
            Window currentWindow = System.Windows.Application.Current.Windows.OfType<ShowVouchers>().SingleOrDefault(w => w.IsActive);
            currentWindow?.Close();
        }
    }
}
