using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Statistics;
using InitialProject.Domain.RepositoryInterfaces.IStatisticsRepo;
using InitialProject.Repository.StatisticRepo;

namespace InitialProject.Service.StatisticService
{
    public class MonthlyAccommodationStatisticService
    {

        private readonly IMonthlyAccommodationStatisticRepository _monthlyAccommodationStatisticRepository;

        public MonthlyAccommodationStatisticService()
        {
            _monthlyAccommodationStatisticRepository =
                Injector.Injector.CreateInstance<IMonthlyAccommodationStatisticRepository>();

        }


        public AccommodationStatistic GetMonthlyStatistics(string month, DateTime year, int accommodationId)
        {
            MonthlyAccommodationStatistics monthlyAccommodationStatistics = _monthlyAccommodationStatisticRepository.GetByAccommodationId(accommodationId);
            switch (month)
            {
                case "January":
                    foreach (var stat in monthlyAccommodationStatistics.JanuaryStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
                case "February":
                    foreach (var stat in monthlyAccommodationStatistics.FebruaryStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
                case "March":
                    foreach (var stat in monthlyAccommodationStatistics.MarchStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
                case "April":
                    foreach (var stat in monthlyAccommodationStatistics.AprilStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
                case "May":
                    foreach (var stat in monthlyAccommodationStatistics.MayStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
                case "Jun":
                    foreach (var stat in monthlyAccommodationStatistics.JunStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
                case "July":
                    foreach (var stat in monthlyAccommodationStatistics.JulyStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
                case "August":
                    foreach (var stat in monthlyAccommodationStatistics.AugustStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
                case "September":
                    foreach (var stat in monthlyAccommodationStatistics.SeptemberStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
                case "October":
                    foreach (var stat in monthlyAccommodationStatistics.OctoberStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
                case "November":
                    foreach (var stat in monthlyAccommodationStatistics.NovemberStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
                case "December":
                    foreach (var stat in monthlyAccommodationStatistics.DecemberStatistics)
                    {
                        if (stat.Year.Year == year.Year)
                            return stat;
                    }
                    break;
            }

            return new AccommodationStatistic();
        }


        public void CreateStatisticForNewAccommodation(int accommodationId)
        {
            List<AccommodationStatistic> list = new List<AccommodationStatistic>();
            AccommodationStatistic accommodationStatistic = new AccommodationStatistic();
            list.Add(accommodationStatistic);
            MonthlyAccommodationStatistics monthlyAccommodationStatistics =
                new MonthlyAccommodationStatistics(accommodationId, list, list, list, list, list, list, list, list,
                    list, list, list, list);
            _monthlyAccommodationStatisticRepository.Save(monthlyAccommodationStatistics);
        }
        public void IncreaseCancelationCount(int accommodationId)
        {
            MonthlyAccommodationStatistics statistics =
                _monthlyAccommodationStatisticRepository.GetByAccommodationId(accommodationId);

            switch (DateTime.Today.Month)
            {
                case 1:
                    foreach (var stat in statistics.JanuaryStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 2:
                    foreach (var stat in statistics.FebruaryStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 3:
                    foreach (var stat in statistics.MarchStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 4:
                    foreach (var stat in statistics.AprilStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 5:
                    foreach (var stat in statistics.MayStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 6:
                    foreach (var stat in statistics.JunStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 7:
                    foreach (var stat in statistics.JulyStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 8:
                    foreach (var stat in statistics.AugustStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 9:
                    foreach (var stat in statistics.SeptemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 10:
                    foreach (var stat in statistics.OctoberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 11:
                    foreach (var stat in statistics.NovemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 12:
                    foreach (var stat in statistics.DecemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.CancelationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
            }
        }
        public void IncreaseReservationCount(int accommodationId)
        {
            MonthlyAccommodationStatistics statistics =
                _monthlyAccommodationStatisticRepository.GetByAccommodationId(accommodationId);

            switch (DateTime.Today.Month)
            {
                case 1:
                    foreach (var stat in statistics.JanuaryStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 2:
                    foreach (var stat in statistics.FebruaryStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 3:
                    foreach (var stat in statistics.MarchStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 4:
                    foreach (var stat in statistics.AprilStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 5:
                    foreach (var stat in statistics.MayStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 6:
                    foreach (var stat in statistics.JunStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 7:
                    foreach (var stat in statistics.JulyStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 8:
                    foreach (var stat in statistics.AugustStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 9:
                    foreach (var stat in statistics.SeptemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 10:
                    foreach (var stat in statistics.OctoberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 11:
                    foreach (var stat in statistics.NovemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 12:
                    foreach (var stat in statistics.DecemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReservationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
            }
        }
        public void IncreaseRescheduleCount(int accommodationId)
        {
            MonthlyAccommodationStatistics statistics =
                _monthlyAccommodationStatisticRepository.GetByAccommodationId(accommodationId);

            switch (DateTime.Today.Month)
            {
                case 1:
                    foreach (var stat in statistics.JanuaryStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 2:
                    foreach (var stat in statistics.FebruaryStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 3:
                    foreach (var stat in statistics.MarchStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 4:
                    foreach (var stat in statistics.AprilStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 5:
                    foreach (var stat in statistics.MayStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 6:
                    foreach (var stat in statistics.JunStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 7:
                    foreach (var stat in statistics.JulyStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 8:
                    foreach (var stat in statistics.AugustStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 9:
                    foreach (var stat in statistics.SeptemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 10:
                    foreach (var stat in statistics.OctoberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 11:
                    foreach (var stat in statistics.NovemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 12:
                    foreach (var stat in statistics.DecemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.ReschedulesCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
            }
        }
        public void IncreaseRenovationCount(int accommodationId)
        {
            MonthlyAccommodationStatistics statistics =
                _monthlyAccommodationStatisticRepository.GetByAccommodationId(accommodationId);

            switch (DateTime.Today.Month)
            {
                case 1:
                    foreach (var stat in statistics.JanuaryStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 2:
                    foreach (var stat in statistics.FebruaryStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 3:
                    foreach (var stat in statistics.MarchStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 4:
                    foreach (var stat in statistics.AprilStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 5:
                    foreach (var stat in statistics.MayStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 6:
                    foreach (var stat in statistics.JunStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 7:
                    foreach (var stat in statistics.JulyStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 8:
                    foreach (var stat in statistics.AugustStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 9:
                    foreach (var stat in statistics.SeptemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 10:
                    foreach (var stat in statistics.OctoberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 11:
                    foreach (var stat in statistics.NovemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
                case 12:
                    foreach (var stat in statistics.DecemberStatistics)
                    {
                        if (stat.Year.Year == DateTime.Now.Year)
                        {
                            stat.RenovationsCount++;
                            _monthlyAccommodationStatisticRepository.Update(statistics);
                            break;
                        }
                    }
                    break;
            }
        }
    }
}
