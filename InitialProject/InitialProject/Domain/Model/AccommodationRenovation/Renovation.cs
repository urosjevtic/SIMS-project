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
        public DateTime EndDate { get; set; }
        public int LengthInDays { get; set; }

        public string Description { get; set; }

        public bool IsFinished
        {
            get { return !(EndDate >= DateTime.Now); }
        }

        public bool IsAbleToCancel
        {
            get{ return IsFinished || (StartDate - DateTime.Now).TotalDays < 5;}
        }

        public Renovation()
        {
            Accommodation = new Accommodation();
        }

        public Renovation(Accommodation accommodation, DateTime startDate, DateTime endDate, int lengthInDays, string description)
        {
            Accommodation = accommodation;
            StartDate = startDate;
            EndDate = endDate;
            LengthInDays = lengthInDays;
            Description = description;
        }


        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Accommodation.Id.ToString(), StartDate.ToString("dd'/'MM'/'yyyy"), EndDate.ToString("dd'/'MM'/'yyyy"), LengthInDays.ToString(), Description };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Accommodation.Id = Convert.ToInt32(values[1]);
            StartDate = DateTime.ParseExact(values[2], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            EndDate = DateTime.ParseExact(values[3], "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
            LengthInDays = Convert.ToInt32(values[4]);
            Description = values[5];
        }

    }
}
