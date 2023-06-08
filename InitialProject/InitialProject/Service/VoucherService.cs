using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Domain.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class VoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService()
        {
            _voucherRepository = Injector.Injector.CreateInstance<IVoucherRepository>();
            CheckVoucherStatus(GetAllCreated());
        }

        public bool IsSelectedVoucher(Voucher SelectedVoucher)
        {
            if(SelectedVoucher != null)
            {
                return true;
            }
            return false;
        }
        public List<Voucher> GetAllCreated()
        {
            List<Voucher> vouchers = _voucherRepository.GetAll();

            List<Voucher> result = new();
            foreach(Voucher v in vouchers)
            {
                if(v.Status == VoucherStatus.Created)
                {
                    result.Add(v);
                }
            }
            return result;
        }
        public void CheckVoucherStatus(List<Voucher> vouchers)
        {
            foreach(Voucher voucher in vouchers)
            {
                if (voucher.CreationDate < DateTime.Now.AddYears(-1))
                {
                    voucher.Status = VoucherStatus.Expired;
                }
            }
        }
        public void ChangeToUsed(Voucher voucher)
        {
            _voucherRepository.ChangeUsed(voucher);
        }

        public void Save(Voucher voucher)
        {
            _voucherRepository.Save(voucher);
        }

        public void Update(Voucher voucher)
        {
            _voucherRepository.Update(voucher);
        }
    }
}
