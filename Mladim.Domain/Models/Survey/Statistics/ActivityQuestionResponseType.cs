using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models.Survey.Statistics;

public class ActivityQuestionResponseType
{
    public int ActivityId { get; }
    public IEnumerable<QuestionResponseTypeSelector> Selector { get; }

    public ActivityQuestionResponseType(int activityId, IEnumerable<QuestionResponseTypeSelector> selector)
    {
        this.ActivityId = activityId;
        this.Selector = selector.ToList();
    }
}
