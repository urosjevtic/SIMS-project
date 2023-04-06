using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Serializer;

namespace InitialProject.Repository
{
    public class VoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/vouchers.csv";

        private readonly Serializer<Voucher> _serializer;

        private List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);
        }
        public List<Voucher> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public List<Voucher> GetAllCreated()
        {
            List<Voucher> created = new List<Voucher>();
            foreach(Voucher voucher in _vouchers)
            {
                if(voucher.Status == VoucherStatus.Created)
                {
                    created.Add(voucher);
                }
            }
            return created;
        }
        public int NextId()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(c => c.Id) + 1;
        }
        public void Save(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers = _serializer.FromCSV(FilePath);
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
        }
        public void ChangeToUsed(Voucher voucher)
        {
            Voucher found = _vouchers.Find(c => c.Id == voucher.Id);
            _vouchers.Remove(found);
            found.Status = VoucherStatus.Used;
            _vouchers.Add(found);
            _serializer.ToCSV(FilePath, _vouchers);
        }
    }
}
