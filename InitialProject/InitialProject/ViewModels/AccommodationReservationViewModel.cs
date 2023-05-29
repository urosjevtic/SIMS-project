using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.ViewModels
{
    class AccommodationReservationViewModel
    {
        public Accommodation Accommodation { get; set; }         

        public AccommodationReservationViewModel()
        {
            
        }

        public AccommodationReservationViewModel(Accommodation accommodation)
        {
            Accommodation = accommodation;
        }
    }
}
