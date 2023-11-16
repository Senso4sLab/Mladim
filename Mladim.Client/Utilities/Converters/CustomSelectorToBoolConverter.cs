using Mladim.Client.ViewModels.Survey;
using MudBlazor;

namespace Mladim.Client.Utilities.Converters;

public class CustomSelectorToBoolConverter : BoolConverter<SurveyResponseSelector>
{

    public CustomSelectorToBoolConverter()
    {
        SetFunc = OnSet;
        GetFunc = OnGet;
    }

    private SurveyResponseSelector Gender = new GenderSurveyResponseSelector();
    private SurveyResponseSelector AgeGroup = new AgeGroupSurveyResponseSelector();

    private SurveyResponseSelector OnGet(bool? value)
    {
        try
        {
            return (value == true) ? Gender : AgeGroup;
        }
        catch (Exception e)
        {
            UpdateGetError("Conversion error: " + e.Message);
            return new GenderSurveyResponseSelector();
        }
    }

    private bool? OnSet(SurveyResponseSelector? arg)
    {
        try
        {
            if (arg == null)
                return false;
            if (arg.GetType() == typeof(GenderSurveyResponseSelector))
                return true;
            if (arg.GetType() == typeof(AgeGroupSurveyResponseSelector))
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
