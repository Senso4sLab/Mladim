using Mladim.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Mladim.Client.Components.Dialogs;

public class AnonymousParticipantTableElements
{   
    [Display(Name = "Starostna skupina")]
    public AgeGroups AgeGroup { get; set; }

    [Display(Name = "Moški")]
    public int Male { get; set; }

    [Display(Name = "Ženska")]
    public int Female { get; set; } 

}
