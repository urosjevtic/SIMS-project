using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace InitialProject.Domain.Model.Users
{
    public class Owner : User, ISerializable
    {
        public bool SuperOwner { get; set; }

        public Owner() { }

        public Owner(string username, string password, bool superOwner) : base(username, password, UserRole.Owner)
        {
            SuperOwner = superOwner;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString(), SuperOwner.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            base.FromCSV(values);
            SuperOwner = Convert.ToBoolean(values[4]);
        }
    }
}
