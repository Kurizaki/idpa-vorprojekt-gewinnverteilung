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

        public string RetainedEarningsOutputText
        {
            get { return RetainedEarningsOutput.Text; }
        }

        public string DividendOutputText
        {
            get { return DividendOutput.Text; }
        }

        public string CarryforwardOutputText
        {
            get { return CarryforwardOutput.Text; }
        }

        public MainWindow()
        {
            InitializeComponent();
            inputValidation = new InputValidation();
            remarkManager = new RemarkManager(RemarksPanel, RemarkDetailPanel, RemarkDetailTitle, RemarkDetailContent);
            calculationLogic = new CalculationLogic();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            OnSubmitClick(AnnualProfitTextBox.Text, SharesTextBox.Text, ReservesTextBox.Text, CarryforwardTextBox.Text, DividendTextBox.Text);
        }

        public void OnSubmitClick(string annualProfit, string shares, string reserves, string carryforward, string dividend)
        {
            if (inputValidation.ValidateNumericInput(annualProfit) &&
                inputValidation.ValidateNumericInput(shares) &&
                inputValidation.ValidateNumericInput(reserves) &&
                inputValidation.ValidateNumericInput(carryforward) &&
                inputValidation.ValidateNumericInput(dividend))
            {
                double profit = double.Parse(annualProfit);
                double capital = double.Parse(shares);
                double reservesValue = double.Parse(reserves);
                double carryforwardValue = double.Parse(carryforward);
                double dividendValue = double.Parse(dividend);

                double legalRetainedEarnings = calculationLogic.CalculateLegalRetainedEarnings(profit, capital, carryforwardValue, reservesValue);
                double calculatedDividend = calculationLogic.CalculateDividend(dividendValue, capital, profit);

                DisplayResult(legalRetainedEarnings, calculatedDividend, carryforwardValue);
                remarkManager.DisplayRemarks();
            }
            else
            {
                remarkManager.AddRemark("Invalid Input", "Please enter valid numeric values.");
                remarkManager.DisplayRemarks();
            }
        }

        private void DisplayResult(double legalRetainedEarnings, double calculatedDividend, double carryforward)
        {
            RetainedEarningsOutput.Text = legalRetainedEarnings.ToString("F2");
            DividendOutput.Text = calculatedDividend.ToString("F2");
            CarryforwardOutput.Text = carryforward.ToString("F2");

            remarkManager.AddRemark("Calculation Completed", "The calculation of legal retained earnings and dividend has been successfully completed.");
        }

        private void CloseRemarkDetail_Click(object sender, RoutedEventArgs e)
        {
            remarkManager.CloseRemarkDetail();
        }
    }
}
