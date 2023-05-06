using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model.Statistics
{
    public class AccommodationStatistic
    {
        public DateTime Year { get; set; }

        public int ReservationsCount { get; set; }
        public int ReschedulesCount { get; set; }
        public int CancelationsCount { get; set; }
        public int RenovationsCount { get; set; }

        public AccommodationStatistic()
        {
            Year = DateTime.Now;
            ReservationsCount = 0;
            ReschedulesCount = 0;
            CancelationsCount = 0;
            RenovationsCount = 0;

        }

        public AccommodationStatistic(DateTime year, int reservationsCount, int reschedulesCount, int cancelationsCount, int renovationsCount)
        {
            Year = year;
            ReservationsCount = reservationsCount;
            ReschedulesCount = reschedulesCount;
            CancelationsCount = cancelationsCount;
            RenovationsCount = renovationsCount;
        }
    }
}
