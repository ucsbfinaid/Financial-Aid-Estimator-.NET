using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Test
{
    [TestClass]
    public class ExactIncomeCalculatorTests
    {
        private IncomeCalculator _incomeCalculator;

        [TestInitialize]
        public void Init()
        {
            IncomeCalculatorConstants constants = TestConstantsFactory.GetIncomeCalculatorConstants();
            _incomeCalculator = new IncomeCalculator(constants);
        }

        [TestMethod]
        public void CalculateAdjustedGrossIncome_Value_Calculated()
        {
            double result = _incomeCalculator.CalculateAdjustedGrossIncome(2000);
            Assert.AreEqual(2000, result);
        }

        [TestMethod]
        public void CalculateAdjustedGrossIncome_NegativeValue_EqualsZero()
        {
            double result = _incomeCalculator.CalculateAdjustedGrossIncome(-2000);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateAdjustedGrossIncome_DecimalValue_Rounded()
        {
            double result = _incomeCalculator.CalculateAdjustedGrossIncome(2000.55);
            Assert.AreEqual(2001, result);
        }

        [TestMethod]
        public void CalculateIncomeEarnedFromWork_Value_Calculated()
        {
            double result = _incomeCalculator.CalculateIncomeEarnedFromWork(2000);
            Assert.AreEqual(2000, result);
        }

        [TestMethod]
        public void CalculateIncomeEarnedFromWork_NegativeValue_EqualsZero()
        {
            double result = _incomeCalculator.CalculateIncomeEarnedFromWork(-2000);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateIncomeEarnedFromWork_DecimalValue_Rounded()
        {
            double result = _incomeCalculator.CalculateIncomeEarnedFromWork(2000.55);
            Assert.AreEqual(2001, result);
        }

        [TestMethod]
        public void CalculateTotalUntaxedIncomeAndBenefits_Value_Calculated()
        {
            double result = _incomeCalculator.CalculateTotalUntaxedIncomeAndBenefits(2000);
            Assert.AreEqual(2000, result);
        }

        [TestMethod]
        public void CalculateTotalUntaxedIncomeAndBenefits_NegativeValue_EqualsZero()
        {
            double result = _incomeCalculator.CalculateTotalUntaxedIncomeAndBenefits(-2000);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateTotalUntaxedIncomeAndBenefits_DecimalValue_Rounded()
        {
            double result = _incomeCalculator.CalculateTotalUntaxedIncomeAndBenefits(2000.55);
            Assert.AreEqual(2001, result);
        }

        [TestMethod]
        public void CalculateAdditionalFinancialInformation_Value_Calculated()
        {
            double result = _incomeCalculator.CalculateAdditionalFinancialInformation(2000);
            Assert.AreEqual(2000, result);
        }

        [TestMethod]
        public void CalculateAdditionalFinancialInformation_NegativeValue_EqualsZero()
        {
            double result = _incomeCalculator.CalculateAdditionalFinancialInformation(-2000);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateAdditionalFinancialInformation_DecimalValue_Rounded()
        {
            double result = _incomeCalculator.CalculateAdditionalFinancialInformation(2000.55);
            Assert.AreEqual(2001, result);
        }

        [TestMethod]
        public void CalculateTotalIncome_Values_Calculated()
        {
            double result = _incomeCalculator.CalculateTotalIncome(1000, 2000, true, 3000, 2000);
            Assert.AreEqual(2000, result);
        }
    }
}