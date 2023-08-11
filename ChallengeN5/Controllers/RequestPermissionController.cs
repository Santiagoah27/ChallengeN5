using ChallengeN5.CQRS.Commands;
using ChallengeN5.CQRS.Queries;
using ChallengeN5.Models;
using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ChallengeN5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestPermissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProducer<Null, string> _producer;

        public RequestPermissionController(IMediator mediator, IProducer<Null, string> producer)
        {
            _mediator = mediator;
            _producer = producer;
        }
        [HttpPost("permission")]
        public async Task<ActionResult<Permission>> CreatePermission(CreatePermissionCommand command)
        {
            var result = await _mediator.Send(command);

            var message = new { Id = Guid.NewGuid(), NameOperation = "request", Permission = result };
              _producer.ProduceAsync("operations", new Message<Null, string> { Value = JsonSerializer.Serialize(message) });

            return Ok(new
            {
                message = "Permission created successfully",
                permission = result
            });
        }

        [HttpPut("permission/{id}")]
        public async Task<ActionResult<Permission>> UpdatePermission(int id, UpdatePermissionCommand command)
        {
            if (id != command.Permission.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            var message = new { Id = Guid.NewGuid(), NameOperation = "modify", Permission = result };
             _producer.ProduceAsync("operations", new Message<Null, string> { Value = JsonSerializer.Serialize(message) });

            return Ok(new 
            { 
                message = "Permission updated successfully",
                permission = result
            });
        }

        [HttpGet("permissions")]
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
        {
            var result = await _mediator.Send(new GetAllPermissionsQuery());

            var message = new { Id = Guid.NewGuid(), NameOperation = "get", Permissions = result };
            _producer.ProduceAsync("operations", new Message<Null, string> { Value = JsonSerializer.Serialize(message) });

            return Ok(result);
        }


    }
}
