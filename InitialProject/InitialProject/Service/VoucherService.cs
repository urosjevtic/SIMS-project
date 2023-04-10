﻿using System;
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
    }
}
