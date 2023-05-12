using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using InitialProject.Domain.Model;
using InitialProject.Domain.Model.Reservations;

namespace InitialProject.Service.RatingServices
{
    public class SuperOwnerService
    {
        private readonly RatedOwnerService _ratedOwnerService;

        public SuperOwnerService()
        {
            _ratedOwnerService = new RatedOwnerService();
        }

        public bool IsSuperOwner(int ownerId)
        {
            if (GetAvrageRating(ownerId) > 2 && ReviewsCount(ownerId) > 2)
                return true;
            return false;
        }

        private int ReviewsCount(int ownerId)
        {
            List<RatedOwner> ratedOwners = _ratedOwnerService.GetByOwnersId(ownerId);
            return ratedOwners.Count();
        }

        public double GetAvrageRating(int ownerId)
        {
            List<RatedOwner> ratedOwners = _ratedOwnerService.GetByOwnersId(ownerId);
            if (ratedOwners.Count == 0)
            {
                return 0;
            }

            double totalRating = 0;
            foreach (var ratedOwner in ratedOwners)
            {
                totalRating += ratedOwner.CleanlinessRating + ratedOwner.OwnerCorrectness;
            }

            return totalRating / (ratedOwners.Count * 2);

        }
    }
}
