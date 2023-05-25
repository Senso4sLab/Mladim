using AutoMapper;
using Mladim.Client.Models;
using Mladim.Domain.Dtos;

namespace Mladim.Client.MappingProfiles.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<OrganizationDto, Organization>().ReverseMap();

            CreateMap<StaffMemberDto, StaffMember>().ReverseMap();
            CreateMap<StaffMember, AddStaffMemberCommand>();
            CreateMap<StaffMember, UpdateStaffMemberCommand>();

            
        }
    }
}
