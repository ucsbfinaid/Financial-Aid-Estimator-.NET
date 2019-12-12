using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Test
{
    [TestClass]
    public class AssetContributionCalculatorTests
    {
        private AssetContributionCalculator _calculator;

        [TestInitialize]
        public void Init()
        {
            AssetContributionCalculatorConstants constants = TestConstantsFactory.GetAssetContributionCalculatorConstants();
            _calculator = new AssetContributionCalculator(constants);
        }

        [TestMethod]
        public void CalculateCashSavingsCheckingsContribution_Value_Calculated()
        {
            double result = _calculator.CalculateCashSavingsCheckingsContribution(1000);
            Assert.AreEqual(1000, result);
        }

        [TestMethod]
        public void CalculateCashSavingsCheckingsContribution_NegativeValue_EqualsZero()
        {
            double result = _calculator.CalculateCashSavingsCheckingsContribution(-1000);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateCashSavingsCheckingsContribution_DecimalValue_Rounded()
        {
            double result = _calculator.CalculateCashSavingsCheckingsContribution(1000.55);
            Assert.AreEqual(1001, result);
        }

        [TestMethod]
        public void CalculateInvestmentNetWorthContribution_Value_Calculated()
        {
            double result = _calculator.CalculateInvestmentNetWorthContribution(1000);
            Assert.AreEqual(1000, result);
        }

        [TestMethod]
        public void CalculateInvestmentNetWorthContribution_NegativeValue_EqualsZero()
        {
            double result = _calculator.CalculateInvestmentNetWorthContribution(-1000);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateInvestmentNetWorthContribution_DecimalValue_Rounded()
        {
            double result = _calculator.CalculateInvestmentNetWorthContribution(1000.55);
            Assert.AreEqual(1001, result);
        }

        [TestMethod]
        public void CalculateAdjustedBusinessFarmNetWorthContribution_Value_Calculated()
        {
            double result = _calculator
                .CalculateAdjustedBusinessFarmNetWorthContribution(EfcCalculationRole.Parent, 202202);
            Assert.AreEqual(87601, result);
        }

        [TestMethod]
        public void CalculateAdjustedBusinessFarmNetWorthContribution_HighValue_Calculated()
        {
            double result = _calculator
                .CalculateAdjustedBusinessFarmNetWorthContribution(EfcCalculationRole.Parent, 202202202);
            Assert.AreEqual(201875702, result);
        }

        [TestMethod]
        public void CalculateAdjustedBusinessFarmNetWorthContribution_LowValue_Calculated()
        {
            double result = _calculator
                .CalculateAdjustedBusinessFarmNetWorthContribution(EfcCalculationRole.Parent, 1000);
            Assert.AreEqual(400, result);
        }

        [TestMethod]
        public void CalculateAdjustedBusinessFarmNetWorthContribution_NegativeValue_EqualsZero()
        {
            double result = _calculator
                .CalculateAdjustedBusinessFarmNetWorthContribution(EfcCalculationRole.Parent, -1000);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateAdjustedBusinessFarmNetWorthContribution_DecimalValue_Rounded()
        {
            double result = _calculator
                .CalculateAdjustedBusinessFarmNetWorthContribution(EfcCalculationRole.Parent, 80000.66);
            Assert.AreEqual(32000, result);
        }

        [TestMethod]
        public void CalculateAdjustedBusinessFarmNetWorthContribution_DependentStudent_Calculated()
        {
            double result = _calculator
                .CalculateAdjustedBusinessFarmNetWorthContribution(EfcCalculationRole.DependentStudent, 8000);
            Assert.AreEqual(8000, result);
        }

        [TestMethod]
        public void CalculateAdjustedBusinessFarmNetWorthContribution_DependentStudentNegativeValue_EqualsZero()
        {
            double result = _calculator
                .CalculateAdjustedBusinessFarmNetWorthContribution(EfcCalculationRole.DependentStudent, -8000);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateAdjustedBusinessFarmNetWorthContribution_DependentStudentDecimalValue_EqualsZero()
        {
            double result = _calculator
                .CalculateAdjustedBusinessFarmNetWorthContribution(EfcCalculationRole.DependentStudent, 8000.66);
            Assert.AreEqual(8001, result);
        }

        [TestMethod]
        public void CalculateNetWorth_NormalValues_Calculated()
        {
            double result = _calculator
                .CalculateNetWorth(EfcCalculationRole.Parent, 6000, 26000, 9000);
            Assert.AreEqual(35600, result);
        }

        [TestMethod]
        public void CalculateAssetProtectionAllowance_Values_Calculated()
        {
            double result = _calculator.CalculateAssetProtectionAllowance(MaritalStatus.MarriedRemarried, 30);
            Assert.AreEqual(1600, result);
        }

        [TestMethod]
        public void CalculateAssetProtectionAllowance_LowAge_EqualZero()
        {
            double result = _calculator.CalculateAssetProtectionAllowance(MaritalStatus.MarriedRemarried, 20);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateAssetProtectionAllowance_HighAge_Calculated()
        {
            double result = _calculator.CalculateAssetProtectionAllowance(MaritalStatus.MarriedRemarried, 70);
            Assert.AreEqual(9400, result);
        }

        [TestMethod]
        public void CalculateAssetProtectionAllowance_SingleParent_Calculated()
        {
            double result = _calculator.CalculateAssetProtectionAllowance(MaritalStatus.SingleSeparatedDivorced, 45);
            Assert.AreEqual(1900, result);
        }

        [TestMethod]
        public void CalculateDiscretionaryNetWorth_Values_Calculated()
        {
            double result = _calculator
                .CalculateDiscretionaryNetWorth(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, 45, 600, 200, 900);
            Assert.AreEqual(-4340, result);
        }

        [TestMethod]
        public void CalculateContributionFromAssets_Values_Calculated()
        {
            double result = _calculator
                .CalculateContributionFromAssets(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, 25, 6000, 26000, 9000);
            Assert.AreEqual(4272, result);
        }

        [TestMethod]
        public void CalculateContributionFromAssets_NegativeValue_EqualsZero()
        {
            double result = _calculator
                .CalculateContributionFromAssets(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, 45, 600, 200, 900);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateContributionFromAssets_DependentStudent_Calculated()
        {
            double result = _calculator
                .CalculateContributionFromAssets(EfcCalculationRole.DependentStudent, MaritalStatus.MarriedRemarried, 45, 6000, 26000, 9000);
            Assert.AreEqual(8200, result);
        }

        [TestMethod]
        public void CalculateContributionFromAssets_IndependentStudentWithoutDep_Calculated()
        {
            double result = _calculator
                .CalculateContributionFromAssets(EfcCalculationRole.IndependentStudentWithoutDependents, MaritalStatus.MarriedRemarried, 30, 6000, 26000, 9000);
            Assert.AreEqual(6800, result);
        }

        [TestMethod]
        public void CalculateContributionFromAssets_IndependentStudentWithDep_Calculated()
        {
            double result = _calculator
                .CalculateContributionFromAssets(EfcCalculationRole.IndependentStudentWithDependents, MaritalStatus.MarriedRemarried, 30, 6000, 26000, 9000);
            Assert.AreEqual(2380, result);
        }
    }
}