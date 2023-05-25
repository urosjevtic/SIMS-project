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



        public List<Location> ReccommmendPlaceForNewAccommdoation()
        {
            Dictionary<int, List<AccommodationStatistic>> accommodationStatistics = GetAccommdoationStatistic();
            Dictionary<int, int> numberOfReservationsForAccommoadations =
                GetNumberOfReservationsForAccommoadations(accommodationStatistics);

            List<Location> locations = new List<Location>();
            locations.Add(FindBuisiestLocation(numberOfReservationsForAccommoadations));
            return locations;


        }

        private Location FindBuisiestLocation(Dictionary<int, int> numberOfReservationsForAccommoadations)
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

            var busiestLocation = reservationsPerLocation.OrderByDescending(kvp => kvp.Value).FirstOrDefault();
            return busiestLocation.Key;
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

                reservationStatistic[accommodation] = GetNumberOfReservationsForLastSixMonths(accommodationStatistics[accommodation]);
            }

            return reservationStatistic;
        }

        private int GetNumberOfReservationsForLastSixMonths(List<AccommodationStatistic> statistics)
        {
            int numberOfReservations = 0;
            foreach (var statistic in statistics)
            {
                if (statistic.MonthAndYear.Month >= DateTime.Now.AddMonths(-6).Month)
                {
                    numberOfReservations += statistic.ReservationsCount;
                }
            }

            return numberOfReservations;
        }
    }
}
