using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IReceiptVoucherValidator
    {
        ReceiptVoucher VHasContact(ReceiptVoucher receiptVoucher, IContactService _contactService);
        ReceiptVoucher VHasCashBank(ReceiptVoucher receiptVoucher, ICashBankService _cashBankService);
        ReceiptVoucher VHasReceiptDate(ReceiptVoucher receiptVoucher);
        ReceiptVoucher VHasReceiptVoucherDetails(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService);
        ReceiptVoucher VRemainingAmountDetails(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService);
        ReceiptVoucher VUnconfirmableDetails(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        ReceiptVoucher VUpdateContact(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService);
        ReceiptVoucher VIsConfirmed(ReceiptVoucher receiptVoucher);
        ReceiptVoucher VCreateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService);
        ReceiptVoucher VUpdateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService);
        ReceiptVoucher VDeleteObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService);
        ReceiptVoucher VConfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucher VUnconfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        bool ValidCreateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService);
        bool ValidUpdateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService);
        bool ValidDeleteObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService);
        bool ValidConfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        bool ValidUnconfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        bool isValid(ReceiptVoucher receiptVoucher);
        string PrintError(ReceiptVoucher receiptVoucher);
    }
}
