using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;

namespace InitialProject.Service.StatisticService
{
    public class YearlyAccommodationService
    {
        private readonly IAccommodationStatisticsDataRepository _accommodationStatisticsDataRepository;

        public YearlyAccommodationService()
        {
            _accommodationStatisticsDataRepository = Injector.Injector.CreateInstance<IAccommodationStatisticsDataRepository>();
        }


        public List<AccommodationStatistic> GetStatisticByAccommodationId(int accommodationId)
        {
            Dictionary<int, AccommodationStatistic> yearlyStatistics = new Dictionary<int, AccommodationStatistic>();

            foreach (var statistic in _accommodationStatisticsDataRepository.GetByAccommodationId(accommodationId).Statistics)
            {
                int year = statistic.MonthAndYear.Year;

                if (!yearlyStatistics.ContainsKey(year))
                {
                    AccommodationStatistic newStatistic = new AccommodationStatistic();
                    newStatistic.MonthAndYear = new DateTime(year, 1, 1);
                    yearlyStatistics.Add(year, newStatistic);
                }

                yearlyStatistics[year].RenovationsCount += statistic.RenovationsCount;
                yearlyStatistics[year].CancelationsCount += statistic.CancelationsCount;
                yearlyStatistics[year].ReservationsCount += statistic.ReservationsCount;
                yearlyStatistics[year].ReschedulesCount += statistic.ReschedulesCount;
            }

            return yearlyStatistics.Values.ToList();
        }




        public DateTime GetYearWithMostReservations(int accommodationId)
        {
            AccommodationStatisticData statisticData = _accommodationStatisticsDataRepository.GetByAccommodationId(accommodationId);
            AccommodationStatistic yearWithMostReservations = statisticData.Statistics[0];
            foreach (var statistic in statisticData.Statistics)
            {
                if (statistic.ReservationsCount > yearWithMostReservations.ReservationsCount)
                {
                    yearWithMostReservations = statistic;
                }
            }

            return yearWithMostReservations.MonthAndYear;
        }

    }
}
