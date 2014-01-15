using System;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation
{
    /// <summary>
    /// Contribution From Assets calculator
    /// </summary>
    public class AssetContributionCalculator
    {
        private readonly AssetContributionCalculatorConstants _constants;

        /// <summary>
        /// Constructs a new Contribution From Assets calculator
        /// </summary>
        /// <param name="constants">Constants used in the calculation of Contribution From Assets</param>
        public AssetContributionCalculator(AssetContributionCalculatorConstants constants)
        {
            _constants = constants;
        }

        /// <summary>
        /// Calculates Contribution From Assets
        /// </summary>
        /// <param name="role">Role of the subject within the calculation</param>
        /// <param name="maritalStatus">Marital Status</param>
        /// <param name="age">Age</param>
        /// <param name="cashSavingsCheckings">Cash, savings, and checkings value</param>
        /// <param name="investmentNetWorth">Net worth of investments</param>
        /// <param name="businessFarmNetWorth">Net worth of business and/or investment farm</param>
        /// <returns>"Contribution From Assets"</returns>
        public double CalculateContributionFromAssets(
            EfcCalculationRole role,
            MaritalStatus maritalStatus,
            int age,
            double cashSavingsCheckings,
            double investmentNetWorth,
            double businessFarmNetWorth)
        {
            double contributionFromAssets = 0;

            if (role == EfcCalculationRole.DependentStudent)
            {
                contributionFromAssets
                    += CalculateNetWorth(role, cashSavingsCheckings, investmentNetWorth, businessFarmNetWorth);
            }
            else
            {
                contributionFromAssets
                    += CalculateDiscretionaryNetWorth(role, maritalStatus, age, cashSavingsCheckings,
                                                        investmentNetWorth, businessFarmNetWorth);
            }

            double assetRate = 1;

            switch (role)
            {
                case EfcCalculationRole.Parent:
                    assetRate = _constants.DependentParentAssetRate;
                    break;

                case EfcCalculationRole.DependentStudent:
                    assetRate = _constants.DependentStudentAssetRate;
                    break;

                case EfcCalculationRole.IndependentStudentWithDependents:
                    assetRate = _constants.IndependentWithDependentsAssetRate;
                    break;

                case EfcCalculationRole.IndependentStudentWithoutDependents:
                    assetRate = _constants.IndependentWithoutDependentsAssetRate;
                    break;
            }

            contributionFromAssets *= assetRate;

            return Math.Round(contributionFromAssets < 0 ? 0 : contributionFromAssets, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates Discretionary Net Worth contribution
        /// </summary>
        /// <param name="role">Role of the subject within the calculation</param>
        /// <param name="maritalStatus">Marital Status</param>
        /// <param name="age">Age</param>
        /// <param name="cashSavingsCheckings">Cash, savings, and checkings value</param>
        /// <param name="investmentNetWorth">Net worth of investments</param>
        /// <param name="businessFarmNetWorth">Net worth of business and/or investment farm</param>
        /// <returns>"Discretionary Net Worth" contribution</returns>
        public double CalculateDiscretionaryNetWorth(
            EfcCalculationRole role,
            MaritalStatus maritalStatus,
            int age,
            double cashSavingsCheckings,
            double investmentNetWorth,
            double businessFarmNetWorth)
        {
            double discretionaryNetWorth = 0;

            discretionaryNetWorth +=
                CalculateNetWorth(role, cashSavingsCheckings, investmentNetWorth, businessFarmNetWorth);

            discretionaryNetWorth -=
                CalculateAssetProtectionAllowance(maritalStatus, age);

            return Math.Round(discretionaryNetWorth, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates Net Worth contribution
        /// </summary>
        /// <param name="role">Role of the subject within the calculation</param>
        /// <param name="cashSavingsCheckings">Cash, savings, and checkings value</param>
        /// <param name="investmentNetWorth">Net worth of investments</param>
        /// <param name="businessFarmNetWorth">Net worth of business and/or investment farm</param>
        /// <returns>Net Worth contribution</returns>
        public double CalculateNetWorth(
            EfcCalculationRole role,
            double cashSavingsCheckings,
            double investmentNetWorth,
            double businessFarmNetWorth)
        {
            double netWorth = 0;

            netWorth += CalculateCashSavingsCheckingsContribution(cashSavingsCheckings);
            netWorth += CalculateInvestmentNetWorthContribution(investmentNetWorth);
            netWorth += CalculateAdjustedBusinessFarmNetWorthContribution(role, businessFarmNetWorth);

            return Math.Round(netWorth, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates Cash, savings, and checkings contribution
        /// </summary>
        /// <param name="cashSavingsCheckings">Cash, savings, and checkings value</param>
        /// <returns>Cash, savings, and checkings contribution</returns>
        public double CalculateCashSavingsCheckingsContribution(double cashSavingsCheckings)
        {
            return Math.Round(cashSavingsCheckings < 0 ? 0 : cashSavingsCheckings, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates Net worth of investments contribution
        /// </summary>
        /// <param name="investmentNetWorth">Net worth of investments</param>
        /// <returns>Net worth of investments contribution</returns>
        public double CalculateInvestmentNetWorthContribution(double investmentNetWorth)
        {
            return Math.Round(investmentNetWorth < 0 ? 0 : investmentNetWorth, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates the Adjusted Net Worth of Business/Farm contribution
        /// </summary>
        /// <param name="role">Role of the subject within the calculation</param>
        /// <param name="businessFarmNetWorth">Net worth of business and/or investment farm</param>
        /// <returns></returns>
        public double CalculateAdjustedBusinessFarmNetWorthContribution(
            EfcCalculationRole role,
            double businessFarmNetWorth)
        {
            if (role == EfcCalculationRole.DependentStudent)
            {
                return Math.Round(businessFarmNetWorth < 0 ? 0 : businessFarmNetWorth, MidpointRounding.AwayFromZero);
            }

            int baseRange = 0;

            int index = 0;
            int maxIndex = _constants.BusinessFarmNetWorthAdjustmentRanges.Length - 1;

            // Loop through BusinessFarmNetWorthAdjustmentContributionRanges until businessFarmNetWorth
            // param is within range
            for (int i = 0; i < _constants.BusinessFarmNetWorthAdjustmentRanges.Length; i++)
            {
                index = i;

                // If at end of ranges, set baseAmount to maximum range
                if (i == maxIndex)
                {
                    baseRange = _constants.BusinessFarmNetWorthAdjustmentRanges[i];
                    break;
                }

                // If businessFarmNetWorth is within range
                if (businessFarmNetWorth < _constants.BusinessFarmNetWorthAdjustmentRanges[i + 1])
                {
                    // If businessFarmNetWorth is within first range, there is no baseAmount
                    // Otherwise, assign standard baseAmount
                    baseRange = (i == 0) ? 0 : _constants.BusinessFarmNetWorthAdjustmentRanges[i];
                    break;
                }
            }

            // Contribution From AAI = 
            //      (Base Amount for Range)
            //          + (((Business Farm Net Worth) - (Lowest Value of Range)) * (Percent for Range))
            double adjustedBusinessFarmNetWorth =
                _constants.BusinessFarmNetWorthAdjustmentBases[index]
                    + ((businessFarmNetWorth - baseRange)
                    * (_constants.BusinessFarmNetWorthAdjustmentPercents[index] * 0.01));

            return Math.Round((adjustedBusinessFarmNetWorth < 0) ? 0 : adjustedBusinessFarmNetWorth,
                MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates Asset Protection Allowance
        /// </summary>
        /// <param name="maritalStatus">Marital status</param>
        /// <param name="age">Age of subject</param>
        /// <returns>"Asset Protection Allowance"</returns>
        public int CalculateAssetProtectionAllowance(MaritalStatus maritalStatus, int age)
        {
            if (age < _constants.AssetProtectionAllowanceLowestAge)
            {
                return 0;
            }

            int[] assetProtectionAllowances =
                (maritalStatus == MaritalStatus.MarriedRemarried)
                    ? _constants.MarriedAssetProtectionAllowances
                    : _constants.SingleAssetProtectionAllowances;

            int maxIndex = (assetProtectionAllowances.Length - 1);

            if (age > (_constants.AssetProtectionAllowanceLowestAge + maxIndex))
            {
                return assetProtectionAllowances[maxIndex];
            }

            return assetProtectionAllowances[age - _constants.AssetProtectionAllowanceLowestAge];
        }
    }
}