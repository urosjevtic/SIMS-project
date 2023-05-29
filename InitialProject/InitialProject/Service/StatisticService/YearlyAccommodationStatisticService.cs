using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service.StatisticService
{
    public class YearlyAccommodationStatisticService
    {
        private readonly IAccommodationStatisticsDataRepository _accommodationStatisticsDataRepository;
        private readonly AccommodationReservationService _accommodationReservationService;

        public YearlyAccommodationStatisticService()
        {
            _accommodationStatisticsDataRepository = Injector.Injector.CreateInstance<IAccommodationStatisticsDataRepository>();
            _accommodationReservationService = new AccommodationReservationService();
        }


        public List<AccommodationStatistic> GetStatisticByAccommodationId(int accommodationId)
        {
            Dictionary<int, AccommodationStatistic> yearlyStatistics = new Dictionary<int, AccommodationStatistic>();

            foreach (var statistic in _accommodationStatisticsDataRepository.GetByAccommodationId(accommodationId).Statistics)
            {
                int year = statistic.MonthAndYear.Year;

                if (!yearlyStatistics.ContainsKey(year))
                {
                    yearlyStatistics[year] = new AccommodationStatistic { MonthAndYear = new DateTime(year, 1, 1) };
                }

                yearlyStatistics[year].RenovationsCount += statistic.RenovationsCount;
                yearlyStatistics[year].CancelationsCount += statistic.CancelationsCount;
                yearlyStatistics[year].ReservationsCount += statistic.ReservationsCount;
                yearlyStatistics[year].ReschedulesCount += statistic.ReschedulesCount;
            }

            return yearlyStatistics.Values.ToList();
        }




        public int GetMostOccupiedYear(int accommodationId)
        {
           List<AccommodationReservation> accommodationReservations = _accommodationReservationService.GetReservationsByAccommodationId(accommodationId);
           Dictionary<int, int> occupiedDaysPerYear = GetNumberOfOccupiedDaysPerYear(accommodationReservations);

           return FindMostOccupiedYear(occupiedDaysPerYear);
        }

        private Dictionary<int, int> GetNumberOfOccupiedDaysPerYear(List<AccommodationReservation> accommodationReservations)
        {
            Dictionary<int, int> reservationsPerYear = new Dictionary<int, int>();

            foreach (var reservation in accommodationReservations)
            {
                if (!reservationsPerYear.ContainsKey(reservation.StartDate.Year))
                {
                    reservationsPerYear[reservation.StartDate.Year] = 0;
                }

                reservationsPerYear[reservation.StartDate.Year] += (reservation.EndDate - reservation.StartDate).Days;
            }

            return reservationsPerYear;
        }

        private int FindMostOccupiedYear(Dictionary<int, int> reservationsPerYear)
        {
            int mostOccupiedYear = 0;
            double occupiance = 0;

            foreach (var year in reservationsPerYear.Keys)
            {
                if (occupiance < CalculateOccupationPeriod(reservationsPerYear[year]))
                {
                    occupiance = CalculateOccupationPeriod((reservationsPerYear[year]));
                    mostOccupiedYear = year;
                }
            }

            return mostOccupiedYear;
        }

        private double CalculateOccupationPeriod(int numberOfDays)
        {
            double daysInYear = 365.25;
            return numberOfDays / daysInYear;
        }
    }
}
