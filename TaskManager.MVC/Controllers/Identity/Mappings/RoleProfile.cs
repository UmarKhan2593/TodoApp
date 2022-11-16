using AutoMapper;
using TaskManager.Presentation.Web.Infrastructure.Requests.Identity.Roles;
using TaskManager.Presentation.Web.Infrastructure.Responses.Identity.Roles;

namespace TaskManager.Presentation.Web.Areas.Identity.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}
