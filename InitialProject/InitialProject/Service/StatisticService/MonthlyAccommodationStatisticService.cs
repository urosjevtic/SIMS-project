using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;
using InitialProject.Repository.StatisticRepo;
using InitialProject.Service.ReservationServices;

namespace InitialProject.Service.StatisticService
{
    public class MonthlyAccommodationStatisticService
    {

        private readonly IAccommodationStatisticsDataRepository _accommodationStatisticsDataRepository;
        private readonly AccommodationReservationService _accommodationReservationService;

        public MonthlyAccommodationStatisticService()
        {
            _accommodationStatisticsDataRepository =
                Injector.Injector.CreateInstance<IAccommodationStatisticsDataRepository>();

            _accommodationReservationService = new AccommodationReservationService();

        }


        public AccommodationStatistic GetMonthlyStatistics(string month, int year, int accommodationId)
        {
            AccommodationStatisticData monthlyAccommodationStatisticsData = _accommodationStatisticsDataRepository.GetByAccommodationId(accommodationId);
            Dictionary<int, AccommodationStatistic> monthlyStatistics =
                GetMonthlyStatisticsForYear(year, monthlyAccommodationStatisticsData.Statistics);


            return GetStatisticsForChoosenMonth(monthlyStatistics, month);
        }

        private AccommodationStatistic GetStatisticsForChoosenMonth(
            Dictionary<int, AccommodationStatistic> monthlyStatistics, string month)
        {
            switch (month)
            {
                case "January":
                    return monthlyStatistics[1];
                case "February":
                    return monthlyStatistics[2];
                case "March":
                    return monthlyStatistics[3];
                case "April":
                    return monthlyStatistics[4];
                case "May":
                    return monthlyStatistics[5];
                case "Jun":
                    return monthlyStatistics[6];
                case "July":
                    return monthlyStatistics[7];
                case "August":
                    return monthlyStatistics[8];
                case "September":
                    return monthlyStatistics[9];
                case "October":
                    return monthlyStatistics[10];
                case "November":
                    return monthlyStatistics[11];
                case "December":
                    return monthlyStatistics[12];
            }

            return new AccommodationStatistic();
        }

        public List<DateTime> GetAvailableMonths(int accommodationId, int year)
        {
            AccommodationStatisticData accommodationStatisticData = _accommodationStatisticsDataRepository.GetByAccommodationId(accommodationId);
            Dictionary<int, AccommodationStatistic> monthlyStatistics = GetMonthlyStatisticsForYear(year, accommodationStatisticData.Statistics);
            List<DateTime> availableMonths = monthlyStatistics.Values.Select(s => s.MonthAndYear).ToList();

            return availableMonths;
        }

        private Dictionary<int, AccommodationStatistic> GetMonthlyStatisticsForYear(int year, List<AccommodationStatistic> statistics)
        {
            Dictionary<int, AccommodationStatistic> monthlyStatistics = new Dictionary<int, AccommodationStatistic>();

            foreach (var statistic in statistics.Where(s => s.MonthAndYear.Year == year))
            {
                if (!monthlyStatistics.ContainsKey(statistic.MonthAndYear.Month))
                {
                    monthlyStatistics[statistic.MonthAndYear.Month] = new AccommodationStatistic
                    {
                        MonthAndYear = new DateTime(year, statistic.MonthAndYear.Month, 1)
                    };
                }

                var monthlyStatistic = monthlyStatistics[statistic.MonthAndYear.Month];
                monthlyStatistic.RenovationsCount = statistic.RenovationsCount;
                monthlyStatistic.CancelationsCount = statistic.CancelationsCount;
                monthlyStatistic.ReservationsCount = statistic.ReservationsCount;
                monthlyStatistic.ReschedulesCount = statistic.ReschedulesCount;
            }

            return monthlyStatistics;
        }

        public int GetMostOccupiedMonth(int accommodationId, int selectedYear)
        {
            List<AccommodationReservation> accommodationReservations = _accommodationReservationService.GetReservationsByAccommodationId(accommodationId);
            Dictionary<int, int> occupiedDaysPerMonth = GetNumberOfOccupiedDaysPerMonth(accommodationReservations, selectedYear);

            return FindMostOccupiedMonth(occupiedDaysPerMonth);
        }


        private Dictionary<int, int> GetNumberOfOccupiedDaysPerMonth(List<AccommodationReservation> accommodationReservations, int selectedYear)
        {
            Dictionary<int, int> reservationsPerMonth = new Dictionary<int, int>();

            foreach (var reservation in accommodationReservations.Where(a => a.StartDate.Year == selectedYear))
            {
                if (!reservationsPerMonth.ContainsKey(reservation.StartDate.Month))
                {
                    reservationsPerMonth[reservation.StartDate.Month] = 0;
                }

                reservationsPerMonth[reservation.StartDate.Month] += (reservation.EndDate - reservation.StartDate).Days;
            }

            return reservationsPerMonth;
        }

        private int FindMostOccupiedMonth(Dictionary<int, int> reservationsPerYear)
        {
            int mostOccupiedMonth = 0;
            double occupiance = 0;

            foreach (var month in reservationsPerYear.Keys)
            {
                if (occupiance < CalculateOccupationPeriod(reservationsPerYear[month]))
                {
                    occupiance = CalculateOccupationPeriod((reservationsPerYear[month]));
                    mostOccupiedMonth = month;
                }
            }

            return mostOccupiedMonth;
        }

        private double CalculateOccupationPeriod(int numberOfDays)
        {
            double daysInMonth = 30.437;
            return numberOfDays / daysInMonth;
        }

    }
}
