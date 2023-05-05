using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model.AccommodationRenovation
{
    public class Renovation : ISerializable
    {
        public int Id {get; set; }
        public Accommodation Accommodation {get; set; }
        public DateTime StartDate { get; set; }
        public int LengthInDays { get; set; }

        public string Description { get; set; }


        public Renovation()
        {
            Accommodation = new Accommodation();
        }

        public Renovation(int id, Accommodation accommodation, DateTime startDate, int lengthInDays, string description)
        {
            Id = id;
            Accommodation = accommodation;
            StartDate = startDate;
            LengthInDays = lengthInDays;
            Description = description;
        }


        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Accommodation.Id.ToString(), StartDate.ToString("dd'/'MM'/'yyyy"), LengthInDays.ToString(), Description };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation.Id = Convert.ToInt32(values[1]);
            StartDate = DateTime.ParseExact(values[2], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            LengthInDays = Convert.ToInt32(values[3]);
            Description = values[4];
        }
    }
}
