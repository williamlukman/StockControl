using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IReceiptVoucherDetailService
    {
        //IReceiptVoucherDetailValidator GetValidator();
        IList<ReceiptVoucherDetail> GetObjectsByReceiptVoucherId(int receiptVoucherId);
        IList<ReceiptVoucherDetail> GetObjectsByReceivableId(int receivableId);
        ReceiptVoucherDetail GetObjectById(int Id);
        ReceiptVoucherDetail CreateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucherDetail CreateObject(int receiptVoucherId, int receivableId, decimal amount, string description,
                                            IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService,
                                            IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucherDetail CreateObject(int receiptVoucherId, int receivableId, decimal amount, string description, bool isInstantClearance,
                                            IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService,
                                            IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucherDetail UpdateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucherDetail SoftDeleteObject(ReceiptVoucherDetail receiptVoucherDetail);
        bool DeleteObject(int Id);
        ReceiptVoucherDetail ConfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucherDetail UnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucherDetail ClearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucherDetail UnclearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);

    }
}