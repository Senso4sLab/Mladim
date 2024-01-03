using Mladim.Client.ViewModels.Survey;
using MudBlazor;

namespace Mladim.Client.Utilities.Converters;
public class CriterionSelectorStringConverter : MudBlazor.Converter<SurveyCriterionSelector, string>
{
    public CriterionSelectorStringConverter()
    {
        SetFunc = OnSet;
        GetFunc = OnGet;
    }

    private string? OnSet(SurveyCriterionSelector? arg)
    {
        try
        {
            if (arg.GetType() == typeof(GenderSelector) || arg.GetType() == typeof(AgeGroupSelector))
                return arg.Name;
            else
                return string.Empty;
        }
        catch (FormatException e)
        {
            UpdateSetError("Conversion error: " + e.Message);
            return string.Empty;
        }
    }

    private SurveyCriterionSelector? OnGet(string? arg)
    {
        try
        {
            if (arg == "Spol")
                return SurveyCriterionSelector.GenderSelector();
            else if (arg == "Starostna skupina")
                return SurveyCriterionSelector.AgeGroupSelector();
            else
                return null;
        }
        catch (Exception e)
        {
            UpdateGetError("Conversion error: " + e.Message);
            return null;
        }
    }
}

//public class CriterionSelectorConverter : BoolConverter<SurveyCriterionSelector>
//{
    
//    public CriterionSelectorConverter()
//    {
//        SetFunc = OnSet;
//        GetFunc = OnGet;       
//    }    

//    private SurveyCriterionSelector OnGet(bool? value)
//    {
//        try
//        {
//            return (value == true) ? SurveyCriterionSelector.GenderSelector()
//                : SurveyCriterionSelector.AgeGroupSelector();
//        }
//        catch (Exception e)
//        {
//            UpdateGetError("Conversion error: " + e.Message);
//            return null;
//        }
//    }

//    private bool? OnSet(SurveyCriterionSelector? arg)
//    {
//        try
//        {
//            if (arg == null)
//                return false;
//            if (arg.GetType() == typeof(GenderSelector))
//                return true;
//            if (arg.GetType() == typeof(AgeGroupSelector))
//                return false;
//            else
//                return false;
//        }
//        catch (FormatException e)
//        {
//            UpdateSetError("Conversion error: " + e.Message);
//            return null;
//        }
//    }

//}
