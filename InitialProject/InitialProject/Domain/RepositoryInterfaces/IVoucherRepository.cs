using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Domain.Model;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        List<Voucher> GetAll();
        void Save(Voucher voucher);
        int NextId();
        void ChangeUsed(Voucher voucher);
    }
}
