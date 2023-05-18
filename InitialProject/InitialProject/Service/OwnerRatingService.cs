using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class OwnerRatingService
    {
        private readonly UnratedOwnerService _unratedOwnerService;
        private readonly RatedOwnerService _ratedOwnerService;

        public OwnerRatingService()
        {
            _unratedOwnerService = new UnratedOwnerService();
            _ratedOwnerService = new RatedOwnerService();
        }

        public void SubmitRating(UnratedOwner unratedOwner, int ownerCorrectness, int cleanlinessRating, string additionalComment, string imageUrl)
        {
            RatedOwner ratedOwner = new RatedOwner
            {
                Reservation = unratedOwner.Reservation,
                OwnerCorrectness= ownerCorrectness,
                CleanlinessRating = cleanlinessRating,
                AdditionalComment = additionalComment,
                ImageUrl = imageUrl

            };

            _ratedOwnerService.Save(ratedOwner);
            //_unratedOwnerService.Remove(unratedOwner);
        }


    }
}