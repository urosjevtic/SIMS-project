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
    public class AccommodationStatisticData : ISerializable
    {
        public int AccommodationId { get; set; }

        public List<AccommodationStatistic> Statistics { get; set; }


        public AccommodationStatisticData()
        {
            Statistics = new List<AccommodationStatistic>();
        }

        public AccommodationStatisticData(int accommodationId, List<AccommodationStatistic> statistics)
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
                    csvValues[csvValues.Length - 1] = statistic.MonthAndYear.ToString() +
                                                      "(" + statistic.ReservationsCount.ToString() + 
                                                      "," + statistic.ReschedulesCount.ToString() + 
                                                      "," + statistic.CancelationsCount.ToString() + 
                                                      "," + statistic.RenovationsCount.ToString() + ")";
                }
            }

            Array.Resize(ref csvValues, csvValues.Length + 1);
            csvValues[csvValues.Length - 1] = "[END]";

            return csvValues;
        }

        public void FromCSV(string[] csvValues)
        {
            AccommodationId = Convert.ToInt32(csvValues[0]);

            for (int i = 1; i < csvValues.Length; i++)
            {
                if (csvValues[i] == "[END]")
                {
                    break;
                }

                string statisticString = csvValues[i];
                string[] statisticParts = statisticString.Split("(");
                string year = statisticParts[0];
                string statsString = statisticParts[1].Remove(statisticParts[1].Length - 1);
                string[] statValues = statsString.Split(',');

                AccommodationStatistic statistic = new AccommodationStatistic
                {
                    MonthAndYear = DateTime.Parse(year),
                    ReservationsCount = Convert.ToInt32(statValues[0]),
                    ReschedulesCount = Convert.ToInt32(statValues[1]),
                    CancelationsCount = Convert.ToInt32(statValues[2]),
                    RenovationsCount = Convert.ToInt32(statValues[3])
                };

                Statistics.Add(statistic);
            }
        }
    }
}
