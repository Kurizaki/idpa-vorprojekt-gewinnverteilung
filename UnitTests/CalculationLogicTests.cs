// CalculationLogicTests.cs
using idpa_vorprojekt_gewinnverteilung.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CalculationLogicTests
    {
        private CalculationLogic calculationLogic;

        [TestInitialize]
        public void Setup()
        {
            calculationLogic = new CalculationLogic();
        }

        [TestMethod]
        public void ValidCalculation_CorrectValues_CorrectResultsDisplayed()
        {
            // Arrange
            double profit = 100000;
            double capital = 500000;
            double reserves = 20000;

            // Act
            double legalRetainedEarnings = calculationLogic.CalculateLegalRetainedEarnings(profit, capital, reserves);
            double dividend = calculationLogic.CalculateDividend(25000, profit);

            // Assert
            Assert.AreEqual(25000, dividend); // 5% of 500000
        }
    }
}
