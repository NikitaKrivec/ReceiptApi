using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceiptApi.Core.Models;
using ReceiptApi.Core.Validator;
using ReceiptApi.Service;

namespace ReceiptApi.Controllers
{
    [Route("api/receipt")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _receiptService;
        private readonly IReceiptValidator _receiptValidator;
        private readonly IItemValidator _itemValidator;

        public ReceiptController(IReceiptService receiptService, IReceiptValidator receiptValidator)
        {
            _receiptService = receiptService;
            _receiptValidator = receiptValidator;
        }

        [Route("receipts")]
        [HttpGet]
        public IActionResult GetReceipts()
        {
            return Ok(_receiptService.GetReceipts());
        }

        [Route("receipt/{id}")]
        [HttpGet]
        public IActionResult GetReceiptById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_receiptValidator.IsInvalidId(id))
            {
                return NoContent();
            }
            return Ok(_receiptService.GetReceiptById(id));
        }

        [Route("receipt/add")]
        [HttpPost]
        public IActionResult AddReceipt(Receipt receipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_receiptValidator.IsReceiptExist(receipt))
            {
                return Conflict();
            }
            return Created("", _receiptService.AddReceipt(receipt));
        }

        [Route("receipt/delete/{id}")]
        [HttpDelete]
        public IActionResult DeleteReceipt(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_receiptValidator.IsInvalidId(id))
            {
                return BadRequest("This Id does not exist.");
            }
            if (!_receiptService.ReceiptIdExists(id))
            {
                return NotFound();
            }
            _receiptService.DeleteReceiptById(id);
            return Ok();
        }

        [Route("receipts/clear/all")]
        [HttpDelete]
        public IActionResult DeleteReceipts()
        {
            return Ok(_receiptService.DeleteAllReceipts());
        }

        [HttpGet]
        [Route("receipts/creation_date")]
        public IActionResult GetReceiptsByDate(DateTime startT, DateTime endT)
        {
            if (!_receiptService.IsTimeValid(startT) || !_receiptService.IsTimeValid(endT))
            {
                return BadRequest();
            }

            var receipts = _receiptService.GetReceiptsByCreationDate(startT, endT);
            return Ok(receipts);
        }

        [HttpGet]
        [Route("get-receipts-by-product-name")]
        public IActionResult GetReceiptsByProductName(string name)
        {
            if (!_receiptService.ProductNameExists(name))
            {
                return NotFound("This product does not exist.");
            }

            return Ok(_receiptService.GetReceiptsByItemProductName(name));
        }
    }
}