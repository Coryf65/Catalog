using System;
using System.Threading.Tasks;
using Catalog.Api.Controllers;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Catalog.UnitTests
{
    // How to name Tests
    // UnitOfWork_StateUnderTest_ExpectedBehaviour()
    // basic layout of a unit test
    // Arrange
    // Act
    // Assert
    public class ItemsControllerTests
    {
        // Fact, is needed for each test
        // test for GetItemsAsync(id)        
        [Fact]
        public async Task GetItemsAsync_ItemNotFound_ReturnsNotFound()
        {
            // Arrange
            var repoStub = new Mock<IItemsRepository>();
            repoStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Item)null);

            var loggerStub = new Mock<ILogger<ItemsController>>();

            var controller = new ItemsController(repoStub.Object); //, loggerStub.Object
            
            // Act
            var result = await controller.GetItemAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}