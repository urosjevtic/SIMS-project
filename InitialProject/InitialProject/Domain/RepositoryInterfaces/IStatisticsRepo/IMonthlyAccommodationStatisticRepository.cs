using InitialProject.Domain.Model.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo
{
    public interface IMonthlyAccommodationStatisticRepository
    {
        public List<MonthlyAccommodationStatistics> GetAll();

        public void Save(MonthlyAccommodationStatistics yearlyAccommodationStatistic);

        public void Update(MonthlyAccommodationStatistics yearlyAccommodationStatistic);

        public MonthlyAccommodationStatistics GetByAccommodationId(int accommodationId);
    }
}
