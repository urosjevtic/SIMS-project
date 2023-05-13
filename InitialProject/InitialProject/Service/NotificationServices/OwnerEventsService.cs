using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Service.RenovationServices;

namespace InitialProject.Service.NotificationServices
{
    public class OwnerEventsService
    {

        private readonly RenovationService _renovationService;
        private readonly AccommodationService _accommodationService;

        public OwnerEventsService()
        {
            _renovationService = new RenovationService();
            _accommodationService = new AccommodationService();
        }


        public void CheckAccommodationRenovationStatus(int ownerId)
        {
            List<Renovation> ownersRenovations = _renovationService.GetByOwnerId(ownerId);
            foreach (var renovation in ownersRenovations)
            {

                if (renovation.IsFinished)
                {
                    if(!renovation.Accommodation.IsRecentlyRenovated)
                        _accommodationService.ChangeRecentlyRenovatedStatus(renovation.Accommodation);
                    if (renovation.EndDate.AddYears(1) <= DateTime.Now)
                        _accommodationService.ChangeRecentlyRenovatedStatus(renovation.Accommodation);

                }
            }
        }
    }
}
