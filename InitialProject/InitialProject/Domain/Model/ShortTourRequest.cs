using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public enum RequestStatus {Active, Invalid, Accepted}
    public class ShortTourRequest : ISerializable
    {
        public int IdRequest { get; set; }
        public int IdUser { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Language { get; set; }
        public int NumberOfPeople { get; set; }
        public string Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public RequestStatus Status { get; set; }
        
        public ShortTourRequest()
        {

        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                IdRequest.ToString(),
                IdUser.ToString(),
                Country,
                City,
                Language,
                NumberOfPeople.ToString(),
                Description,
                From.ToString(),
                To.ToString(),
                Status.ToString()
            };

            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            IdRequest = Convert.ToInt32(values[0]);
            IdUser = Convert.ToInt32(values[1]);
            Country = values[2];
            City = values[3];
            Language = values[4];
            NumberOfPeople = Convert.ToInt32(values[5]);
            Description = values[6];
            From = DateTime.Parse(values[7]);
            To = DateTime.Parse(values[8]);
            Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[9]);
        }
    }
}
