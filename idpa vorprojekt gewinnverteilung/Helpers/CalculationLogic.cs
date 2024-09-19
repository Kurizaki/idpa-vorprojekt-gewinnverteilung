namespace idpa_vorprojekt_gewinnverteilung.Helpers;

public class CalculationLogic
{
    public double CalculateLegalRetainedEarnings(double profit, double capital, double carryforward, double reserves)
    {
        while (carryforward < 0)
        {
            capital += carryforward;
            carryforward = 0;
        }

        if (carryforward >= 0)
        {
            if ((carryforward + reserves) < (capital * 0.2))
            {
                reserves += (profit * 0.05);
                return reserves;
            }
            else if ((carryforward + reserves) > (capital * 0.2))
            {
                return 0;
            }
        }

        return reserves;
    }

    public double CalculateDividend(double dividend, double capital, double profit)
    {
        return (capital / 100) * dividend;
    }
}
