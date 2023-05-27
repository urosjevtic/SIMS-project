using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model.Forums
{
    public class Forum : ISerializable
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string AuthorsComment { get; set; }
        public User Author { get; set; }

        public Location Location { get; set; }

        public List<ForumComment> Comments { get; set; }


        public Forum()
        {
            Author = new User();
            Location = new Location();
            Comments = new List<ForumComment>();
        }

        public Forum(int id, string topic, string authorsComment, User author, Location location, List<ForumComment> comments)
        {
            Id = id;
            Topic = topic;
            AuthorsComment = authorsComment;
            Author = author;
            Location = location;
            Comments = comments;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Topic, AuthorsComment, Author.Id.ToString(), Location.Id.ToString() };

            if (Comments.Count() > 0)
            {
                foreach (var comment in Comments)
                {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = comment.Id.ToString();
                }
            }

            Array.Resize(ref csvValues, csvValues.Length + 1);
            csvValues[csvValues.Length - 1] = "[END]";


            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Topic = values[1];
            AuthorsComment = values[2];
            Author.Id = Convert.ToInt32(values[3]);
            Location.Id = Convert.ToInt32(values[4]);

            int i = 5;

            while (values[i] != "[END]")
            {
                int ids = Convert.ToInt32(values[i]);
                Comments.Add(new ForumComment(ids, "", new User(), 0));
                i++;
            }
        }
    }
}
