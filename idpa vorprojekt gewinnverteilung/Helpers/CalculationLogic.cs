namespace idpa_vorprojekt_gewinnverteilung.Helpers;

public class CalculationLogic
{
    // Calculates the legal retained earnings based on the input values
    public double CalculateLegalRetainedEarnings(double profit, double capital, double carryforward, double reserves)
    {
        // If the carryforward is negative, it is subtracted from the capital
        while (carryforward < 0)
        {
            capital += carryforward;
            carryforward = 0;
        }

        // Calculation of legal retained earnings
        if (carryforward >= 0)
        {
            if ((carryforward + reserves) < (capital * 0.2))
            {
                reserves += (profit * 0.05); // 5% of the profit is added to the reserves
                return reserves;
            }
            else if ((carryforward + reserves) > (capital * 0.2))
            {
                return 0; // No additional reserves required
            }
        }

        return reserves;
    }

    // Calculates the dividend based on the capital and the desired dividend percentage
    public double CalculateDividend(double dividend, double capital, double profit)
    {
        return (capital / 100) * dividend;
    }
}
