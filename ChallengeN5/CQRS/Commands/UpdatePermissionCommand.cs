using AutoMapper;
using ChallengeN5.Models;
using ChallengeN5.Repositories;
using MediatR;
using Nest;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeN5.CQRS.Commands
{
    public class UpdatePermissionCommand : MediatR.IRequest<Permission>
    {
        public UpdatePermissionDto Permission { get; set; }

        public UpdatePermissionCommand(UpdatePermissionDto permission)
        {
            Permission = permission;
        }
    }

    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, Permission>
    {
        private readonly IGenericRepository<Permission> _permissionRepository;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;

        public UpdatePermissionCommandHandler(IGenericRepository<Permission> permissionRepository, IMapper mapper, IElasticClient elasticClient)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _elasticClient = elasticClient;
        }

        public async Task<Permission> Handle(UpdatePermissionCommand command, CancellationToken cancellationToken)
        {
            var updatedPermission = _mapper.Map<Permission>(command.Permission);

            await _permissionRepository.UpdateAsync(command.Permission.Id, updatedPermission, cancellationToken);

            var indexResponse = await _elasticClient.IndexDocumentAsync(updatedPermission);
            if (!indexResponse.IsValid)
            {
                Console.WriteLine($"Error al indexar el documento: {indexResponse.DebugInformation}");
            }

            return updatedPermission;
        }
    }
}
