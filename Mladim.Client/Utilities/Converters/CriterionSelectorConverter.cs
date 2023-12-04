using Mladim.Client.ViewModels.Survey;
using MudBlazor;

namespace Mladim.Client.Utilities.Converters;

public class CriterionSelectorConverter : BoolConverter<SurveyCriterionSelector>
{
    
    public CriterionSelectorConverter()
    {
        SetFunc = OnSet;
        GetFunc = OnGet;       
    }

    

    private SurveyCriterionSelector OnGet(bool? value)
    {
        try
        {
            return (value == true) ? SurveyCriterionSelector.GenderSelector()
                : SurveyCriterionSelector.AgeGroupSelector();
        }
        catch (Exception e)
        {
            UpdateGetError("Conversion error: " + e.Message);
            return null;
        }
    }

    private bool? OnSet(SurveyCriterionSelector? arg)
    {
        try
        {
            if (arg == null)
                return false;
            if (arg.GetType() == typeof(GenderSelector))
                return true;
            if (arg.GetType() == typeof(AgeGroupSelector))
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
