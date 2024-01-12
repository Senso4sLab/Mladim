using CsvHelper.Configuration;
using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Models.Survey.ParticipantResponseTypes;

namespace Mladim.Client.Utilities.CsvMapping;

public class ParticipantsPerTypeCsvMap : ClassMap<ParticipantResponseType>
{
    public ParticipantsPerTypeCsvMap()
    {
        Map(ppt => ppt.ResponseType).Name("Vrsta odgovora");
        Map(ppt => ppt.Value).Name("št. udeležencev");
    }
}
