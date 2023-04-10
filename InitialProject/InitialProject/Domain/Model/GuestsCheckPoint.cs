using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public class GuestsCheckPoint : ISerializable
    {
        public int Id { get; set; } 
        public int GuestsId { get; set; }
        public int CheckPointId { get; set; }


        public GuestsCheckPoint() { }
        public GuestsCheckPoint(int id,int guestId, int checkPointId)
        {
            Id = id;
            GuestsId = guestId;
            CheckPointId = checkPointId;

        }
        public string[] ToCSV()
        {
            string[] csvValues = {
               Id.ToString(),
               GuestsId.ToString(),
               CheckPointId.ToString(),
              
            };

            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestsId = Convert.ToInt32(values[1]);
            CheckPointId = Convert.ToInt32(values[2]);
            
        }
    }
}
