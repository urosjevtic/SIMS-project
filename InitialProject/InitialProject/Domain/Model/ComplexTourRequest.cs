using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public class ComplexTourRequest : ISerializable
    {
        public int IdRequest { get; set; }
        public int IdUser { get; set; }
        public RequestStatus Status { get; set; }
        public List<ShortTourRequest> Requests { get; set; }
        public ComplexTourRequest()
        {
            Requests = new List<ShortTourRequest>();
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                IdRequest.ToString(),
                IdUser.ToString(),
                Status.ToString(),
            };
            foreach(ShortTourRequest request in Requests)
            {
                Array.Resize(ref csvValues, csvValues.Length + 9);
                csvValues[csvValues.Length - 9] = request.IdRequest.ToString();
                csvValues[csvValues.Length - 8] = request.Country;
                csvValues[csvValues.Length - 7] = request.City;
                csvValues[csvValues.Length - 6] = request.Language;
                csvValues[csvValues.Length - 5] = request.NumberOfPeople.ToString();
                csvValues[csvValues.Length - 4] = request.Description;
                csvValues[csvValues.Length - 3] = request.From.ToString();
                csvValues[csvValues.Length - 2] = request.To.ToString();
                csvValues[csvValues.Length - 1] = request.Status.ToString();
            }
            Array.Resize(ref csvValues, csvValues.Length + 1);
            csvValues[csvValues.Length - 1] = "[END]";

            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            IdRequest = Convert.ToInt32(values[0]);
            IdUser = Convert.ToInt32(values[1]);
            Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[2]);
            int i = 3;
            while (values[i] != "[END]")
            {
                ShortTourRequest shortRequest = new();
                shortRequest.IdRequest = Convert.ToInt32(values[i]);
                i++;
                shortRequest.Country = values[i];
                i++;
                shortRequest.City = values[i];
                i++;
                shortRequest.Language = values[i];
                i++;
                shortRequest.NumberOfPeople = Convert.ToInt32(values[i]);
                i++;
                shortRequest.Description = values[i];
                i++;
                shortRequest.From = Convert.ToDateTime(values[i]);
                i++;
                shortRequest.To = Convert.ToDateTime(values[i]);
                i++;
                shortRequest.Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[i]);
                i++;
                
                Requests.Add(shortRequest);
            }
        }
    }
}
