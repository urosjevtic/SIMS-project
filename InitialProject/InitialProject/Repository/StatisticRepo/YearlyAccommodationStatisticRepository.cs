using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;
using InitialProject.Serializer;
using Microsoft.VisualBasic.ApplicationServices;

namespace InitialProject.Repository.StatisticRepo
{
    public class YearlyAccommodationStatisticRepository : IYearlyAccommodationStatisticsRepository
    {
        private const string FilePath = "../../../Resources/Data/YearlyAccommodationStatistics.csv";

        private readonly Serializer<YearlyAccommodationStatistic> _serializer;

        private List<YearlyAccommodationStatistic> _statistics;


        public YearlyAccommodationStatisticRepository()
        {
            _serializer = new Serializer<YearlyAccommodationStatistic>();
            _statistics = _serializer.FromCSV(FilePath);
        }


        public List<YearlyAccommodationStatistic> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public YearlyAccommodationStatistic GetByAccommodationId(int accommodationId)
        {
            _statistics = _serializer.FromCSV(FilePath);
            return _statistics.FirstOrDefault(a => a.AccommodationId == accommodationId);
        }

        public void Save(YearlyAccommodationStatistic yearlyAccommodationStatistic)
        {
            _statistics = _serializer.FromCSV(FilePath);
            _statistics.Add(yearlyAccommodationStatistic);
            _serializer.ToCSV(FilePath, _statistics);
        }
    }
}
