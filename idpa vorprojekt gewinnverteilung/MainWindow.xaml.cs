using idpa_vorprojekt_gewinnverteilung.Helpers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace idpa_vorprojekt_gewinnverteilung
{
    public partial class MainWindow
    {
        private InputValidation inputValidation;
        private RemarkManager remarkManager;
        private CalculationLogic calculationLogic;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnSubmitClick(string a, string b, string c)
        {
        }

        private void DisplayResult(double result)
        {
        }

        private void DisplayRemarks()
        {
        }
    }


}