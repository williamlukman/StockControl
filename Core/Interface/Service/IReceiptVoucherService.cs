using Core.DomainModel;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IReceiptVoucherService
    {
        IReceiptVoucherValidator GetValidator();
        IList<ReceiptVoucher> GetAll();
        IList<ReceiptVoucher> GetObjectsByCashBankId(int cashBankId);
        ReceiptVoucher GetObjectById(int Id);
        IList<ReceiptVoucher> GetObjectsByContactId(int contactId);
        ReceiptVoucher CreateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService);
        ReceiptVoucher CreateObject(int cashBankId, int contactId, DateTime paymentDate, decimal totalAmount,
                                    IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService,
                                    IContactService _contactService, ICashBankService _cashBankService);
        ReceiptVoucher UpdateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService);
        ReceiptVoucher SoftDeleteObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService);
        bool DeleteObject(int Id);
        ReceiptVoucher ConfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService,
                                     ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucher UnconfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService,
                                     ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucher ClearObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService,
                                     ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucher UnclearObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService,
                                     ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
    }
}