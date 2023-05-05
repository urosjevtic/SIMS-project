using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.AccommodationRenovation;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.RepositoryInterfaces.IAccommodationRenovationsRepo;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service.RenovationServices
{
    public class RenovationService
    {

        private readonly IRenovationRepository _renovationRepository;
        private readonly AccommodationReservationService _accommodationReservationService;


        public RenovationService()
        {
            _renovationRepository = Injector.Injector.CreateInstance<IRenovationRepository>();
            _accommodationReservationService = new AccommodationReservationService();
        }


        public List<Renovation> GetAll()
        {
            return _renovationRepository.GetAll();
        }

        public List<Renovation> GetByAccommodationId(int accommodationId)
        {
           return _renovationRepository.GetByAccommodationId(accommodationId);
        }

        public void Save(Renovation renovation)
        {
            _renovationRepository.Save(renovation);
        }

        public void ScheduleRenovation(Accommodation accommodation, DateTime renovationStartDate, int renovationLength, string desctiption)
        {
            Renovation renovation = new Renovation();
            renovation.Accommodation = accommodation;
            renovation.StartDate = renovationStartDate;
            renovation.LengthInDays = renovationLength;
            renovation.Description = desctiption;

            _renovationRepository.Save(renovation);
        }


        public List<DateTime> FindAvailableDates(List<AccommodationReservation> reservations, DateTime renovationStartDate, DateTime endDate, int renovationDays)
        {
            List<DateTime> availableDates = new List<DateTime>();

            // Create a list of dates between the start and end date range
            List<DateTime> datesInRange = Enumerable.Range(0, 1 + endDate.Subtract(renovationStartDate).Days)
                .Select(offset => renovationStartDate.AddDays(offset))
                .ToList();

            // Loop through each date in the range and check if it's available for the entire renovation period
            foreach (DateTime date in datesInRange)
            {
                bool isAvailable = true;

                // Check if the date is already reserved
                foreach (AccommodationReservation reservation in reservations)
                {
                    if (date >= reservation.StartDate && date <= reservation.EndDate)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                // Check if the date is available for the entire renovation period
                if (isAvailable)
                {
                    DateTime renovationEndDate = date.AddDays(renovationDays - 1);

                    foreach (AccommodationReservation reservation in reservations)
                    {
                        if (reservation.StartDate <= renovationEndDate && reservation.EndDate >= date)
                        {
                            isAvailable = false;
                            break;
                        }
                    }

                    if (isAvailable)
                    {
                        availableDates.Add(date);
                    }
                }
            }

            return availableDates;
        }

    }
}
