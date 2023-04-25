using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;

namespace InitialProject.Service.StatisticService
{
    public class YearlyAccommodationService
    {
        private readonly IYearlyAccommodationStatisticsRepository _yearlyAccommodationStatisticsRepository;

        public YearlyAccommodationService()
        {
            _yearlyAccommodationStatisticsRepository = Injector.Injector.CreateInstance<IYearlyAccommodationStatisticsRepository>();
        }


        public List<AccommodationStatistic> GetStatisticByAccommodationId(int accommodationId)
        {
           return _yearlyAccommodationStatisticsRepository.GetByAccommodationId(accommodationId).Statistics;
        }

        public void CreateStatisticForNewAccommodation(int accommodationId)
        {
            List<AccommodationStatistic> list = new List<AccommodationStatistic>();
            AccommodationStatistic accommodationStatistic = new AccommodationStatistic();
            list.Add(accommodationStatistic);
            YearlyAccommodationStatistic statistic =
                new YearlyAccommodationStatistic(accommodationId, list);
            _yearlyAccommodationStatisticsRepository.Save(statistic);
        }

    }
}
