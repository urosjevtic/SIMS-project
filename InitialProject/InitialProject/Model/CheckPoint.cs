using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using InitialProject.Repository;

namespace InitialProject.Model
{
    public class CheckPoint : ISerializable
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int SerialNumber { get; set; }   
        public bool IsChecked { get; set; } 
        public CheckPoint() { }
        public CheckPoint(int id, string name,int serialNumber, bool isChecked)
        {
            Id = id;    
            Name = name;
            SerialNumber = serialNumber;
            IsChecked = isChecked;
        }

        private readonly CheckPointRepository _checkPointRepository;
        public string[] ToCSV()
        {
            string[] csvValues = {
               Id.ToString(),
               Name,
               SerialNumber.ToString(),
               IsChecked.ToString(),
            };
            return csvValues;
        }
       

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            SerialNumber = Convert.ToInt32(values[2]);  
            IsChecked = Convert.ToBoolean(values[3]);
            
        }
        public string MakeString()
        {
            return Name;
        }

    }
}
