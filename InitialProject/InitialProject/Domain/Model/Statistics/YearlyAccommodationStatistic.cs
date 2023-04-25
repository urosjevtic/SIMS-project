using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Users;
using System.Xml.Linq;
using InitialProject.Serializer;
using Microsoft.VisualBasic.ApplicationServices;

namespace InitialProject.Domain.Model.Statistics
{
    public class YearlyAccommodationStatistic : ISerializable
    {
        public int AccommodationId { get; set; }

        public List<AccommodationStatistic> Statistics { get; set; }


        public YearlyAccommodationStatistic()
        {
            Statistics = new List<AccommodationStatistic>();
        }

        public YearlyAccommodationStatistic(int accommodationId, List<AccommodationStatistic> statistics)
        {
            AccommodationId = accommodationId;
            Statistics = statistics;

        }

        public string[] ToCSV()
        {
            string[] csvValues = { AccommodationId.ToString()  };
            if (Statistics.Count() > 0)
            {
                foreach (AccommodationStatistic statistic in Statistics)
                {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = statistic.Year.ToString() + "(" + statistic.ReservationsCount.ToString() + "," + statistic.ReschedulesCount.ToString() + "," + statistic.CancelationsCount.ToString() + "," + statistic.RenovationsCount.ToString() + ")";
                }
            }

            Array.Resize(ref csvValues, csvValues.Length + 1);
            csvValues[csvValues.Length - 1] = "[END]";

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            AccommodationId = Convert.ToInt32(values[0]);
            int i = 1;

            while (values[i] != "[END]")
            {
                string statisticNotParesed = values[i];
                string[] splitedWord = statisticNotParesed.Split("(");
                string year = splitedWord[0];
                string stats = splitedWord[1].Remove(splitedWord[1].Length - 1);
                string[] statovi = stats.Split(',');

                AccommodationStatistic statistic = new AccommodationStatistic();
                statistic.Year = DateTime.Parse(year);
                statistic.ReservationsCount = Convert.ToInt32(statovi[0]);
                statistic.ReschedulesCount = Convert.ToInt32(statovi[1]);
                statistic.CancelationsCount = Convert.ToInt32(statovi[2]);
                statistic.RenovationsCount = Convert.ToInt32(statovi[3]);

                Statistics.Add(statistic);

                i++;
            }

        }
    }
}
