using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptApi.Controllers;
using ReceiptApi.Core.Models;
using ReceiptApi.Core.Validator;
using ReceiptApi.Service;

namespace RecieptControllerApiTest
{
    [TestClass]
    public class RecieptControllerApiTest
    {
        private Mock<IReceiptService> _receiptServiceMock;
        private Mock<ReceiptValidator> _receiptValidatorMock;
        private ReceiptController _controller;
        private Receipt _receipt;
        private Receipt _receipt1;
        private List<Receipt> _receiptsList;
        private List<Item> _list;
        private DateTime _created;

        [TestInitialize]
        public void Setup()
        {
            _list = new List<Item>()
            {
                new Item()
                {
                    Id = 1,
                    ProductName = "Restaurant"
                }
            };

            _receipt = new Receipt()
            {
                Id = 1,
                CreatedOn = new DateTime(2022, 11, 7),
                ReceiptItems = _list
            };

            _receipt1 = new Receipt()
            {
                Id = 2,
                CreatedOn = new DateTime(2000, 11, 8),
                ReceiptItems = _list
            };

            _receiptsList = new List<Receipt>() { _receipt, _receipt1 };
            _receiptServiceMock = new Mock<IReceiptService>();
            _created = new DateTime(2022, 11, 8);
            _receiptValidatorMock = new Mock<ReceiptValidator>();
            _controller = new ReceiptController(_receiptServiceMock.Object, _receiptValidatorMock.Object);
        }

        [TestMethod]
        public void ReceiptApiTest_AddReceipt_ShouldReturnReceipt()
        {
            _receiptServiceMock.Setup(r => r.AddReceipt(_receipt)).Returns(_receipt);

            var actionResult = _controller.AddReceipt(_receipt) as ObjectResult;

            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Value as Receipt, _receipt);
        }

        [TestMethod]
        public void ControllerTest()
        {

        }
    }
}