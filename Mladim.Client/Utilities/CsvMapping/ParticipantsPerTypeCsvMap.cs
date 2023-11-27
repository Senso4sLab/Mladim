using CsvHelper.Configuration;
using Mladim.Client.ViewModels.Survey;

namespace Mladim.Client.Utilities.CsvMapping;

public class ParticipantsPerTypeCsvMap : ClassMap<ParticipantsPerType>
{
    public ParticipantsPerTypeCsvMap()
    {
        Map(ppt => ppt.Type).Name("Vrsta odgovora");
        Map(ppt => ppt.NumOfParticipants).Name("št. udeležencev");
    }
}
