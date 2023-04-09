using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int TourId { get; set; }
        public int CheckPointId { get; set; }
        public bool IsGoing { get; set; }
        public bool IsChecked { get; set; }

        public Notification()
        {

        }
        public Notification(Tour tour, User user)
        {
            GuestId = user.Id;
            TourId = tour.Id;
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
               Id.ToString(),
               TourId.ToString(),
               CheckPointId.ToString(),
               GuestId.ToString(),
               IsGoing.ToString(),
               IsChecked.ToString(),
            };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            CheckPointId = Convert.ToInt32(values[2]);
            GuestId = Convert.ToInt32(values[3]);
            IsGoing = Convert.ToBoolean(values[4]);
            IsChecked = Convert.ToBoolean(values[5]);
        }
    }
}
