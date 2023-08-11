using AutoMapper;
using ChallengeN5.CQRS.Commands;
using ChallengeN5.Models;

namespace ChallengeN5.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, Permission>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdatePermissionDto, Permission>();
        }
    }
}
