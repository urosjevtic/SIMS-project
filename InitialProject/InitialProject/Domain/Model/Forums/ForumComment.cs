using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using InitialProject.Domain.Model.Users;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model.Forums
{
    public class ForumComment : ISerializable
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public User Author { get; set; }

        public int NumberOfReports { get; set; }

        public List<User> ReportedBy {get; set; }

        public bool HasUserReported { get; set; }

        public ForumComment()
        {
            Author = new User();
            ReportedBy = new List<User>();
        }

        public ForumComment(int id, string comment, User author, int numberOfReports, List<User> reportedBy)
        {
            Id = id;
            Comment = comment;
            Author = author;
            NumberOfReports = numberOfReports;
            ReportedBy = reportedBy;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Comment, Author.Id.ToString(), NumberOfReports.ToString() };

            if (ReportedBy.Count() > 0)
            {
                foreach (var reporter in ReportedBy)
                {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = reporter.Id.ToString();
                }
            }

            Array.Resize(ref csvValues, csvValues.Length + 1);
            csvValues[csvValues.Length - 1] = "[END]";


            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Comment = values[1];
            Author.Id = Convert.ToInt32(values[2]);
            NumberOfReports = Convert.ToInt32(values[3]);


            int i = 4;

            while (values[i] != "[END]")
            {
                int ids = Convert.ToInt32(values[i]);
                User reporter = new User();
                reporter.Id = ids;
                ReportedBy.Add(reporter);
                i++;
            }
        }
    }

}
