namespace idpa_vorprojekt_gewinnverteilung.Helpers
{
    public class InputValidation
    {
        // Validates if the input is a numeric value
        public bool ValidateNumericInput(string input)
        {
            return double.TryParse(input, out _);
        }

        // Validates if the input is within the specified range
        public bool ValidateRange(double input, double min, double max)
        {
            return input >= min && input <= max;
        }

        // Validates if the input is not null or empty
        public bool ValidateRequiredField(string input)
        {
            return !string.IsNullOrEmpty(input);
        }
    }
}
