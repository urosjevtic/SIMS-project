using InitialProject.Domain.Model.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;
using InitialProject.Repository.StatisticRepo;

namespace InitialProject.Service.StatisticService
{
    public class AccommodationStatisticService
    {

        private readonly IAccommodationStatisticsDataRepository _statisticsRepository;


        public AccommodationStatisticService()
        {
            _statisticsRepository = new AccommodationStatisticDataRepository();
        }
        public void CreateStatisticForNewAccommodation(int accommodationId)
        {
            List<AccommodationStatistic> list = new List<AccommodationStatistic>();
            AccommodationStatistic accommodationStatistic = new AccommodationStatistic(DateTime.Now, 0, 0, 0, 0);
            list.Add(accommodationStatistic);
            AccommodationStatisticData statisticData =
                new AccommodationStatisticData(accommodationId, list);
            _statisticsRepository.Save(statisticData);
        }


        public void IncreasCancelationCount(int accommodationId)
        {
            AccommodationStatisticData statisticData = _statisticsRepository.GetByAccommodationId(accommodationId);
            foreach (var statistic in statisticData.Statistics)
            {
                if (statistic.MonthAndYear.Month == DateTime.Now.Month && statistic.MonthAndYear.Year == DateTime.Now.Year)
                {
                    statistic.CancelationsCount++;
                    _statisticsRepository.Update(statisticData);
                    return;
                }
            }

            statisticData.Statistics.Add(new AccommodationStatistic(DateTime.Today, 0, 0, 1, 0));
            _statisticsRepository.Update(statisticData);

        }


        public void IncreaseRescheduleCount(int accommodationId)
        {
            AccommodationStatisticData statisticData = _statisticsRepository.GetByAccommodationId(accommodationId);
            foreach (var statistic in statisticData.Statistics)
            {
                if (statistic.MonthAndYear.Month == DateTime.Now.Month && statistic.MonthAndYear.Year == DateTime.Now.Year)
                {
                    statistic.ReschedulesCount++;
                    _statisticsRepository.Update(statisticData);
                    return;
                }
            }

            statisticData.Statistics.Add(new AccommodationStatistic(DateTime.Today, 0, 1, 0, 0));
            _statisticsRepository.Update(statisticData);
        }

        public void IncreaseRenovationCount(int accommodationId)
        {
            AccommodationStatisticData statisticData = _statisticsRepository.GetByAccommodationId(accommodationId);
            foreach (var statistic in statisticData.Statistics)
            {
                if (statistic.MonthAndYear.Month == DateTime.Now.Month && statistic.MonthAndYear.Year == DateTime.Now.Year)
                {
                    statistic.RenovationsCount++;
                    _statisticsRepository.Update(statisticData);
                    return;
                }
            }

            statisticData.Statistics.Add(new AccommodationStatistic(DateTime.Today, 0, 0, 1, 1));
            _statisticsRepository.Update(statisticData);
        }

        public void IncreaseReservationCount(int accommodationId)
        {
            AccommodationStatisticData statisticData = _statisticsRepository.GetByAccommodationId(accommodationId);
            foreach (var statistic in statisticData.Statistics)
            {
                if (statistic.MonthAndYear.Month == DateTime.Now.Month && statistic.MonthAndYear.Year == DateTime.Now.Year)
                {
                    statistic.ReservationsCount++;
                    _statisticsRepository.Update(statisticData);
                    return;
                }
            }

            statisticData.Statistics.Add(new AccommodationStatistic(DateTime.Today, 1, 0, 1, 0));
            _statisticsRepository.Update(statisticData);
        }
    }
}
