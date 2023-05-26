using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Service.ReservationServices;
using InitialProject.Service.StatisticService;

namespace InitialProject.Service.AccommodationServices
{
    public class AccommodationSuggestionService
    {

        private readonly AccommodationService _accommodationService;
        private readonly YearlyAccommodationStatisticService _yearlyAccommodationStatisticService;

        public AccommodationSuggestionService()
        {
            _yearlyAccommodationStatisticService = new YearlyAccommodationStatisticService();
            _accommodationService = new AccommodationService();
        }



        public LocationStatistic ReccommmendPlaceForNewAccommdoation()
        {
            Dictionary<int, List<AccommodationStatistic>> accommodationStatistics = GetAccommdoationStatistic();
            Dictionary<int, int> numberOfReservationsForAccommoadations =
                GetNumberOfReservationsForAccommoadations(accommodationStatistics);

            return FindBuisiestLocation(numberOfReservationsForAccommoadations);


        }

        public Accommodation ReccommendedAccommodationForClosing(int ownerId)
        {
            Dictionary<int, List<AccommodationStatistic>> accommodationStatistics = GetAccommdoationStatistic();
            Dictionary<int, int> numberOfReservationsForAccommoadations =
                GetNumberOfReservationsForAccommoadations(accommodationStatistics);

            List<LocationStatistic> leastVisitedLocations =
                FindLeastVisitedLocation(numberOfReservationsForAccommoadations);

            return FindAccommodationForClosing(ownerId, leastVisitedLocations);


        }

        private Accommodation FindAccommodationForClosing(int ownerId, List<LocationStatistic> leastVisidetLocations)
        {
            List<Accommodation> accommodations = _accommodationService.GetAllAccommodationByOwnerId(ownerId);
            foreach (var leastVisitedLocation in leastVisidetLocations.OrderByDescending(ls => ls.NumberOfReservations).ToList())
            {
                foreach (var accommdoation in accommodations)
                {
                    if (accommdoation.Location.Equals(leastVisitedLocation.Location))
                    {
                        return accommdoation;
                    }
                }
            }

            return null;
        }

        private List<LocationStatistic> FindLeastVisitedLocation(Dictionary<int, int> numberOfReservationsForAccommoadations)
        {
            Dictionary<Location, int> reservationsPerLocation = GetNumberOFReservationsForLocations(numberOfReservationsForAccommoadations);

            List<LocationStatistic> leastVisitedLocations = new List<LocationStatistic>();

            foreach (var reservationPerLocation in reservationsPerLocation)
            {
                if (reservationPerLocation.Value < 10)
                {
                    leastVisitedLocations.Add(new LocationStatistic(reservationPerLocation.Key, reservationPerLocation.Value));
                }
            }

            return leastVisitedLocations;
        }

        private LocationStatistic FindBuisiestLocation(Dictionary<int, int> numberOfReservationsForAccommoadations)
        {
            Dictionary<Location, int> reservationsPerLocation = GetNumberOFReservationsForLocations(numberOfReservationsForAccommoadations);

            var busiestLocation = reservationsPerLocation.OrderByDescending(kvp => kvp.Value).FirstOrDefault();
            return new LocationStatistic(busiestLocation.Key, busiestLocation.Value);
        }

        private Dictionary<Location, int> GetNumberOFReservationsForLocations(Dictionary<int, int> numberOfReservationsForAccommoadations)
        {
            Dictionary<Location, int> reservationsPerLocation = new Dictionary<Location, int>();
            foreach (var accommdoationId in numberOfReservationsForAccommoadations.Keys)
            {
                Location location = _accommodationService.GetById(accommdoationId).Location;
                if (!reservationsPerLocation.ContainsKey(location))
                {
                    reservationsPerLocation[location] = numberOfReservationsForAccommoadations[accommdoationId];
                }

                reservationsPerLocation[location] += numberOfReservationsForAccommoadations[accommdoationId];
            }

            return reservationsPerLocation;
        }


        private Dictionary<int, List<AccommodationStatistic>> GetAccommdoationStatistic()
        {
            Dictionary<int, List<AccommodationStatistic>> accommodationStatistics =
                new Dictionary<int, List<AccommodationStatistic>>();

            List<Accommodation> accommodations = _accommodationService.GetAll();
            foreach (var accommodation in accommodations)
            {
                accommodationStatistics[accommodation.Id] =
                    _yearlyAccommodationStatisticService.GetStatisticByAccommodationId(accommodation.Id);
            }

            return accommodationStatistics;
        }

        private Dictionary<int, int> GetNumberOfReservationsForAccommoadations(Dictionary<int, List<AccommodationStatistic>> accommodationStatistics)
        {
            Dictionary<int, int> reservationStatistic = new Dictionary<int, int>();
            foreach (var accommodation in accommodationStatistics.Keys)
            {

                reservationStatistic[accommodation] = GetNumberOfReservationsForLastYear(accommodationStatistics[accommodation]);
            }

            return reservationStatistic;
        }

        private int GetNumberOfReservationsForLastYear(List<AccommodationStatistic> statistics)
        {
            int numberOfReservations = 0;
            foreach (var statistic in statistics)
            {
                if (statistic.MonthAndYear.Year >= DateTime.Now.AddYears(-1).Year)
                {
                    numberOfReservations += statistic.ReservationsCount;
                }
            }

            return numberOfReservations;
        }
    }
}
