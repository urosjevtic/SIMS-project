using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Service;
using InitialProject.Domain.Model;
using System.Windows.Input;
using InitialProject.Utilities;
using System.Windows;

namespace InitialProject.ViewModels.GuideViewModel
{
    public class MyAccountViewModel
    {
        private SuperGuideStatusService _superGuideStatusService;
        private TourReservationService _tourReservationService;
        private VoucherService _voucherService; 
        private TourService _tourService;   
        public List<SuperGuideStatus> SuperGuideStatus { get; set; }
        public ICommand QuitJobCommand { get; private set; }
        public User LoggedUser { get; set; }
        public MyAccountViewModel(User user)
        {
            _superGuideStatusService = new SuperGuideStatusService();
            _tourReservationService = new TourReservationService(); 
            _voucherService = new VoucherService(); 
            _tourService = new TourService();   
            SuperGuideStatus = new List<SuperGuideStatus>();
            SuperGuideStatus = _superGuideStatusService.GetAll();
            LoggedUser = user;
            QuitJobCommand = new RelayCommand(QuitJob);

        }
        private void QuitJob()
        {
            foreach(Tour tour in _tourService.GetAll())
            {
                _tourService.SendVauchersForAllTours(tour,LoggedUser);  
                foreach(TourReservation tourReservation in _tourReservationService.GetAll())
                {
                    if(tourReservation.IdTour == tour.Id)
                    {
                        //_tourReservationService.Delete(tourReservation);
                    }
                }
                //_tourService.Delete(tour);  
            }
            CheckExistingVouchers();
            MessageBox.Show("Successfuly quited job! All your tours are calceled. ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        private void CheckExistingVouchers()
        {
            foreach(Voucher voucher in _voucherService.GetAllCreated())
            {
                if(voucher.GuideId == LoggedUser.Id)
                {
                    voucher.CreationDate = DateTime.Now;
                    voucher.GuideId = -1;
                    _voucherService.Update(voucher);
                }
            }
        }

    }
}
