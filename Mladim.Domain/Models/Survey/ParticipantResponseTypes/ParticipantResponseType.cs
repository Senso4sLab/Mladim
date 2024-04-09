using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models.Survey.ParticipantResponseTypes;

public record ParticipantResponseType(Enum ResponseType, float Value)
{
    public static ParticipantResponseType Zero(Enum responseType) =>
         new ParticipantResponseType(responseType, 0);

    public static ParticipantResponseType Create(Enum responseType, float value) =>
         new ParticipantResponseType(responseType, value);


    public ParticipantResponseType ToPercent(float numOfParticipants) =>
        new ParticipantResponseType(ResponseType, (float)Math.Round((this.Value * 100 / numOfParticipants), 1));

}

public class ParticipantResponseTypeByCriterion //ena vrstica
{
    public string Criterion { get; }
    public IEnumerable<ParticipantResponseType> ReponseTypesPerCriterion { get; } = new List<ParticipantResponseType>(); // vse colone
    public ParticipantResponseTypeByCriterion(string criterion, IEnumerable<ParticipantResponseType> reponseTypesPerCriterion)
    {
        this.Criterion = criterion;
        this.ReponseTypesPerCriterion = reponseTypesPerCriterion.ToList();
    }    

    public ParticipantResponseTypeByCriterion ToPercent(float numOfParticipants) =>
       new ParticipantResponseTypeByCriterion(this.Criterion, ReponseTypesPerCriterion.Select(rt => rt.ToPercent(numOfParticipants)).ToList());

}
public class ContingencyTable
{
    public string Name { get; }
    public IEnumerable<ParticipantResponseTypeByCriterion> ParticipantsByCriteria { get; } = new List<ParticipantResponseTypeByCriterion>();
    public ContingencyTable(string name, IEnumerable<ParticipantResponseTypeByCriterion> participantsByCriteria)
    {
        this.Name = name;
        this.ParticipantsByCriteria = participantsByCriteria.ToList();
    }
}
