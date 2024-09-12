namespace idpa_vorprojekt_gewinnverteilung.Helpers;

public class InputValidation
{
    public bool ValidateNumericInput(string input)
    {
        try
        {
            if (double.TryParse(input, out double result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public bool ValidateRange(double input, double min, double max)
    {
        if (input >= min && input <= max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ValidateRequiredField(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

