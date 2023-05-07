using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Statistics;

namespace InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo
{
    public interface IAccommodationStatisticsDataRepository
    {
        public List<AccommodationStatisticData> GetAll();

        public void Save(AccommodationStatisticData accommodationStatisticData);

        public void Update(AccommodationStatisticData accommodationStatisticData);

        public AccommodationStatisticData GetByAccommodationId(int accommodationId);
    }
}
