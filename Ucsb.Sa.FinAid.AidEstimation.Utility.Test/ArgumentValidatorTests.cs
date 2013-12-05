using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;

namespace Ucsb.Sa.FinAid.AidEstimation.Utility.Test
{
    [TestClass]
    public class ArgumentValidatorTests
    {
        [TestMethod]
        public void ValidatePositiveMoneyValue_ValidInput_ParsedValue()
        {
            ArgumentValidator validator = new ArgumentValidator();
            double result = validator.ValidatePositiveMoneyValue("3", "test", "Test");
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void ValidatePositiveMoneyValue_NullInput_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidatePositiveMoneyValue(null, "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidatePositiveMoneyValue_EmptyInput_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidatePositiveMoneyValue(null, "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidatePositiveMoneyValue_BadValue_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidatePositiveMoneyValue("G", "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidatePositiveMoneyValue_OverMax_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidatePositiveMoneyValue("9999999999", "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidatePositiveMoneyValue_Negative_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidatePositiveMoneyValue("-1", "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidatePositiveMoneyValue_NullDisplayName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidatePositiveMoneyValue("3", null, "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidatePositiveMoneyValue_EmptyDisplayName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidatePositiveMoneyValue("3", String.Empty, "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidatePositiveMoneyValue_NullParamName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidatePositiveMoneyValue("3", "test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidatePositiveMoneyValue_EmptyParamName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidatePositiveMoneyValue("3", "test", String.Empty);
        }

        [TestMethod]
        public void ValidateMoneyValue_ValidInput_ParsedValue()
        {
            ArgumentValidator validator = new ArgumentValidator();
            double result = validator.ValidateMoneyValue("-3", "test", "Test");
            Assert.AreEqual(-3, result);
        }

        [TestMethod]
        public void ValidateMoneyValue_NullInput_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMoneyValue(null, "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateMoneyValue_EmptyInput_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMoneyValue(null, "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateMoneyValue_BadValue_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMoneyValue("G", "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateMoneyValue_OverMax_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMoneyValue("9999999999", "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateMoneyValue_UnderMin_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMoneyValue("-9999999999", "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateMoneyValue_NullDisplayName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMoneyValue("3", null, "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateMoneyValue_EmptyDisplayName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMoneyValue("3", String.Empty, "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateMoneyValue_NullParamName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMoneyValue("3", "test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateMoneyValue_EmptyParamName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMoneyValue("3", "test", String.Empty);
        }

        // NEW

        [TestMethod]
        public void ValidateNonZeroInteger_ValidInput_ParsedValue()
        {
            ArgumentValidator validator = new ArgumentValidator();
            int result = validator.ValidateNonZeroInteger("3", "test", "Test");
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void ValidateNonZeroInteger_NullInput_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateNonZeroInteger(null, "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateNonZeroInteger_EmptyInput_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateNonZeroInteger(null, "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateNonZeroInteger_BadValue_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateNonZeroInteger("G", "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateNonZeroInteger_Zero_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateNonZeroInteger("0", "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateNonZeroInteger_Negative_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateNonZeroInteger("-99", "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateNonZeroInteger_NullDisplayName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateNonZeroInteger("3", null, "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateNonZeroInteger_EmptyDisplayName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateNonZeroInteger("3", String.Empty, "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateNonZeroInteger_NullParamName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateNonZeroInteger("3", "test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateNonZeroInteger_EmptyParamName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateNonZeroInteger("3", "test", String.Empty);
        }

        // NEW NEW

        [TestMethod]
        public void ValidateMaritalStatus_Single_ParsedValue()
        {
            ArgumentValidator validator = new ArgumentValidator();
            MaritalStatus result = validator.ValidateMaritalStatus("single", "test", "Test");
            Assert.AreEqual(MaritalStatus.SingleSeparatedDivorced, result);
        }

        [TestMethod]
        public void ValidateMaritalStatus_Married_ParsedValue()
        {
            ArgumentValidator validator = new ArgumentValidator();
            MaritalStatus result = validator.ValidateMaritalStatus("married", "test", "Test");
            Assert.AreEqual(MaritalStatus.MarriedRemarried, result);
        }

        [TestMethod]
        public void ValidateMaritalStatus_VariedCase_ParsedValue()
        {
            ArgumentValidator validator = new ArgumentValidator();
            MaritalStatus result = validator.ValidateMaritalStatus("mArRiEd", "test", "Test");
            Assert.AreEqual(MaritalStatus.MarriedRemarried, result);
        }

        [TestMethod]
        public void ValidateMaritalStatus_NullInput_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMaritalStatus(null, "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateMaritalStatus_EmptyInput_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMaritalStatus(null, "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateMaritalStatus_BadValue_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMaritalStatus("G", "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateMaritalStatus_NullDisplayName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMaritalStatus("3", null, "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateMaritalStatus_EmptyDisplayName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMaritalStatus("3", String.Empty, "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateMaritalStatus_NullParamName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMaritalStatus("3", "test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateMaritalStatus_EmptyParamName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateMaritalStatus("3", "test", String.Empty);
        }

        // NEW NEW NEW

        [TestMethod]
        public void ValidateUnitedStatesStateOrTerritory_State_ParsedValue()
        {
            ArgumentValidator validator = new ArgumentValidator();
            UnitedStatesStateOrTerritory result = validator.ValidateUnitedStatesStateOrTerritory("minnesota", "test", "Test");
            Assert.AreEqual(UnitedStatesStateOrTerritory.Minnesota, result);
        }

        [TestMethod]
        public void ValidateUnitedStatesStateOrTerritory_VariedCase_ParsedValue()
        {
            ArgumentValidator validator = new ArgumentValidator();
            UnitedStatesStateOrTerritory result = validator.ValidateUnitedStatesStateOrTerritory("cAlIfOrNiA", "test", "Test");
            Assert.AreEqual(UnitedStatesStateOrTerritory.California, result);
        }

        [TestMethod]
        public void ValidateUnitedStatesStateOrTerritory_NullInput_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateUnitedStatesStateOrTerritory(null, "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateUnitedStatesStateOrTerritory_EmptyInput_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateUnitedStatesStateOrTerritory(null, "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        public void ValidateUnitedStatesStateOrTerritory_BadValue_ReturnsError()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateUnitedStatesStateOrTerritory("G", "test", "Test");
            Assert.AreEqual(1, validator.Errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateUnitedStatesStateOrTerritory_NullDisplayName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateUnitedStatesStateOrTerritory("3", null, "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateUnitedStatesStateOrTerritory_EmptyDisplayName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateUnitedStatesStateOrTerritory("3", String.Empty, "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateUnitedStatesStateOrTerritory_NullParamName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateUnitedStatesStateOrTerritory("3", "test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateUnitedStatesStateOrTerritory_EmptyParamName_ThrowsException()
        {
            ArgumentValidator validator = new ArgumentValidator();
            validator.ValidateUnitedStatesStateOrTerritory("3", "test", String.Empty);
        }
    }
}
