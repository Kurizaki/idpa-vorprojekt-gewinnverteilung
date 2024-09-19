namespace idpa_vorprojekt_gewinnverteilung.Helpers
{
    public class CalculationLogic
    {
        // Calculates the legal retained earnings based on input values
        public double CalculateLegalRetainedEarnings(double profit, double capital, double carryforward, double reserves)
        {
            // Adjust capital if carryforward is negative
            if (carryforward < 0)
            {
                capital += carryforward;
                carryforward = 0;
            }

            // Legal retained earnings calculation
            double targetReserves = capital * 0.2;
            if (carryforward + reserves < targetReserves)
            {
                reserves += profit * 0.05; // Add 5% of profit to reserves
            }

            return carryforward + reserves >= targetReserves ? 0 : reserves;
        }

        // Calculates dividend based on capital and dividend percentage
        public double CalculateDividend(double dividendPercentage, double capital, double profit)
        {
            return (capital * dividendPercentage) / 100;
        }
    }
}
