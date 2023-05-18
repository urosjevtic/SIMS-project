using InitialProject.Serializer;
using System;

namespace InitialProject.Domain.Model
{
    public class Comment : ISerializable
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public int IdTour { get; set; }
        public bool IsReported { get; set; }    

        public Comment() { }

        public Comment(DateTime creationTime, string text, User user, int id, bool isReported)
        {
            CreationTime = creationTime;
            Text = text;
            User = user;
            IdTour = id;
            IsReported = isReported;    
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), CreationTime.ToString(), Text, User.Id.ToString(), IdTour.ToString() ,IsReported.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            CreationTime = Convert.ToDateTime(values[1]);
            Text = values[2];
            User = new User() { Id = Convert.ToInt32(values[3]) };
            IdTour = Convert.ToInt32(values[4]);
            IsReported = Boolean.Parse(values[5]);  
        }
    }
}
