using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;
using InitialProject.Serializer;

namespace InitialProject.Repository.StatisticRepo
{
    public class MonthlyAccommodationStatisticRepository : IMonthlyAccommodationStatisticRepository
    {
        private const string FilePath = "../../../Resources/Data/monthlyAccommodationStatistics.csv";

        private readonly Serializer<MonthlyAccommodationStatistics> _serializer;

        private List<MonthlyAccommodationStatistics> _statistics;


        public MonthlyAccommodationStatisticRepository()
        {
            _serializer = new Serializer<MonthlyAccommodationStatistics>();
            _statistics = _serializer.FromCSV(FilePath);
        }
        public List<MonthlyAccommodationStatistics> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(MonthlyAccommodationStatistics monthlyAccommodationStatistics)
        {
            _statistics = _serializer.FromCSV(FilePath);
            _statistics.Add(monthlyAccommodationStatistics);
            _serializer.ToCSV(FilePath, _statistics);
        }

        public void Update(MonthlyAccommodationStatistics monthlyAccommodationStatistics)
        {
            _statistics = _serializer.FromCSV(FilePath);
            foreach (var statistic in _statistics)
            {
                if (statistic.AccommodationId == monthlyAccommodationStatistics.AccommodationId)
                {
                    _statistics.Remove(statistic);
                    _statistics.Add(monthlyAccommodationStatistics);
                    break;
                }
            }

            _serializer.ToCSV(FilePath, _statistics);
        }

        public MonthlyAccommodationStatistics GetByAccommodationId(int accommodationId)
        {
            _statistics = _serializer.FromCSV(FilePath);
            return _statistics.FirstOrDefault(a => a.AccommodationId == accommodationId);
        }
    }
}
