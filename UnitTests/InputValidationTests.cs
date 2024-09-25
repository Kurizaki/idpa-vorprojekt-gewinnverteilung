using idpa_vorprojekt_gewinnverteilung.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class InputValidationTests
    {
        private InputValidation inputValidation;

        [TestInitialize]
        public void Setup()
        {
            inputValidation = new InputValidation();
        }

        [TestMethod]
        public void ValidateNumericInput_ValidNumber_ReturnsTrue()
        {
            // Arrange
            string input = "123.45";

            // Act
            bool result = inputValidation.ValidateNumericInput(input);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateNumericInput_InvalidNumber_ReturnsFalse()
        {
            // Arrange
            string input = "abc";

            // Act
            bool result = inputValidation.ValidateNumericInput(input);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidatePositiveValue_PositiveNumber_ReturnsTrue()
        {
            // Arrange
            double input = 123.45;

            // Act
            bool result = inputValidation.ValidatePositiveValue(input);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidatePositiveValue_NegativeNumber_ReturnsFalse()
        {
            // Arrange
            double input = -123.45;

            // Act
            bool result = inputValidation.ValidatePositiveValue(input);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateInputs_ValidInputs_ReturnsTrue()
        {
            // Arrange
            double profit = 100000;
            double capital = 500000;
            double reserves = 20000;
            double dividend = 5;

            // Act
            bool result = inputValidation.ValidateInputs(profit, capital, reserves, dividend);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateInputs_InvalidInputs_ReturnsFalse()
        {
            // Arrange
            double profit = -100000;
            double capital = 0;
            double reserves = -20000;
            double dividend = 500;

            // Act
            bool result = inputValidation.ValidateInputs(profit, capital, reserves, dividend);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
