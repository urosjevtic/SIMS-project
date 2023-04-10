using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace InitialProject.Domain.Model
{
    public enum VoucherStatus{ Created, Expired, Used };
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public DateTime CreationDate { get; set; }
        public VoucherStatus Status { get; set; }
        public String Text { get; set; }    
        public Voucher()
        {

        }
        public Voucher(int id, int idUser, DateTime date,String text)
        {
            Id = id;
            IdUser = idUser;
            CreationDate = date;
            Text = text;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
               Id.ToString(),
               IdUser.ToString(),
               CreationDate.ToString(),
               Status.ToString(),
               Text
            };

            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IdUser = Convert.ToInt32(values[1]);
            CreationDate = DateTime.Parse(values[2]);
            Status = (VoucherStatus)Enum.Parse(typeof(VoucherStatus),values[3]);
            Text = values[4];
        }

    }
}
