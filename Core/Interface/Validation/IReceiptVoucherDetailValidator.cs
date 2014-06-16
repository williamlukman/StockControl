using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IReceiptVoucherDetailValidator
    {
        ReceiptVoucherDetail VHasReceiptVoucher(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService);
        ReceiptVoucherDetail VHasReceivable(ReceiptVoucherDetail receiptVoucherDetail, IReceivableService _receivableService);
        ReceiptVoucherDetail VReceivableContactIsTheSame(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceivableService _receivableService);
        ReceiptVoucherDetail VAmountLessThanCashBank(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService);
        ReceiptVoucherDetail VCorrectInstantClearance(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService);
        ReceiptVoucherDetail VUniqueCashBankId(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService);
        ReceiptVoucherDetail VNonNegativeOrZeroAmount(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail VIsConfirmed(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail VUpdateContactOrReceivable(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherDetailService _receiptVoucherDetailService);

        ReceiptVoucherDetail VRemainingAmount(ReceiptVoucherDetail receiptVoucherDetail, IReceivableService _receivableService);
        ReceiptVoucherDetail VIsClearedForNonInstantClearance(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService);
        ReceiptVoucherDetail VIsInstantClearance(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService);
        ReceiptVoucherDetail VIsNonInstantClearance(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService);
        ReceiptVoucherDetail VClearanceDate(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService);

        ReceiptVoucherDetail VCreateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucherDetail VUpdateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        ReceiptVoucherDetail VDeleteObject(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail VConfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        ReceiptVoucherDetail VUnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        ReceiptVoucherDetail VClearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        ReceiptVoucherDetail VUnclearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        ReceiptVoucherDetail VClearConfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        ReceiptVoucherDetail VUnclearUnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        bool ValidCreateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        bool ValidUpdateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService);
        bool ValidDeleteObject(ReceiptVoucherDetail receiptVoucherDetail);
        bool ValidConfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        bool ValidUnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        bool ValidClearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        bool ValidUnclearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        bool ValidClearConfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        bool ValidUnclearUnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService);
        bool isValid(ReceiptVoucherDetail receiptVoucherDetail);
        string PrintError(ReceiptVoucherDetail receiptVoucherDetail);
    }
}
