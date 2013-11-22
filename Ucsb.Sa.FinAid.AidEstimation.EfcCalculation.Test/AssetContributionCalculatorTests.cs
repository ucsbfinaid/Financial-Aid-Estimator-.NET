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
            AssetContributionCalculatorConstants constants = new AssetContributionCalculatorConstants();

            constants.DependentParentAssetRate = 0.12;
            constants.DependentStudentAssetRate = 0.2;
            constants.IndependentWithDependentsAssetRate = 0.07;
            constants.IndependentWithoutDependentsAssetRate = 0.2;

            constants.AssetProtectionAllowanceLowestAge = 25;
            constants.MarriedAssetProtectionAllowances = new[]
            {
                0, 2100, 4300, 6400, 8600, 10700, 12800, 15000, 17100, 19300,
                21400, 23500, 25700, 27800, 30000, 32100, 32900, 33700, 34500,
                35400, 36200, 37100, 38000, 39000, 39900, 40900, 42100, 43100,
                44200, 45500, 46800, 47900, 49300, 50800, 52200, 53500, 55000,
                56900, 58500, 60100, 61800,
            };
            constants.SingleAssetProtectionAllowances = new[]
            {
                0, 600, 1300, 1900, 2500, 3200, 3800, 4400, 5100, 5700, 6300,
                7000, 7600, 8200, 8900, 9500, 9700, 9900, 10100, 10300, 10600,
                10800, 11100, 11300, 11600, 11900, 12200, 12500, 12800, 13100,
                13400, 13700, 14100, 14400, 14800, 15100, 15600, 16000, 16400,
                16900, 17400
            };

            constants.BusinessFarmNetWorthAdjustmentRanges = new[] { 1, 120000, 365000, 610000 };
            constants.BusinessFarmNetWorthAdjustmentBases = new[] { 0, 48000, 170500, 317500 };
            constants.BusinessFarmNetWorthAdjustmentPercents = new double[] { 40, 50, 60, 100 };

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
            Assert.AreEqual(89101, result);
        }

        [TestMethod]
        public void CalculateAdjustedBusinessFarmNetWorthContribution_HighValue_Calculated()
        {
            double result = _calculator
                .CalculateAdjustedBusinessFarmNetWorthContribution(EfcCalculationRole.Parent, 202202202);
            Assert.AreEqual(201909702, result);
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
            Assert.AreEqual(10700, result);
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
            Assert.AreEqual(61800, result);
        }

        [TestMethod]
        public void CalculateAssetProtectionAllowance_SingleParent_Calculated()
        {
            double result = _calculator.CalculateAssetProtectionAllowance(MaritalStatus.SingleSeparatedDivorced, 45);
            Assert.AreEqual(10600, result);
        }

        [TestMethod]
        public void CalculateDiscretionaryNetWorth_Values_Calculated()
        {
            double result = _calculator
                .CalculateDiscretionaryNetWorth(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, 45, 6000, 26000, 9000);
            Assert.AreEqual(-600, result);
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
                .CalculateContributionFromAssets(EfcCalculationRole.Parent, MaritalStatus.MarriedRemarried, 45, 6000, 26000, 9000);
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
            Assert.AreEqual(4980, result);
        }

        [TestMethod]
        public void CalculateContributionFromAssets_IndependentStudentWithDep_Calculated()
        {
            double result = _calculator
                .CalculateContributionFromAssets(EfcCalculationRole.IndependentStudentWithDependents, MaritalStatus.MarriedRemarried, 30, 6000, 26000, 9000);
            Assert.AreEqual(1743, result);
        }
    }
}