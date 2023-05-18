using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class GuestRatingService
    {
        private readonly UnratedGuestService _unratedGuestService;
        private readonly RatedGuestService _ratedGuestService;

        public GuestRatingService()
        {
            _unratedGuestService = new UnratedGuestService();
            _ratedGuestService = new RatedGuestService();
        }

        public void SubmitRating(UnratedGuest unratedGuest, int ruleFollowingRating, int cleanlinessRating, string additionalComment)
        {
            RatedGuest ratedGuest = new RatedGuest
            {
                Reservation = unratedGuest.Reservation,
                RuleFollowingRating = ruleFollowingRating,
                CleanlinessRating = cleanlinessRating,
                AdditionalComment = additionalComment,

            };

            _ratedGuestService.Save(ratedGuest);
            _unratedGuestService.Remove(unratedGuest);
        }


    }
}
