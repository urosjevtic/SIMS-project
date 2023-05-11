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
            AccommodationStatistic newStatistic = CreateAccommodationStatistic();
            AccommodationStatisticData statisticData = new AccommodationStatisticData(accommodationId, new List<AccommodationStatistic> { newStatistic });
            _statisticsRepository.Save(statisticData);
        }

        private static AccommodationStatistic CreateAccommodationStatistic()
        {
            return new AccommodationStatistic
            {
                MonthAndYear = DateTime.Now,
                CancelationsCount = 0,
                ReschedulesCount = 0,
                RenovationsCount = 0,
                ReservationsCount = 0
            };
        }


        public void IncreaseCancelationCount(int accommodationId)
        {
            UpdateStatistic(accommodationId, s => s.CancelationsCount++);
        }

        public void IncreaseRescheduleCount(int accommodationId)
        {
            UpdateStatistic(accommodationId, s => s.ReschedulesCount++);
        }

        public void IncreaseRenovationCount(int accommodationId)
        {
            UpdateStatistic(accommodationId, s => s.RenovationsCount++);
        }

        public void IncreaseReservationCount(int accommodationId)
        {
            UpdateStatistic(accommodationId, s => s.ReservationsCount++);
        }

        private void UpdateStatistic(int accommodationId, Action<AccommodationStatistic> updateAction)
        {
            var statisticData = _statisticsRepository.GetByAccommodationId(accommodationId);
            var statistic = statisticData.Statistics.FirstOrDefault(s => s.MonthAndYear.Year == DateTime.Now.Year && s.MonthAndYear.Month == DateTime.Now.Month);
            if (statistic == null)
            {
                statistic = new AccommodationStatistic(DateTime.Today, 0, 0, 0, 0);
                statisticData.Statistics.Add(statistic);
            }
            updateAction(statistic);
            _statisticsRepository.Update(statisticData);
        }
    }
}
