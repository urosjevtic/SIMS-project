using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public class Image : ISerializable
    {
        public int Id;
        public List<string> Url;
        public int EntityLd;

        public Image()
        {
            Url = new List<string>();
        }
        public Image(int id, List<string> url, int entityld)
        {
            Id = id;
            Url = url;
            EntityLd = entityld;
        }



        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                EntityLd.ToString(),
            };

            if (Url.Count() > 0)
            {
                foreach (string url in Url)
                {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = url;
                }
            }

            Array.Resize(ref csvValues, csvValues.Length + 1);
            csvValues[csvValues.Length - 1] = "[END]";

            return csvValues;
        }

        public void FromCSV(string[] values)
        {

            Id = int.Parse(values[0]);
            EntityLd = int.Parse(values[1]);
            int i = 2;
            Url.Clear();

            while (values[i] != "[END]")
            {
                Url.Add(values[i]);
                i++;
            }

        }

    }
}
