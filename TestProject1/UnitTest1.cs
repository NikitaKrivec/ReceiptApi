using Microsoft.AspNetCore.Mvc;
using ReceiptApi.Controllers;
using ReceiptApi.Core.Models;
using ReceiptApi.Core.Validator;
using ReceiptApi.Data;
using ReceiptApi.Service;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private ReceiptService _receiptService;
        private Receipt _receipt;
        private Receipt _receipt1;
        private DateTime _CreatedOn;
        private DateTime _CreatedOn1;
        private DataContext _context;
        private ReceiptController _controller;
        private IItemValidator _itemValidator;
        private IReceiptValidator _receiptValidator;
        
        [TestInitialize]
        public void Setup()
        {
            _receiptService = new ReceiptService(_context);
            _itemValidator = new ItemValidator();
            _receiptValidator = new ReceiptValidator(_receiptService);


            _controller = new ReceiptController(_receiptService, _receiptValidator, _itemValidator);
            _receipt = new Receipt();
            _CreatedOn = new DateTime(2022, 6, 10, 12, 00, 00);
            var itemsList = new List<Item>()
            {
                new Item() { Id = 1, ProductName = "Taxi"},
                new Item() { Id = 2, ProductName = "Restaurant"}
            };
            _receipt.CreatedOn = _CreatedOn;
            _receipt.ReceiptItems = itemsList;

            _receiptService = new ReceiptService(_context);
            _receipt1 = new Receipt();
            _CreatedOn1 = new DateTime(2022, 8, 10, 12, 00, 00);
            var itemsList1 = new List<Item>()
            {
                new Item() { Id = 3, ProductName = "Store"},
                new Item() { Id = 4, ProductName = "Cinema"}
            };
            _receipt1.CreatedOn = _CreatedOn1;
            _receipt1.ReceiptItems = itemsList1;
        }

        [TestMethod]
        public void ReceiptApiTest_AddReceipt_ResultIsNotNull()
        {
            // Act
            var result = _controller.AddReceipt(_receipt);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReceiptApiTest_GetReceiptsByDate_ReturnsOkObjectResult()
        {
            // Arrange
            var from = new DateTime(2020, 1, 1);
            var to = new DateTime(2021, 10, 10);

            // Arrange
            _controller.AddReceipt(_receipt);
            _controller.AddReceipt(_receipt1);
            IActionResult actionResult = _controller.GetReceiptsByDate(from, to);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}