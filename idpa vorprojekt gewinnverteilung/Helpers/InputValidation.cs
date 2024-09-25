namespace idpa_vorprojekt_gewinnverteilung.Helpers
{
    public class InputValidation
    {
        private RemarkManager remarkManager;

        public InputValidation(RemarkManager remarkManager = null)
        {
            this.remarkManager = remarkManager;
        }

        // Validiert, ob die Eingabe numerisch ist
        public bool ValidateNumericInput(string input)
        {
            return double.TryParse(input, out _);
        }

        // Validiert, ob der Wert positiv ist
        public bool ValidatePositiveValue(double input)
        {
            return input >= 0;
        }

        // Überprüft die Eingaben, wobei Gewinn auch negativ sein darf (Verlust)
        public bool ValidateInputs(double profit, double capital, double reserves, double dividend)
        {
            bool isValid = true;

            // Gewinn darf negativ sein (Verlustvortrag ist erlaubt)
            if (profit < 0)
            {
                remarkManager?.AddRemark("Verlustvortrag", "Der Gewinnvortrag ist negativ (Verlust).");
            }

            // Kapital muss positiv und grösser als 0 sein
            if (!ValidatePositiveValue(capital) || capital == 0)
            {
                remarkManager?.AddRemark("Ungültiges Kapital", "Das Kapital muss eine positive Zahl sein.");
                isValid = false;
            }

            // Reserven dürfen nicht negativ sein
            if (!ValidatePositiveValue(reserves))
            {
                remarkManager?.AddRemark("Ungültige Reserven", "Die gesetzlichen Reserven dürfen nicht negativ sein.");
                isValid = false;
            }

            // Dividende muss positiv sein (Dividendenprozentsatz darf nicht negativ sein)
            if (!ValidatePositiveValue(dividend))
            {
                remarkManager?.AddRemark("Ungültige Dividende", "Die Dividende darf nicht negativ sein.");
                isValid = false;
            }

            return isValid;
        }
    }
}
