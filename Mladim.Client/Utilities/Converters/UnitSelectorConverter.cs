using Mladim.Client.ViewModels.Survey;
using MudBlazor;

namespace Mladim.Client.Utilities.Converters;

public class UnitSelectorConverter : BoolConverter<UnitSelector>
{
    
    public UnitSelectorConverter()
    {  

        SetFunc = OnSet;
        GetFunc = OnGet;

        
    }   


    private UnitSelector OnGet(bool? value)
    {
        try
        {
            return (value == true) ? new PercantagesUnit() 
                : new NumOfParticipantsUnit();
        }
        catch (Exception e)
        {
            UpdateGetError("Conversion error: " + e.Message);
            return null;
        }
    }

    private bool? OnSet(UnitSelector? arg)
    {
        try
        {
            if (arg == null)
                return false;
            if (arg.GetType() == typeof(PercantagesUnit))
                return true;
            if (arg.GetType() == typeof(NumOfParticipantsUnit))
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
