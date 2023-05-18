using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model.Users
{
    public class OwnerSettings : ISerializable
    {
        public int OwnerId { get; set; }
        public string Language { get; set; }
        public string Theme { get; set; }

        public OwnerSettings() { }

        public OwnerSettings(int ownerId, string language, string theme)
        {
            OwnerId = ownerId;
            Language = language;
            Theme = theme;
        }


        public string[] ToCSV()
        {
            string[] csvValues = { OwnerId.ToString(), Language, Theme };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            OwnerId = Convert.ToInt32(values[0]);
            Language = values[1];
            Theme = values[2];
        }
    }
}
