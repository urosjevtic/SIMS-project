using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model.Forums
{
    public class Forum : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public User Author { get; set; }

        public Location Location { get; set; }
        public DateTime CreationDate { get; set; }

        public List<ForumComment> Comments { get; set; }

        public int CommentNumber
        {
            get { return Comments.Count; }
        }
        private bool _isSpecial;

        public bool IsSpecial
        {
            get { return _isSpecial; }
            set
            {
                if (_isSpecial != value)
                {
                    _isSpecial = value;
                    OnPropertyChanged(nameof(IsSpecial));
                }
            }
        }

        public bool IsOpen { get; set; }


        public Forum()
        {
            Author = new User();
            Location = new Location();
            Comments = new List<ForumComment>();
        }

        public Forum(int id, User author, Location location, DateTime creationDate, List<ForumComment> comments, bool isOpen)
        {
            Id = id;
            Author = author;
            Location = location;
            CreationDate = creationDate;
            Comments = comments;
            IsOpen = isOpen;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Author.Id.ToString(), Location.Id.ToString(), CreationDate.ToString(), IsOpen.ToString() };

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
            Author.Id = Convert.ToInt32(values[1]);
            Location.Id = Convert.ToInt32(values[2]);
            CreationDate = Convert.ToDateTime(values[3]);
            IsOpen = Convert.ToBoolean(values[4]);

            int i = 5;

            while (values[i] != "[END]")
            {
                int ids = Convert.ToInt32(values[i]);
                Comments.Add(new ForumComment(ids, "", new User(), 0, new List<User>()));
                i++;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
