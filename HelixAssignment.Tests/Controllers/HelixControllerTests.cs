namespace HelixAssignment.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Net;
    using Microsoft.Extensions.Logging;
    using System;
    using HelixAssignment.ViewModel;
    using HelixAssignment.BAL;
    using HelixAssignment.DAL;
    using NUnit.Framework;
    using Moq;

    [TestFixture]
    public class HelixControllerTests 
    {
        private const int Id = 1;
        private const string Name = "George";
        private const int Age = 26;
        private const string BaseUri = "http://localhost:3001/api/people";
        private Mock<IHelixEventService> _eventService;

        [SetUp]
        public void Setup()
        {
            _eventService = new Mock<IHelixEventService>();
        }

        [Test]
        public void HelixControllerTests_IsInstanceOf_HelixController_ReceivingHelixEventServiceOnCreation_Test()
        {
            // Arrange
            var controller = new HelixController(_eventService.Object);

            // Act | Assert
            Assert.IsInstanceOf<HelixController>(controller);
        }

        /*
        public async Task<ActionResult<ICollection<HelixEvent>>> GetProducts()
        {        
        }
        public async Task<ActionResult<HelixEvent>> GetEventById(long id)
        {       
        }
        public async Task<HttpResponseMessage> PutProductsToEvent([FromBody]HelixEventViewModel eventRequest)
        {       
        }*/       
    }
}