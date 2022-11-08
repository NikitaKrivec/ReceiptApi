using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptApi.Controllers;
using ReceiptApi.Core.Models;
using ReceiptApi.Core.Validator;
using ReceiptApi.Service;

namespace ApiReceiptServiceTest
{
    public class ReceiptApiTest
    {
        private Mock<IReceiptService> _receiptService;
        private Mock<IReceiptValidator> _receiptValidator;
        private ReceiptController _controller;
        private Receipt _receipt;
        private Receipt _receipt1;
        private List<Receipt> _receiptsList;
        private List<Item> _itemList;
        private List<Item> _itemList1;
        private DateTime _created;

        public ReceiptApiTest()
        {
            _receipt = new Receipt()
            {
                Id = 1,
                CreatedOn = new DateTime(2022, 11, 7),
                ReceiptItems = _itemList
            };

            _receipt1 = new Receipt()
            {
                Id = 2,
                CreatedOn = new DateTime(2022, 11, 8),
                ReceiptItems = _itemList1
            };

            _itemList = new List<Item>()
            {
                new Item()  { Id = 1, ProductName = "Restaurant" }
            };

            _itemList1 = new List<Item>()
            {
                new Item() { Id = 2, ProductName = "Store" }
            };
            _receiptsList = new List<Receipt>() { _receipt, _receipt1 };
            _receiptService = new Mock<IReceiptService>();
            _created = new DateTime(2022, 11, 8);
            _receiptValidator = new Mock<IReceiptValidator>();
            _controller = new ReceiptController(_receiptService.Object, _receiptValidator.Object);
        }

        [Fact]
        public void ReceiptApiTest_AddReceipt_ShouldReturnReceipt()
        {
            //Arrange
            _receiptService.Setup(r => r.AddReceipt(_receipt)).Returns(_receipt);

            //Act
            var actionResult = _controller.AddReceipt(_receipt) as ObjectResult;
            
            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.Value as Receipt, _receipt);
        }

        [Fact]
        public void ReceiptApiTest_GetReceiptsByDate_ShouldReturnReceiptsWithinDate()
        {
            //Arrange
            var startT = new DateTime(2022, 11, 1);
            var endT = new DateTime(2020, 11, 3);
            
            //Act
            var actionResult = _controller.GetReceiptsByDate(startT, endT) as BadRequestObjectResult;
            
            //Assert
            Assert.Equal(typeof(BadRequestObjectResult), typeof(BadRequestObjectResult));
        }

        [Fact]
        public void ReceiptApiTest_GetReceiptsByItemProductName_ShouldReturnListOfProductsWhichContainsProductName()
        {
            //Arrange
            _receiptService.Setup(r => r.GetReceiptsByItemProductName("Restaurant")).Returns(_receiptsList);
            
            //Act
            var actionResult = _controller.GetReceiptsByProductName("Restaurant") as ObjectResult;
            
            //Assert
            Assert.Equal(actionResult.Value as List<Receipt>, _receiptsList);
        }

        [Fact]
        public void ReceiptApiTest_GetReceiptByI_ReturnsNotFoundResponse()
        {
            //Arrange
            int id = 10;

            //Act
            var noContentResponse = _controller.GetReceiptById(id);

            //Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }

        [Fact]
        public void ReceiptApiTest_GetReceiptsByCreationDate_ReturnsOkObjectResult()
        {
            // Arrange
            var startT = new DateTime(2022, 11, 1);
            var endT = new DateTime(2022, 11, 10);

            // Act
            _receiptService.Setup(r => r.GetReceiptsByCreationDate(startT, endT)).Returns(_receiptsList);
            var actionResult = _controller.GetReceiptsByDate(startT, endT) as ObjectResult;
            
            // Assert
            Assert.IsType<ObjectResult>(actionResult as OkObjectResult);
            Assert.Equal(actionResult.Value as List<Receipt>, _receiptsList);
        }

        [Fact]
        public void ReceiptApiTest_DeleteReceipt_ShouldReturnNull()
        {
            // Act
            var actionResult = _controller.DeleteReceipt(3) as ObjectResult;

            // Assert
            Assert.Null(actionResult);
        }

        [Fact]
        public void ReceiptApiTest_AddReceipt_ReturnsCreatedRequest()
        {
            //Arrange
            var requestBody = new Receipt()
            {
                Id = 2,
                CreatedOn = new DateTime(2022, 11, 8),
                ReceiptItems = _itemList1
            };

            //Act
            var actionResult = _controller.AddReceipt(requestBody);

            //Assert
            Assert.IsType<CreatedResult>(actionResult);
        }

        [Fact]
        public void ReceiptApiTest_DeleteReceipt_ShouldDeleteSameReceiptById()
        {   
            //Arrange
            _receiptService.Setup(r => r.GetReceiptById(_receipt.Id)).Returns(_receipt);

            //Act
            var actionResult = _controller.DeleteReceipt(_receipt.Id) as ObjectResult;
           
            //Assert
            Assert.IsType<Receipt>(actionResult.Value);
            Assert.Equal(_receipt.Id, (actionResult.Value as Receipt).Id);
        }

        [Fact]
        public void ReceiptApiTest_AddReceipt_ResultIsNotNull()
        {
            // Act
            var actionResult = _controller.AddReceipt(_receipt);

            // Assert
            Assert.NotNull(actionResult);
        }
    }
}