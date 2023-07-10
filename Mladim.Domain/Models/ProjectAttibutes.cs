namespace Mladim.Domain.Models;


public class ProjectAttibutes  : BaseAttibutes
{   
    public string? WebpageUrl { get; private set; }
    private ProjectAttibutes()
    {

    }

    private ProjectAttibutes(string name, string description, string? webpageUrl = null)
    {
        this.Name = name;
        this.Description = description;
        this.WebpageUrl = webpageUrl;
    }


    public static  ProjectAttibutes Create(string name, string description, string? webpageUrl = null) =>
        new ProjectAttibutes(name, description, webpageUrl);


}
