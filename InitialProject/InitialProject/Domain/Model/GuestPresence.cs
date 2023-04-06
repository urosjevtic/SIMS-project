using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Repository;

namespace InitialProject.Domain.Model
{
    public class GuestPresence
    {
        public int GuestId { get; set; }
        public int CheckPointId { get; set; }
        public bool Present { get; set; }

        public GuestPresence() { }
        public GuestPresence(int idGuest, int idCheckPoint, bool present)
        {
            GuestId = idGuest;
            CheckPointId = idCheckPoint;
            Present = present;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
               GuestId.ToString(),
               CheckPointId.ToString(),
               Present.ToString(),
            };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            GuestId = Convert.ToInt32(values[0]);
            CheckPointId = Convert.ToInt32(values[1]);
            Present = Convert.ToBoolean(values[3]);

        }
    }
}
