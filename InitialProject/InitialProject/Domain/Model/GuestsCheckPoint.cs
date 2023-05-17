using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Domain.Model
{
    public class GuestsCheckPoint : ISerializable
    {
        public int Id { get; set; } 
        public TourGuest Guest { get; set; }
        public CheckPoint CheckPoint{ get; set; }

        private readonly CheckPointRepository _checkPointrepository;
        private readonly TourGuestRepository _tourGuestsRepository;


        public GuestsCheckPoint() {
            _checkPointrepository = new CheckPointRepository();
            _tourGuestsRepository = new TourGuestRepository();
        }
        public GuestsCheckPoint(int id,TourGuest guest, CheckPoint checkPoint)
        {
            Id = id;
            Guest = guest;
            CheckPoint = checkPoint;

        }
        public string[] ToCSV()
        {
            string[] csvValues = {
               Id.ToString(),
               Guest.Id.ToString(),
               CheckPoint.Id.ToString(),
              
            };

            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest = _tourGuestsRepository.GetById(Convert.ToInt32(values[1]));
            CheckPoint = _checkPointrepository.GetById(Convert.ToInt32(values[2]));
            
        }
    }
}
