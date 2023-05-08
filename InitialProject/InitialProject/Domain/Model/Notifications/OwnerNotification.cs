using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model.Users;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model.Notifications
{
    public class OwnerNotification : ISerializable
    {
        public Owner Owner { get; set; }
        public int ReservationReschedulingCount { get; set; }

        public int RenovationRecommendationCount {get; set; }

        public int ForumNotificationCount { get; set; }

        public int UnradtedGuestsCount { get; set; }

        public bool NewNotification { get; set; }

        public OwnerNotification()
        {
            Owner = new Owner();
        }

        public OwnerNotification(Owner owner, int reservationReschedulingCount, int renovationRecommendationCount, int forumNotificationCount, int unradtedGuestsCount, bool newNotification)
        {
            Owner = owner;
            ReservationReschedulingCount = reservationReschedulingCount;
            RenovationRecommendationCount = renovationRecommendationCount;
            ForumNotificationCount = forumNotificationCount;
            UnradtedGuestsCount = unradtedGuestsCount;
            NewNotification = newNotification;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Owner.Id.ToString(), ReservationReschedulingCount.ToString(), RenovationRecommendationCount.ToString(),
                ForumNotificationCount.ToString(), UnradtedGuestsCount.ToString(), NewNotification.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Owner.Id = Convert.ToInt32(values[0]);
            ReservationReschedulingCount = Convert.ToInt32(values[1]);
            RenovationRecommendationCount = Convert.ToInt32(values[2]);
            ForumNotificationCount = Convert.ToInt32(values[3]);
            UnradtedGuestsCount = Convert.ToInt32(values[4]);
            NewNotification = Convert.ToBoolean(values[5]);

        }
    }
}
