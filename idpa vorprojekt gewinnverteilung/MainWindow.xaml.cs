using idpa_vorprojekt_gewinnverteilung.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace idpa_vorprojekt_gewinnverteilung
{
    public partial class MainWindow : Window
    {
        private InputValidation inputValidation;
        private RemarkManager remarkManager;
        private CalculationLogic calculationLogic;
        private bool annualProfitExplanationShown = false;
        private bool sharesExplanationShown = false;
        private bool reservesExplanationShown = false;
        private bool carryforwardExplanationShown = false;
        private bool dividendExplanationShown = false;


        public string GetRetainedEarningsOutput()
        {
            return RetainedEarningsOutput.Text;
        }

        public string GetDividendOutput()
        {
            return DividendOutput.Text;
        }

        public string GetCarryforwardOutput()
        {
            return CarryforwardOutput.Text;
        }

        public MainWindow()
        {
            InitializeComponent();
            remarkManager = new RemarkManager(RemarksPanel, RemarkDetailPanel, RemarkDetailTitle, RemarkDetailContent);
            inputValidation = new InputValidation(remarkManager);
            calculationLogic = new CalculationLogic();
            remarkManager.AddRemark("Willkommen", "Auf der linken Seite können Sie in den grauen Eingabefeldern die verlangten  Beträge eingeben.Sie müssen zwingend alle Felder korrekt ausfüllen! Wenn dies nicht der Fall ist, erscheint eine Fehlermeldung und eine Bemerkung im mittleren Feld.Wenn Sie alle Daten eingegeben haben, drücken Sie auf den Button Berechnen.Das Programm berechnet dann die korrekte Gewinnverteilung für Ihre Daten und gibt diese dann im Feld auf der rechten Seite aus.");
        }
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            OnSubmitClick(AnnualProfitTextBox.Text, SharesTextBox.Text, ReservesTextBox.Text, CarryforwardTextBox.Text, DividendTextBox.Text);
        }
        private void CloseRemarkDetail_Click(object sender, RoutedEventArgs e)
        {
            RemarkDetailPanel.Visibility = Visibility.Collapsed;
            RemarkDetailPanel.Opacity = 0;
        }
        public void OnSubmitClick(string annualProfit, string shares, string reserves, string carryforward, string dividend)
        {
            var inputs = new string[] { annualProfit, shares, reserves, carryforward, dividend };
            string[] inputNames = { "Jahresgewinn", "Kapital", "Reserven", "Gewinnvortrag", "Dividende" };

            for (int i = 0; i < inputs.Length; i++)
            {
                if (!inputValidation.ValidateNumericInput(inputs[i]))
                {
                    remarkManager.AddRemark("Ungültige Eingabe", $"Bitte geben Sie einen gültigen numerischen Wert für {inputNames[i]} ein.");
                    remarkManager.DisplayRemarks();
                    return;
                }
            }

            if (!double.TryParse(annualProfit, out double profit) ||
                !double.TryParse(shares, out double capital) ||
                !double.TryParse(reserves, out double reservesValue) ||
                !double.TryParse(carryforward, out double carryforwardValue) ||
                !double.TryParse(dividend, out double dividendValue))
            {
                remarkManager.AddRemark("Ungültige Eingabe", "Bitte geben Sie nur gültige numerische Werte ein.");
                remarkManager.DisplayRemarks();
                return;
            }

            if (carryforwardValue < 0)
            {
                try
                {
                    capital = calculationLogic.HandleLossCarryForward(carryforwardValue, capital);
                    profit += carryforwardValue;
                }
                catch (InvalidOperationException ex)
                {
                    remarkManager.AddRemark("Berechnungsfehler", ex.Message);
                    remarkManager.DisplayRemarks();
                    return;
                }
            }

            if (inputValidation.ValidateInputs(profit, capital, reservesValue, dividendValue))
            {
                try
                {
                    double legalReserves = calculationLogic.CalculateLegalRetainedEarnings(profit, capital, reservesValue);
                    double calculatedDividend = calculationLogic.CalculateDividend(dividendValue, profit);
                    double calculatedCarryforward = calculationLogic.CalculateCarryForward(profit, calculatedDividend, legalReserves);

                    DisplayResult(legalReserves, calculatedDividend, calculatedCarryforward);
                }
                catch (ArgumentException ex)
                {
                    remarkManager.AddRemark("Berechnungsfehler", ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    remarkManager.AddRemark("Berechnungsfehler", ex.Message);
                }
            }
            else
            {
                remarkManager.AddRemark("Ungültige Eingabe", "Bitte stellen Sie sicher, dass alle Werte positiv sind und die Dividende ein gültiger Betrag ist.");
            }

            remarkManager.DisplayRemarks();
        }


        private void AnnualProfitTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!annualProfitExplanationShown)
            {
                remarkManager.AddRemark("Erklärung Jahresgewinn", "Hier geben Sie den Jahresgewinn in CHF Ihres Unternehmens an.");
                annualProfitExplanationShown = true;
            }
        }

        private void SharesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!sharesExplanationShown)
            {
                remarkManager.AddRemark("Erklärung Aktienkapital", "Hier geben Sie das Aktien- oder Partizipationskapital in CHF an. Dieses Kapital stellt den Grundwert der Anteile des Unternehmens dar.");
                sharesExplanationShown = true;
            }
        }

        private void ReservesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!reservesExplanationShown)
            {
                remarkManager.AddRemark("Erklärung Gesetzliche Reserven", "Hier geben Sie die aktuellen gesetzlichen Reserven in CHF an. Gesetzliche Reserven sind Rücklagen, die gesetzlich vorgeschrieben sind, um finanzielle Stabilität zu gewährleisten.");
                reservesExplanationShown = true;
            }
        }

        private void CarryforwardTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!carryforwardExplanationShown)
            {
                remarkManager.AddRemark("Erklärung Gewinn-/Verlustvortrag", "Hier geben Sie den Gewinn- oder Verlustvortrag aus dem Vorjahr in CHF an. Dies ist der Betrag, der in das neue Geschäftsjahr übertragen wurde.");
                carryforwardExplanationShown = true;
            }
        }

        private void DividendTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!dividendExplanationShown)
            {
                remarkManager.AddRemark("Erklärung Gewünschte Dividende", "Hier geben Sie den gewünschten Dividendenbetrag in CHF an, den Sie an die Aktionäre ausschütten möchten.");
                dividendExplanationShown = true;
            }
        }

        private void DisplayResult(double legalReserves, double calculatedDividend, double carryforward)
        {
            RetainedEarningsOutput.Text = $"{legalReserves:F2} CHF";
            DividendOutput.Text = $"{calculatedDividend:F2} CHF";
            CarryforwardOutput.Text = $"{carryforward:F2} CHF";

            string explanation = $"Berechnung abgeschlossen: \n" +
                                 $"- Gesetzliche Reserven: {legalReserves:F2} CHF\n" +
                                 $"- Ausschüttbare Dividende: {calculatedDividend:F2} CHF\n" +
                                 $"- Gewinn-/Verlustvortrag: {carryforward:F2} CHF\n" +
                                 "\nDetails zur Berechnung:\n" +
                                 $"- Jahresgewinn (nach Verlust): {GetAnnualProfit()} CHF\n" +
                                 $"- Kapital: {GetShares()} CHF\n" +
                                 $"- Reserven vor Berechnung: {GetReserves()} CHF\n" +
                                 $"- Dividendenanforderung: {GetDividend()} CHF\n" +
                                 $"- Gewinn-/Verlustvortrag: {GetCarryforward()} CHF";

            remarkManager.AddRemark("Berechnungserklärung", explanation);
            remarkManager.DisplayRemarks();
        }

        public string GetAnnualProfit()
        {
            return AnnualProfitTextBox.Text;
        }

        public string GetShares()
        {
            return SharesTextBox.Text;
        }

        public string GetReserves()
        {
            return ReservesTextBox.Text;
        }

        public string GetDividend()
        {
            return DividendTextBox.Text;
        }

        public string GetCarryforward()
        {
            return CarryforwardTextBox.Text;
        }
    }
}
