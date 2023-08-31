namespace Mladim.Client.Models;

public class MladimApiUrls
{

    public string GetOrganizationById { get; set; } = string.Empty;
    public string GetOrganizationsByUserId { get; set; } = string.Empty;
    public string RemoveOrganization { get; set; } = string.Empty;
    public string OrganizationCommand { get; set; } = string.Empty;


    public string GetProjectById { get; set; } = string.Empty;
    public string GetProjectsByOrganizationId { get; set; } = string.Empty;
    public string RemoveProject { get; set; } = string.Empty;
    public string ProjectCommand { get; set; } = string.Empty;


    public string GetActivityById { get; set; } = string.Empty;
    public string GetActivitiesByOrganizationId { get; set; } = string.Empty;
    public string GetActivitiesByProjectId { get; set; } = string.Empty;
    public string RemoveActivity { get; set; } = string.Empty;
    public string ActivityCommand { get; set; } = string.Empty;


    public string GroupCommand { get; set; } = string.Empty;
    public string GetGroupById { get; set; } = string.Empty;
    public string GetGroupsByOrganizationId { get; set; } = string.Empty; 


    public string GetStafMembersByOrganizationId { get; set; } = string.Empty;
    public string GetLeadStaffMembers { get; set; } = string.Empty;

    public string StaffMemberCommand { get; set; } = string.Empty;

    public string GetParticipantsByOrganizationId { get; set; } = string.Empty;
    public string ParticipantCommand { get; set; } = string.Empty;

    public string GetPartnersByOrganizationId { get; set; } = string.Empty;
    public string PartnerCommand { get; set; } = string.Empty;


    public string Login { get; set; } = string.Empty;
    public string ConfirmRegistration { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string AccountCommand { get; set; } = string.Empty;
    public string GetAccountById { get; set; } = string.Empty;


    public string GetFileByProjectId { get; set; } = string.Empty;
    public string GetFileByActivityId { get; set; } = string.Empty;

    public string AddOrganizationProfileImage { get; set; } = string.Empty;
    public string AddOrganizationBannerImage { get; set; } = string.Empty;


    public string GetOrganizationStatistics { get; set; }  = string.Empty;
    public string GetProjectStatistics { get; set; } = string.Empty;
}
