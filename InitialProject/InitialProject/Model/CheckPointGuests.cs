using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Model
{
    public class CheckPointGuests : ISerializable
    {
        public int Id { get; set; } 
        public int CheckPointId { get; set; } 
        public List<int> GuestId { get; set; }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
               CheckPointId.ToString()

            };

            if (GuestId.Count() > 0)
            {
                foreach (int id in GuestId)
                {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = id.ToString();
                }
            }

            Array.Resize(ref csvValues, csvValues.Length + 1);
            csvValues[csvValues.Length - 1] = "[END]";

           
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            CheckPointId = Convert.ToInt32(values[1]);
            int i = 2;

            GuestId.Clear();

            while (values[i] != "[END]")
            {
                int ids = Convert.ToInt32(values[i]);
                GuestId.Add(ids);
                i++;
            }
        }

    }
}
