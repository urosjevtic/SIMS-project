using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class VoucherService
    {
        private readonly VoucherRepository _voucherRepository;

        public VoucherService()
        {
            _voucherRepository = new VoucherRepository();
        }

        public bool IsSelectedVoucher(Voucher SelectedVoucher)
        {
            if(SelectedVoucher != null)
            {
                return true;
            }
            return false;
        }
    }
}
