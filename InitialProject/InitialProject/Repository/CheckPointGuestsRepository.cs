using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using InitialProject.Model;

namespace InitialProject.Repository
{
    public class CheckPointGuestsRepository
    {
        private const string FilePath = "../../../Resources/Data/checkPointGuests.csv";

        private readonly Serializer<CheckPointGuests> _serializer;

        private List<CheckPointGuests> _checkPointGuests;
        //private List<CheckPoint> _checkpoints;

        public CheckPointGuestsRepository()
        {
            _serializer = new Serializer<CheckPointGuests>();

            _checkPointGuests = _serializer.FromCSV(FilePath);

        }

        public int NextId()
        {
            _checkPointGuests = _serializer.FromCSV(FilePath);
            if (_checkPointGuests.Count < 1)
            {
                return 1;
            }
            return _checkPointGuests.Max(c => c.Id) + 1;
        }
        public void Save(CheckPointGuests checkPointGuests)
        {
            checkPointGuests.Id = NextId();
            _checkPointGuests = _serializer.FromCSV(FilePath);
            _checkPointGuests.Add(checkPointGuests);
            _serializer.ToCSV(FilePath, _checkPointGuests);
        }

        public List<CheckPointGuests> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
    }
}
