using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChallengeN5.Data;
using ChallengeN5.Repositories;
using ChallengeN5.CQRS.Queries;
using ChallengeN5.CQRS.Handlers;
using ChallengeN5.Controllers;
using Microsoft.AspNetCore.Mvc;
using ChallengeN5.Models;
using Nest;
using MediatR;

namespace ChallengeN5.Tests
{
    public class PermissionRepositoryTest
    {
        [Fact]
        public async Task GetPermissions_ShouldReturnPermissions_WhenCalled()
        {
            // Arrange
            var mockDbContext = new Mock<PermissionDbContext>();
            var repo = new GenericRepository(mockDbContext.Object);

            // Act
            var result = await repo.GetPermissions(CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }

    public class GetAllPermissionsQueryHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldReturnPermissions_WhenCalled()
        {
            // Arrange
            var mockRepo = new Mock<GenericRepository>();
            var mockElasticClient = new Mock<IElasticClient>();
            var handler = new GetAllPermissionsQueryHandler(mockRepo.Object, mockElasticClient.Object);

            // Act
            var result = await handler.Handle(new GetAllPermissionsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }

    public class PermissionControllerTest
    {
        [Fact]
        public async Task GetPermissions_ShouldReturnPermissions_WhenCalled()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new RequestPermissionController(mockMediator.Object);

            // Act
            var result = await controller.GetPermissions();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Permission>>>(result);
            Assert.NotNull(actionResult.Value);
        }
    }
}
