using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ForumComment()
        {
            Author = new User();
        }

        public ForumComment(int id, string comment, User author, int numberOfReports)
        {
            Id = id;
            Comment = comment;
            Author = author;
            NumberOfReports = numberOfReports;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Comment, Author.Id.ToString(), NumberOfReports.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Comment = values[1];
            Author.Id = Convert.ToInt32(values[2]);
            NumberOfReports = Convert.ToInt32(values[3]);

        }
    }

}
