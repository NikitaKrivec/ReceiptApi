using ReceiptApi.Core.Models;
using ReceiptApi.Service;

namespace ReceiptApi.Core.Validator
{
    public class ReceiptValidator : IReceiptValidator
    {
        private IReceiptService _service;
        public ReceiptValidator(IReceiptService service)
        {
            _service = service;
        }

        public bool IsInvalidId(int id)
        {
            if (_service.GetReceiptById(id) == null)
            {
                return true;
            }
            return false;
        }

        public bool IsReceiptExist(Receipt receipt)
        {
            var receipts = _service.GetReceipts();

            foreach (var r in receipts)
            {
                if (r.CreatedOn == receipt.CreatedOn || r.ReceiptItems == receipt.ReceiptItems)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

