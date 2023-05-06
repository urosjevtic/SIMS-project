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
    public class AccommodationStatisticDataRepository : IAccommodationStatisticsDataRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationStatisticsData.csv";

        private readonly Serializer<AccommodationStatisticData> _serializer;

        private List<AccommodationStatisticData> _statistics;


        public AccommodationStatisticDataRepository()
        {
            _serializer = new Serializer<AccommodationStatisticData>();
            _statistics = _serializer.FromCSV(FilePath);
        }


        public List<AccommodationStatisticData> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Update(AccommodationStatisticData accommodationStatisticData)
        {
            _statistics = _serializer.FromCSV(FilePath);
            foreach (var statistic in _statistics)
            {
                if (statistic.AccommodationId == accommodationStatisticData.AccommodationId)
                {
                    _statistics.Remove(statistic);
                    _statistics.Add(accommodationStatisticData);
                    break;
                }
            }

            _serializer.ToCSV(FilePath, _statistics);
        }

        public AccommodationStatisticData GetByAccommodationId(int accommodationId)
        {
            _statistics = _serializer.FromCSV(FilePath);
            return _statistics.FirstOrDefault(a => a.AccommodationId == accommodationId);
        }

        public void Save(AccommodationStatisticData accommodationStatisticData)
        {
            _statistics = _serializer.FromCSV(FilePath);
            _statistics.Add(accommodationStatisticData);
            _serializer.ToCSV(FilePath, _statistics);
        }
    }
}
