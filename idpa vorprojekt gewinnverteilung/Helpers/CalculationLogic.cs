namespace idpa_vorprojekt_gewinnverteilung.Helpers
{
    public class CalculationLogic
    {
        // BBeschreibung des Rechenvorgangs
        public double CalculateLegalRetainedEarnings(double profit, double capital, double reserves)
        {
            // Berechnung der erforderlichen Reserven basierend auf den gesetzlichen Anforderungen
            double requiredReserves = capital * 0.2; // 20% des Aktienkapitals

            // Falls die erforderlichen Reserven noch nicht erreicht sind, 5% des Gewinns in die Reserven
            if (reserves < requiredReserves && profit > 0)
            {
                double reserveIncrease = profit * 0.05;
                reserves += reserveIncrease;

                // Rest des Gewinns, der nach der Zuweisung zu den Reserven übrig bleibt
                profit -= reserveIncrease;
            }
            return reserves;
        }

        public double CalculateDividend(double dividendAmount, double profit)
        {
            // Überprüfen, ob der Dividendenbetrag plausibel ist (darf nicht negativ sein)
            if (dividendAmount < 0)
            {
                throw new ArgumentException("Der Dividendenbetrag darf nicht negativ sein.");
            }

            // Überprüfen, ob genügend Gewinn nach der Reservebildung vorhanden ist
            if (dividendAmount > profit)
            {
                throw new InvalidOperationException("Der Gewinn reicht nicht aus, um die gewünschte Dividende auszuschütten.");
            }

            return dividendAmount;
        }

        // Berechnung des Gewinn- oder Verlustvortrags für das nächste Jahr
        public double CalculateCarryForward(double profit, double dividend, double reserves)
        {
            // Berechnung des verbleibenden Gewinns nach Dividende und Reserven
            double carryforward = profit - dividend - reserves;

            // Wenn der Gewinn negativ ist (Verlustvortrag), wird dieser Verlust weitergetragen
            return carryforward;
        }

        // Neue Methode zur Behandlung von Verlustvorträgen, die vor den Berechnungen den Verlust abzieht
        public double HandleLossCarryForward(double loss, double capital)
        {
            // Verlustvortrag wird vom Kapital abgezogen
            capital += loss;

            // Sicherstellen, dass Kapital nicht negativ wird
            if (capital < 0)
            {
                throw new InvalidOperationException("Das Kapital ist nach dem Verlustvortrag negativ.");
            }

            // Geben Sie das angepasste Kapital zurück
            return capital;
        }
    }
}
