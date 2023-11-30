using CsvHelper.Configuration;
using Mladim.Client.ViewModels.Survey;

namespace Mladim.Client.Utilities.CsvMapping;

public class ParticipantsPerTypeCsvMap : ClassMap<ParticipantsByResponseType>
{
    public ParticipantsPerTypeCsvMap()
    {
        Map(ppt => ppt.ResponseType).Name("Vrsta odgovora");
        Map(ppt => ppt.Unit).Name("št. udeležencev");
    }
}
