using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Organization;

public class AddOrganizationProfileImageDto
{
    public int OrganizationId { get; set; }
    public List<byte> Data { get; set; } = new();
    public string FileName { get; set; } = string.Empty;

    private AddOrganizationProfileImageDto(int organizationId, List<byte> data, string fileName) =>
        (OrganizationId, Data, FileName) = (organizationId, data, fileName);


    public static AddOrganizationProfileImageDto Create(int organizationId, List<byte> data, string fileName) =>
        new AddOrganizationProfileImageDto(organizationId, data, fileName);
}
