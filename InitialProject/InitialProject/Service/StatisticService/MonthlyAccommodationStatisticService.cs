using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;
using InitialProject.Repository.StatisticRepo;

namespace InitialProject.Service.StatisticService
{
    public class MonthlyAccommodationStatisticService
    {

        private readonly IAccommodationStatisticsDataRepository _accommodationStatisticsDataRepository;

        public MonthlyAccommodationStatisticService()
        {
            _accommodationStatisticsDataRepository =
                Injector.Injector.CreateInstance<IAccommodationStatisticsDataRepository>();

        }


        public AccommodationStatistic GetMonthlyStatistics(string month, int year, int accommodationId)
        {
            AccommodationStatisticData monthlyAccommodationStatisticsData = _accommodationStatisticsDataRepository.GetByAccommodationId(accommodationId);
            Dictionary<int, AccommodationStatistic> monthlyStatistics =
                GetStatisticForYear(year, monthlyAccommodationStatisticsData.Statistics);


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
            AccommodationStatisticData monthlyAccommodationStatisticsData = _accommodationStatisticsDataRepository.GetByAccommodationId(accommodationId);
            Dictionary<int, AccommodationStatistic> monthlyStatisticForYear = GetStatisticForYear(year, monthlyAccommodationStatisticsData.Statistics);
            List<DateTime> availableMonths = new List<DateTime>();
            foreach (var statistic in monthlyStatisticForYear)
            {
                availableMonths.Add(statistic.Value.MonthAndYear);
            }

            return availableMonths;
        }

        private Dictionary<int, AccommodationStatistic> GetStatisticForYear(int year, List<AccommodationStatistic> statistics)
        {
            Dictionary<int, AccommodationStatistic> monthlyStatistic = new Dictionary<int, AccommodationStatistic>();
            List<AccommodationStatistic> statisticForYear = new List<AccommodationStatistic>();
            foreach (var statistic in statistics)
            {
                if (statistic.MonthAndYear.Year == year)
                {
                    if (!monthlyStatistic.ContainsKey(statistic.MonthAndYear.Month))
                    {
                        AccommodationStatistic newStatistic = new AccommodationStatistic();
                        newStatistic.MonthAndYear = new DateTime(1, statistic.MonthAndYear.Month, 1);
                        monthlyStatistic.Add(statistic.MonthAndYear.Month, newStatistic);
                    }
                    monthlyStatistic[statistic.MonthAndYear.Month].RenovationsCount = statistic.RenovationsCount;
                    monthlyStatistic[statistic.MonthAndYear.Month].CancelationsCount = statistic.CancelationsCount;
                    monthlyStatistic[statistic.MonthAndYear.Month].ReservationsCount = statistic.ReservationsCount;
                    monthlyStatistic[statistic.MonthAndYear.Month].ReschedulesCount = statistic.ReschedulesCount;

                }
            }


            return monthlyStatistic;
        }

       
    }
}
