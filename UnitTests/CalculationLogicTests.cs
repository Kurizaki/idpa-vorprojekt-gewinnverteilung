using idpa_vorprojekt_gewinnverteilung.Helpers;
namespace UnitTests;

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
        double carryforward = 20000;
        double reserves = 50000;

        // Act
        double legalRetainedEarnings = calculationLogic.CalculateLegalRetainedEarnings(profit, capital, carryforward, reserves);
        double dividend = calculationLogic.CalculateDividend(5, capital, profit);

        // Assert
        Assert.AreEqual(55000, legalRetainedEarnings); // 5% of 100000 added to reserves
        Assert.AreEqual(25000, dividend); // 5% of 500000
    }
}
