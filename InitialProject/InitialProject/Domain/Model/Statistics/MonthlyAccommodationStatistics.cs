using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model.Statistics
{
    public class MonthlyAccommodationStatistics : ISerializable
    {
        public int AccommodationId { get; set; }
        public List<AccommodationStatistic> JanuaryStatistics { get; set; }
        public List<AccommodationStatistic> FebruaryStatistics { get; set; }

        public List<AccommodationStatistic> MarchStatistics { get; set; }
        public List<AccommodationStatistic> AprilStatistics { get; set; }
        public List<AccommodationStatistic> MayStatistics { get; set; }
        public List<AccommodationStatistic> JunStatistics { get; set; }
        public List<AccommodationStatistic> JulyStatistics { get; set; }
        public List<AccommodationStatistic> AugustStatistics { get; set; }
        public List<AccommodationStatistic> SeptemberStatistics { get; set; }
        public List<AccommodationStatistic> OctoberStatistics { get; set; }
        public List<AccommodationStatistic> NovemberStatistics { get; set; }
        public List<AccommodationStatistic> DecemberStatistics { get; set; }

        public MonthlyAccommodationStatistics()
        {
            JanuaryStatistics = new List<AccommodationStatistic>();
            FebruaryStatistics = new List<AccommodationStatistic>();
            MarchStatistics = new List<AccommodationStatistic>();
            AprilStatistics = new List<AccommodationStatistic>();
            MayStatistics = new List<AccommodationStatistic>();
            JunStatistics = new List<AccommodationStatistic>();
            JulyStatistics = new List<AccommodationStatistic>();
            AugustStatistics = new List<AccommodationStatistic>();
            SeptemberStatistics = new List<AccommodationStatistic>();
            OctoberStatistics = new List<AccommodationStatistic>();
            NovemberStatistics = new List<AccommodationStatistic>();
            DecemberStatistics = new List<AccommodationStatistic>();
        }

        public MonthlyAccommodationStatistics(int accommodationId, List<AccommodationStatistic> januaryStatistics, List<AccommodationStatistic> februaryStatistics, List<AccommodationStatistic> marchStatistics, List<AccommodationStatistic> aprilStatistics, List<AccommodationStatistic> mayStatistics, List<AccommodationStatistic> junStatistics, List<AccommodationStatistic> julyStatistics, List<AccommodationStatistic> augustStatistics, List<AccommodationStatistic> septemberStatistics, List<AccommodationStatistic> octoberStatistics, List<AccommodationStatistic> novemberStatistics, List<AccommodationStatistic> decemberStatistics)
        {
            AccommodationId = accommodationId;
            JanuaryStatistics = januaryStatistics;
            FebruaryStatistics = februaryStatistics;
            MarchStatistics = marchStatistics;
            AprilStatistics = aprilStatistics;
            MayStatistics = mayStatistics;
            JunStatistics = junStatistics;
            JulyStatistics = julyStatistics;
            AugustStatistics = augustStatistics;
            SeptemberStatistics = septemberStatistics;
            OctoberStatistics = octoberStatistics;
            NovemberStatistics = novemberStatistics;
            DecemberStatistics = decemberStatistics;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { AccommodationId.ToString() };
            csvValues = ToCSVMonthly(csvValues, JanuaryStatistics);
            csvValues = ToCSVMonthly(csvValues, FebruaryStatistics);
            csvValues = ToCSVMonthly(csvValues, MarchStatistics);
            csvValues = ToCSVMonthly(csvValues, AprilStatistics);
            csvValues = ToCSVMonthly(csvValues, MayStatistics);
            csvValues = ToCSVMonthly(csvValues, JunStatistics);
            csvValues = ToCSVMonthly(csvValues, JulyStatistics);
            csvValues = ToCSVMonthly(csvValues, AugustStatistics);
            csvValues = ToCSVMonthly(csvValues, SeptemberStatistics);
            csvValues = ToCSVMonthly(csvValues, OctoberStatistics);
            csvValues = ToCSVMonthly(csvValues, NovemberStatistics);
            csvValues = ToCSVMonthly(csvValues, DecemberStatistics);
            return csvValues;
        }

        private string[] ToCSVMonthly(string[] csvValues, List<AccommodationStatistic> monthlyStatistic)
        {
            if (monthlyStatistic.Count() > 0)
            {
                string newMonth = "";
                Array.Resize(ref csvValues, csvValues.Length + 1);
                foreach (AccommodationStatistic statistic in monthlyStatistic)
                {
                     newMonth += statistic.Year.ToString() +
                                      "(" + statistic.ReservationsCount.ToString() +
                                      "," + statistic.ReschedulesCount.ToString() +
                                      "," + statistic.CancelationsCount.ToString() +
                                      "," + statistic.RenovationsCount.ToString() + ")"
                                      +";";
                }

                csvValues[csvValues.Length - 1] = newMonth;
            }

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            AccommodationId = Convert.ToInt32(values[0]);
            JanuaryStatistics = SplitByTackaSemicolon(values[1]);
            FebruaryStatistics = SplitByTackaSemicolon(values[2]);
            MarchStatistics = SplitByTackaSemicolon(values[3]);
            AprilStatistics = SplitByTackaSemicolon(values[4]);
            MayStatistics = SplitByTackaSemicolon(values[5]);
            JunStatistics = SplitByTackaSemicolon(values[6]);
            JulyStatistics = SplitByTackaSemicolon(values[7]);
            AugustStatistics = SplitByTackaSemicolon(values[8]);
            SeptemberStatistics = SplitByTackaSemicolon(values[9]);
            OctoberStatistics = SplitByTackaSemicolon(values[10]);
            NovemberStatistics = SplitByTackaSemicolon(values[11]);
            DecemberStatistics = SplitByTackaSemicolon(values[12]);

        }

        private List<AccommodationStatistic> SplitByTackaSemicolon(string monthStatistic)
        {
            List<AccommodationStatistic> returnValue = new List<AccommodationStatistic>();
            string[] statistics = monthStatistic.Split(';');
            foreach (var statistic in statistics)
            {
                if(statistic == "")
                    break;
                string[] splitedWord = statistic.Split("(");
                string year = splitedWord[0];
                string stats = splitedWord[1].Remove(splitedWord[1].Length - 1);
                string[] statovi = stats.Split(',');

                AccommodationStatistic statistika = new AccommodationStatistic();
                statistika.Year = DateTime.Parse(year);
                statistika.ReservationsCount = Convert.ToInt32(statovi[0]);
                statistika.ReschedulesCount = Convert.ToInt32(statovi[1]);
                statistika.CancelationsCount = Convert.ToInt32(statovi[2]);
                statistika.RenovationsCount = Convert.ToInt32(statovi[3]);

                returnValue.Add(statistika);
            }

            return returnValue;
        }
    }
}
