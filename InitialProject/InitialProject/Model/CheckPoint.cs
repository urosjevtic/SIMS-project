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
        public CheckPoint FindById(int id)
        {
            CheckPoint checkPoint = new CheckPoint();
            List<CheckPoint> checkPoints = new List<CheckPoint>();
            checkPoints = _checkPointRepository.GetAll();
            foreach(CheckPoint point in checkPoints)
            {
                if(checkPoint.Id == id)
                {
                    return checkPoint;
                }
            }
            return checkPoint;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[2];
            SerialNumber = Convert.ToInt32(values[3]);  
            IsChecked = Convert.ToBoolean(values[4]);
            
        }
        public string MakeString()
        {
            return Name;
        }

    }
}
