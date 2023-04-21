using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public class Notes : ISerializable
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }
        public List<string> OwnerNotes { get; set; }

        public Notes()
        {
            OwnerNotes = new List<string>();
        }

        public Notes(int ownerId, List<string> ownerNotes)
        {
            OwnerId = ownerId;
            OwnerNotes = ownerNotes;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), OwnerId.ToString() };

            if (OwnerNotes.Count > 0)
            {
                foreach (string note in OwnerNotes)
                {
                    Array.Resize(ref csvValues, csvValues.Length + 1);
                    csvValues[csvValues.Length - 1] = note;
                }
            }

            Array.Resize(ref csvValues, csvValues.Length + 1);
            csvValues[csvValues.Length - 1] = "[END]";

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            OwnerId = Convert.ToInt32(values[1]);

            int i = 2;
            while (values[i] != "[END]")
            {
                OwnerNotes.Add(values[i]);
                i++;
            }
        }
    }
}
