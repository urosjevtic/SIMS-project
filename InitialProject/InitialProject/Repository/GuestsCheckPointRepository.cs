using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.Model;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    public class GuestsCheckPointRepository : IGuestsCheckPointRepository
    {
        private const string FilePath = "../../../Resources/Data/guestsCheckPoint.csv";
        private readonly Serializer<GuestsCheckPoint> _serializer;

        private List<GuestsCheckPoint> _guestsCheckPoint;

        public GuestsCheckPointRepository(){

            _serializer = new Serializer<GuestsCheckPoint>();
            _guestsCheckPoint = new List<GuestsCheckPoint>();
        }
        public List<GuestsCheckPoint> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public GuestsCheckPoint GetById(int id)
        {
            GuestsCheckPoint notification = new GuestsCheckPoint();
            List<GuestsCheckPoint> notifications = new List<GuestsCheckPoint>();
            notifications = GetAll();
            foreach (GuestsCheckPoint not in notifications)
            {
                if (not.Id == id)
                {
                    notification = not;
                    break;
                }
            }
            return notification;
        }

        public void Save(GuestsCheckPoint notification)
        {
            notification.Id = NextId();
            _guestsCheckPoint = _serializer.FromCSV(FilePath);
            _guestsCheckPoint.Add(notification);
            _serializer.ToCSV(FilePath, _guestsCheckPoint);
        }

        public int NextId()
        {
            _guestsCheckPoint = _serializer.FromCSV(FilePath);
            if (_guestsCheckPoint.Count < 1)
            {
                return 1;
            }
            return _guestsCheckPoint.Max(c => c.Id) + 1;
        }
    }
}
