using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model.Users
{
    public class Guest :User, ISerializable
    {
        public bool SuperGuest { get; set; }
        public int Bonus { get; set; }
        public Guest() { }
        public Guest(string username, string password, bool superGuest, int bonus)
        {
            SuperGuest = superGuest;
            Bonus = bonus;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString(), SuperGuest.ToString(), Bonus.ToString() };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            base.FromCSV(values);
            SuperGuest = Convert.ToBoolean(values[4]);
            Bonus = Convert.ToInt32(values[5]);
        }
    }
}
