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

        public MainWindow()
        {
            InitializeComponent();
            inputValidation = new InputValidation();
            remarkManager = new RemarkManager(RemarksPanel, RemarkDetailPanel, RemarkDetailTitle, RemarkDetailContent);
            calculationLogic = new CalculationLogic();
        }

        private void BerechnenButton_Click(object sender, RoutedEventArgs e)
        {
            OnSubmitClick(JahresgewinnTextBox.Text, AktienTextBox.Text, ReservenTextBox.Text, GewinnVortragTextBox.Text, DividendeTextBox.Text);
        }

        public void OnSubmitClick(string jahresgewinn, string aktien, string reserven, string gewinnVortrag, string dividende)
        {
            if (inputValidation.ValidateNumericInput(jahresgewinn) &&
                inputValidation.ValidateNumericInput(aktien) &&
                inputValidation.ValidateNumericInput(reserven) &&
                inputValidation.ValidateNumericInput(gewinnVortrag) &&
                inputValidation.ValidateNumericInput(dividende))
            {
                double profit = double.Parse(jahresgewinn);
                double capital = double.Parse(aktien);
                double reserves = double.Parse(reserven);
                double carryforward = double.Parse(gewinnVortrag);
                double dividend = double.Parse(dividende);

                double legalRetainedEarnings = calculationLogic.CalculateLegalRetainedEarnings(profit, capital, carryforward, reserves);
                double calculatedDividend = calculationLogic.CalculateDividend(dividend, capital, profit);

                DisplayResult(legalRetainedEarnings, calculatedDividend, carryforward);
                remarkManager.DisplayRemarks();
            }
            else
            {
                remarkManager.AddRemark("Ungültige Eingabe", "Bitte geben Sie gültige numerische Werte ein.");
                remarkManager.DisplayRemarks();
            }
        }

        private void DisplayResult(double legalRetainedEarnings, double calculatedDividend, double carryforward)
        {
            GewinnreserveOutput.Text = legalRetainedEarnings.ToString("F2");
            DividendenOutput.Text = calculatedDividend.ToString("F2");
            GewinnVortragOutput.Text = carryforward.ToString("F2");
        }

        private void CloseRemarkDetail_Click(object sender, RoutedEventArgs e)
        {
            remarkManager.CloseRemarkDetail();
        }
    }
}
