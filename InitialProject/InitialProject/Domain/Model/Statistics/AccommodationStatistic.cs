using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model.Statistics
{
    public class AccommodationStatistic
    {
        public DateTime MonthAndYear { get; set; }

        public int ReservationsCount { get; set; }
        public int ReschedulesCount { get; set; }
        public int CancelationsCount { get; set; }
        public int RenovationsCount { get; set; }

        public AccommodationStatistic()
        {
            ReservationsCount = 0;
            ReschedulesCount = 0;
            CancelationsCount = 0;
            RenovationsCount = 0;

        }

        public AccommodationStatistic(DateTime monthAndYear, int reservationsCount, int reschedulesCount, int cancelationsCount, int renovationsCount)
        {
            MonthAndYear = monthAndYear;
            ReservationsCount = reservationsCount;
            ReschedulesCount = reschedulesCount;
            CancelationsCount = cancelationsCount;
            RenovationsCount = renovationsCount;
        }
    }
}
