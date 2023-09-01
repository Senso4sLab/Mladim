namespace Mladim.Domain.Dtos.Organization;

public class AddUserProfileImageDto
{
    public string UserId { get; set; }
    public List<byte> Data { get; set; } = new();
    public string FileName { get; set; } = string.Empty;

    private AddUserProfileImageDto(string userId, List<byte> data, string fileName) =>
        (UserId, Data, FileName) = (userId, data, fileName);


    public static AddUserProfileImageDto Create(string userId, List<byte> data, string fileName) =>
        new AddUserProfileImageDto(userId, data, fileName);

}
