using AutoMapper;
using ChallengeN5.Models;
using ChallengeN5.Repositories;
using MediatR;
using Nest;


namespace ChallengeN5.CQRS.Commands
{
    public class CreatePermissionCommand : MediatR.IRequest<Permission>
    {
        public Permission Permission { get; set; }

        public CreatePermissionCommand(Permission permission)
        {
            Permission = permission;
        }
    }

    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Permission>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Permission> _permissionRepository;
        private readonly IElasticClient _elasticClient;

        public CreatePermissionCommandHandler(IGenericRepository<Permission> permissionRepository, IElasticClient elasticClient, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _elasticClient = elasticClient;
            _mapper = mapper;
        }

        public async Task<Permission> Handle(CreatePermissionCommand command, CancellationToken cancellationToken)
        {
            var permission = _mapper.Map<Permission>(command.Permission);

            await _permissionRepository.AddAsync(permission, cancellationToken);

            var indexResponse = await _elasticClient.IndexDocumentAsync(permission);
            if (!indexResponse.IsValid)
            {
                Console.WriteLine($"Error al indexar el documento: {indexResponse.DebugInformation}");
            }

            return permission;
        }
    }

}
