using ChallengeN5.Models;
using ChallengeN5.Repositories;
using MediatR;


namespace ChallengeN5.CQRS.Queries
{
    public class GetAllPermissionsQuery : IRequest<IEnumerable<Permission>>
    {
    }

    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<Permission>>
    {
        private readonly IGenericRepository<Permission> _permissionRepository;

        public GetAllPermissionsQueryHandler(IGenericRepository<Permission> permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<Permission>> Handle(GetAllPermissionsQuery query, CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.ListAllAsync();

            return permissions;
        }
    }
}
