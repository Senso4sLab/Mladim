using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;

namespace Mladim.Client.MappingProfiles.Profiles.Staff;

public class StaffMembers : Profile
{
    public StaffMembers()
    {
        //CreateMap<StaffMemberRoleVM, StaffMemberCommandDto>();
        //CreateMap<StaffMemberQueryDto, StaffMemberRoleVM>();
        //CreateMap<StaffMemberQueryDto, NamedEntityVM>();






        CreateMap<StaffMemberDetailsQueryDto, StaffMemberVM>();
        CreateMap<StaffMemberVM, AddStaffMemberCommandDto>();
        CreateMap<StaffMemberVM, UpdateStaffMemberCommandDto>();



    }
}
