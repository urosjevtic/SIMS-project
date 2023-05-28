using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Forums;
using InitialProject.Domain.Model.Notifications;
using InitialProject.Domain.Model.Users;
using InitialProject.Domain.RepositoryInterfaces.IForumsRepo;
using InitialProject.Serializer;

namespace InitialProject.Repository.ForumsRepo
{
    public class ForumCommentRepository : IForumCommentRepository
    {

        private const string FilePath = "../../../Resources/Data/forumComments.csv";

        private readonly Serializer<ForumComment> _serializer;

        private List<ForumComment> _comments;

        public ForumCommentRepository()
        {
            _serializer = new Serializer<ForumComment>();
            _comments = _serializer.FromCSV(FilePath);
        }

        public List<ForumComment> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public ForumComment GetById(int id)
        {
            _comments = _serializer.FromCSV(FilePath);
            return _comments.FirstOrDefault(c => c.Id == id);
        }

        public void Update(ForumComment forumComment)
        {
            _comments = _serializer.FromCSV(FilePath);
            foreach (var comment in _comments)
            {
                if (comment.Id == forumComment.Id)
                {
                    _comments.Remove(comment);
                    _comments.Add(forumComment);
                    break;
                }
            }

            _serializer.ToCSV(FilePath, _comments);
        }

        public void Delete(ForumComment forumComment)
        {
            _comments = _serializer.FromCSV(FilePath);
            _comments.Remove(forumComment);
            _serializer.ToCSV(FilePath, _comments);
        }

        private int NextId()
        {
            _comments = _serializer.FromCSV(FilePath);
            if (_comments.Count < 1)
            {
                return 1;
            }
            return _comments.Max(c => c.Id) + 1;
        }
        public ForumComment Save(ForumComment comment)
        {
            comment.Id = NextId();
            _comments = _serializer.FromCSV(FilePath);
            _comments.Add(comment);
            _serializer.ToCSV(FilePath, _comments);

            return comment;
        }
    }
}
