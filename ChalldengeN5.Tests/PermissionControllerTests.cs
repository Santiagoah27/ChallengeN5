using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChallengeN5.Data;
using ChallengeN5.Controllers;
using Microsoft.AspNetCore.Mvc;
using ChallengeN5.Models;
using Nest;
using MediatR;
using ChallengeN5.Repositories;
using AutoMapper;
using Confluent.Kafka;
using ChallengeN5.CQRS.Queries;
using ChallengeN5.CQRS.Commands;
using System;

namespace ChallengeN5.Tests
{
    public class PermissionRepositoryTest
    {
        [Fact]
        public async Task GetPermissions_ShouldReturnPermissions_WhenCalled()
        {
            // Arrange
            var mockDbContext = new Mock<PermissionDbContext>();
            var repo = new GenericRepository<Permission>(mockDbContext.Object);

            // Act
            var result = await repo.ListAllAsync();

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
            var mockRepo = new Mock<IGenericRepository<Permission>>();
            var handler = new GetAllPermissionsQueryHandler(mockRepo.Object);

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
            var mockProducer = new Mock<IProducer<Null, string>>();
            var controller = new RequestPermissionController(mockMediator.Object, mockProducer.Object);

            // Act
            var result = await controller.GetPermissions();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Permission>>>(result);
            Assert.NotNull(actionResult.Value);
        }
    }

    public class CreatePermissionCommandHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldCreatePermission_WhenCalled()
        {
            // Arrange
            var mockRepo = new Mock<IGenericRepository<Permission>>();
            var mockMapper = new Mock<IMapper>();
            var mockElasticClient = new Mock<IElasticClient>();
            var handler = new CreatePermissionCommandHandler(mockRepo.Object, (IElasticClient)mockMapper.Object, (IMapper)mockElasticClient.Object);
            var permission = new Permission
            {
                Id = 0,
                TypeId = 1,
                EmployeeName = "Lola",
                EmployeeLastName = "Matinez",
                PermissionType = 1,
                PermissionDate = DateTime.Now
            };

            var command = new CreatePermissionCommand(permission);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Permission>(), CancellationToken.None), Times.Once());
        }
    }

    public class UpdatePermissionCommandHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldUpdatePermission_WhenCalled()
        {
            // Arrange
            var mockRepo = new Mock<IGenericRepository<Permission>>();
            var mockMapper = new Mock<IMapper>();
            var mockElasticClient = new Mock<IElasticClient>();
            var handler = new UpdatePermissionCommandHandler(mockRepo.Object, mockMapper.Object, mockElasticClient.Object);
            var permission = new UpdatePermissionDto
            {
                Id = 2,
                TypeId = 1,
                EmployeeName = "Paolo",
                EmployeeLastName = "Martinez",
                PermissionType = 1,
                PermissionDate = DateTime.Now
            };
            var command = new UpdatePermissionCommand(permission);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Permission>(), CancellationToken.None), Times.Once());
        }
    }
}
