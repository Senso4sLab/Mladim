namespace Mladim.Client.Models;

public class MladimApiUrls
{

    public string GetOrganizationById { get; set; }
    public string GetOrganizationsByUserId { get; set; }
    public string RemoveOrganization { get; set; }
    public string OrganizationCommand { get; set; }

    public string GetStafMembersByOrganizationId { get; set; }
    public string StaffMemberCommand { get; set; }   

    public string GetParticipantsByOrganizationId { get; set; }
    public string ParticipantCommand { get; set; }

    public string GetPartnersByOrganizationId { get; set; }
    public string PartnerCommand { get; set; }


    public string Login { get; set; }
}
