using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Forums;
using InitialProject.Domain.RepositoryInterfaces.IForumsRepo;
using InitialProject.Serializer;

namespace InitialProject.Repository.ForumsRepo
{
    public class ForumRepository : IForumRepository
    {

        private const string FilePath = "../../../Resources/Data/forums.csv";

        private readonly Serializer<Forum> _serializer;

        private List<Forum> _forums;

        public ForumRepository()
        {
            _serializer = new Serializer<Forum>();
            _forums = _serializer.FromCSV(FilePath);
        }


        public List<Forum> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Forum GetByLocationId(int locationId)
        {
            _forums = _serializer.FromCSV(FilePath);
            return _forums.FirstOrDefault(f => f.Location.Id == locationId);
        }

        public void Update(Forum forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            foreach (var f in _forums)
            {
                if (f.Id == forum.Id)
                {
                    _forums.Remove(f);
                    _forums.Add(forum);
                    break;
                }
            }

            _serializer.ToCSV(FilePath, _forums);
        }

        public void Delete(Forum forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            _forums.Remove(forum);
            _serializer.ToCSV(FilePath, _forums);
        }
    }
}
