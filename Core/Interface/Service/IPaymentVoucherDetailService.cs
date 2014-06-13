using Core.DomainModel;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPaymentVoucherDetailService
    {
        IPaymentVoucherDetailValidator GetValidator();
        IList<PaymentVoucherDetail> GetObjectsByPaymentVoucherId(int paymentVoucherId);
        IList<PaymentVoucherDetail> GetObjectsByPayableId(int payableId);
        PaymentVoucherDetail GetObjectById(int Id);
        PaymentVoucherDetail CreateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        PaymentVoucherDetail CreateObject(int paymentVoucherId, int payableId, decimal amount, string description,
                                            IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService,
                                            IPayableService _payableService, IContactService _contactService);
        PaymentVoucherDetail CreateObject(int paymentVoucherId, int payableId, decimal amount, string description, bool isInstantClearance,
                                            IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService,
                                            IPayableService _payableService, IContactService _contactService);
        PaymentVoucherDetail UpdateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        PaymentVoucherDetail SoftDeleteObject(PaymentVoucherDetail paymentVoucherDetail);
        bool DeleteObject(int Id);
        PaymentVoucherDetail ConfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        PaymentVoucherDetail UnconfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        PaymentVoucherDetail ClearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        PaymentVoucherDetail UnclearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
    }
}