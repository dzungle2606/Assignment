using System;

using HelixAssignment.Controllers;
using Moq;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using System.Collections.Generic;
using HelixAssignment.BAL;
using HelixAssignment.DAL;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HelixAssignment.ViewModel;

namespace HelixAssignment.Test.Controllers
{
    public class HelixControllerTests
    {
        public HelixControllerTests()
        {
        }


        private const int helixEventId = 1;
        private Mock<IHelixEventService> _helixService;

        [SetUp]
        public void Setup()
        {
            _helixService = new Mock<IHelixEventService>();
        }

        [Test]
        public void HelixController_IsInstanceOf_HelixController_ReceivingHelixServiceOnCreation_Test()
        {
            // Arrange
            var controller = new HelixController(_helixService.Object);

            // Act | Assert
            IsInstanceOf<HelixController>(controller);
        }

        [Test]
        public void HelixController_GetProducts_ReturnsOkResultWithEventListAsContent_Test()
        {
            // Arrange
            List<HelixEvent> eventList = CreateEventList();
            _helixService.Setup(m => m.GetAllEvents()).ReturnsAsync(eventList.ToList());

            var controller = new HelixController(_helixService.Object);

            // Act
            var actionResultTask = controller.GetProducts();
            actionResultTask.Wait();
            var result = actionResultTask.Result.Result as OkObjectResult;

            // Assertdotne
            IsNotNull(result);
            IsNotNull(result.Value);
            ICollection<HelixEvent> list = (ICollection<HelixEvent> )result.Value;
            AreEqual(eventList.Count, list.Count);
        }

        private List<HelixEvent> CreateEventList()
        {
            var list = new List<HelixEvent>
            {
                new HelixEvent() { HelixEventId = 1, 
                                    Timestamp = DateTime.Now, 
                                    ProductsLink = new List<HelixEventProduct>() { 
                                                new HelixEventProduct() { EventId = 1, ProductId = 1, Quantity = 1, Sale_amount = 10 },
                                                new HelixEventProduct() { EventId = 1, ProductId = 2, Quantity = 2, Sale_amount = 20 }
                                                }  
                                },
                new HelixEvent(){ HelixEventId = 2 ,
                                    Timestamp = DateTime.Now,
                                    ProductsLink = new List<HelixEventProduct>() {
                                                new HelixEventProduct() { EventId = 2, ProductId = 1, Quantity = 1, Sale_amount = 10 },
                                                new HelixEventProduct() { EventId = 2, ProductId = 2, Quantity = 2, Sale_amount = 20 }
                                                }
                                }
            };
            return list;
        }

        [Test]
        public void HelixController_GetProducts_ListIsEmpty_ReturnsOkResultWithEventListAsContent_Test()
        {
            // Arrange
            _helixService.Setup(m => m.GetAllEvents()).ReturnsAsync(new List<HelixEvent>().ToList());

            var controller = new HelixController(_helixService.Object);

            // Act
            var actionResultTask = controller.GetProducts();
            actionResultTask.Wait();
            var result = actionResultTask.Result.Result as OkObjectResult;

            // Assert
            IsNotNull(result);
        }


        [Test]
        public void HelixController_GetEventById_EventByIdFromService_ReturnsOkWithEventEntityAsContent_Test()
        {
            // Arrange
            var helixEvent = CreateDefaultHelixEventObject();
            _helixService.Setup(m => m.GetEventById(It.IsAny<long>())).ReturnsAsync(helixEvent);


            var controller = new HelixController(_helixService.Object);

            // Act
            var actionResultTask = controller.GetEventById(helixEventId);
            actionResultTask.Wait();
            var result = actionResultTask.Result.Result as OkObjectResult;

            // Assert
            IsNotNull(result);
            var responseItem = (HelixEvent)result.Value;

            IsNotNull(responseItem);
            AreEqual(helixEventId, responseItem.HelixEventId);           
        }

        private HelixEvent CreateDefaultHelixEventObject()
        {
            return new HelixEvent()
            {
                HelixEventId = helixEventId,
                Timestamp = DateTime.Now,
                ProductsLink = new List<HelixEventProduct>() {
                        new HelixEventProduct() { EventId = 1, ProductId = 1, Quantity = 1, Sale_amount = 10 },
                        new HelixEventProduct() { EventId = 1, ProductId = 2, Quantity = 2, Sale_amount = 20 }
                    }
            };
        }

        [Test]
        public void HelixController_GetEventById_EventByIdFromService_EventCouldNotBeFound_ReturnsNotFoundResult_Test()
        {
            // Arrange
            var controller = new HelixController(_helixService.Object);

            // Act
            var actionResult = controller.GetEventById(100);
            actionResult.Wait();

            var result = actionResult.Result.Result;

            // Assert
            IsNotNull(result);
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }


        [Test]
        public void HelixController_PutProductsToEvent_HelixEventUpdated_ReturnsOkResultWithContent_Test()
        {
            // Arrange
            var helixEvent = CreateValidDefaultHelixEventViewModelObject();
            _helixService.Setup(m => m.Update(It.IsAny<HelixEventViewModel>())).ReturnsAsync(true);

            var controller = new HelixController(_helixService.Object);

            // Act
            var actionResult = controller.PutProductsToEvent(helixEvent);
            actionResult.Wait();

            // Assert
            IsNotNull(actionResult);
            Assert.IsInstanceOf(typeof(OkResult), actionResult.Result);
        }

        private HelixEventViewModel CreateValidDefaultHelixEventViewModelObject()
        {
            return new HelixEventViewModel()
            {
                Id = helixEventId,
                Timestamp = DateTime.Now,
                Products = new List<ProductViewModel>()
                    {
                        new ProductViewModel() {                        
                                Id = 1,
                                Quantity = 1,
                                Name = "Product 1",
                                Sale_amount = 10
                        },
                        new ProductViewModel() {
                                Id = 2,
                                Quantity = 2,
                                Name = "Product 2",
                                Sale_amount = 20
                        }
                    }
            };
        }

     

        [Test]
        public void HelixController_PutProductsToEvent_MissingEventInfo_ReturnsBadRequestResult_Test()
        {
            // Arrange
            var helixEvent = CreateInValidDefaultHelixEventViewModelObject();

            var controller = new HelixController(_helixService.Object);
            controller.ModelState.AddModelError("Id", "Required");

            // Act
            var actionResult = controller.PutProductsToEvent(helixEvent);
            actionResult.Wait();

            // Assert
            IsNotNull(actionResult);
            Assert.IsInstanceOf(typeof(BadRequestResult), actionResult.Result);
        }

        private HelixEventViewModel CreateInValidDefaultHelixEventViewModelObject()
        {
            return new HelixEventViewModel()
            {
            };
        }
    }
}
