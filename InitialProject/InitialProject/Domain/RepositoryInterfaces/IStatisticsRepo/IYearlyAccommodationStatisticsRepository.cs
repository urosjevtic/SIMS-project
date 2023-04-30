using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Statistics;

namespace InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo
{
    public interface IYearlyAccommodationStatisticsRepository
    {
        public List<YearlyAccommodationStatistic> GetAll();

        public void Save(YearlyAccommodationStatistic yearlyAccommodationStatistic);

        public void Update(YearlyAccommodationStatistic yearlyAccommodationStatistic);

        public YearlyAccommodationStatistic GetByAccommodationId(int accommodationId);
    }
}
