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
            string input = "100000";

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
        public void ValidateRequiredField_NonEmptyString_ReturnsTrue()
        {
            // Arrange
            string input = "100000";

            // Act
            bool result = inputValidation.ValidateRequiredField(input);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateRequiredField_EmptyString_ReturnsFalse()
        {
            // Arrange
            string input = "";

            // Act
            bool result = inputValidation.ValidateRequiredField(input);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateRange_WithinRange_ReturnsTrue()
        {
            // Arrange
            double input = 5;
            double min = 0;
            double max = 10;

            // Act
            bool result = inputValidation.ValidateRange(input, min, max);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateRange_OutOfRange_ReturnsFalse()
        {
            // Arrange
            double input = 15;
            double min = 0;
            double max = 10;

            // Act
            bool result = inputValidation.ValidateRange(input, min, max);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
