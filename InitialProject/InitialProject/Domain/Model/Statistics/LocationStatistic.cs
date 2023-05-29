using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model.Statistics
{
    public class LocationStatistic
    {
        public Location Location { get; set; }
        public int NumberOfReservations { get; set; }

        public LocationStatistic()
        {

        }

        public LocationStatistic(Location location, int numberOfReservations)
        {
            Location = location;
            NumberOfReservations = numberOfReservations;
        }
    }
}
