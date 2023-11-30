using Mladim.Client.ViewModels.Survey;
using MudBlazor;

namespace Mladim.Client.Utilities.Converters;

public class ContingencyCalculatorSelectorToBoolConverter : BoolConverter<ContingencyCalculator>
{
    private SurveyResponseSelector ResponseSelector;
    public ContingencyCalculatorSelectorToBoolConverter(SurveyResponseSelector responseSelector)
    {
        this.ResponseSelector = responseSelector;

        SetFunc = OnSet;
        GetFunc = OnGet;
    }   


    private ContingencyCalculator OnGet(bool? value)
    {
        try
        {
            return (value == true) ? new ContingencyTablePercentages(ResponseSelector) 
                : new ContingencyTableParticipants(ResponseSelector);
        }
        catch (Exception e)
        {
            UpdateGetError("Conversion error: " + e.Message);
            return new ContingencyTableParticipants(ResponseSelector);
        }
    }

    private bool? OnSet(ContingencyCalculator? arg)
    {
        try
        {
            if (arg == null)
                return false;
            if (arg.GetType() == typeof(ContingencyTablePercentages))
                return true;
            if (arg.GetType() == typeof(ContingencyTableParticipants))
                return false;
            else
                return false;
        }
        catch (FormatException e)
        {
            UpdateSetError("Conversion error: " + e.Message);
            return null;
        }
    }

}
