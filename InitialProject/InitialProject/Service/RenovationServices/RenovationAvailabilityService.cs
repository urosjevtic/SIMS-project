using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Reservations;

namespace InitialProject.Service.RenovationServices
{
    public class RenovationAvailabilityService
    {
        public RenovationAvailabilityService() { }


        public List<DateTime> FindDates(List<AccommodationReservation> reservations, List<DateTime> datesInRange, int renovationDays)
        {
            List<DateTime> availableDates = new List<DateTime>();

            foreach (DateTime date in datesInRange)
            {

                if (IsDateAvailable(reservations, date, renovationDays))
                {
                    availableDates.Add(date);
                }
            }

            return availableDates;
        }

        private bool IsDateAvailable(List<AccommodationReservation> reservations, DateTime date, int renovationDays)
        {
            bool isAvailable = CheckDateAvailability(reservations, date);

            if (isAvailable)
                isAvailable = CheckRenovationOverlap(reservations, date, renovationDays, isAvailable);

            return isAvailable;
        }

        private bool CheckDateAvailability(List<AccommodationReservation> reservations, DateTime date)
        {
            bool isAvailable = true;
            foreach (AccommodationReservation reservation in reservations)
            {
                if (date >= reservation.StartDate && date <= reservation.EndDate)
                {
                    isAvailable = false;
                    break;
                }
            }

            return isAvailable;
        }

        private bool CheckRenovationOverlap(List<AccommodationReservation> reservations, DateTime date, int renovationDays, bool isAvailable)
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

            return isAvailable;
        }
    }
}
