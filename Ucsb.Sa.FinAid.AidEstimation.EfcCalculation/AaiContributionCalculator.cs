using System;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation
{
    /// <summary>
    /// Contribution from Adjusted Available Income (AAI) calculator
    /// </summary>
    public class AaiContributionCalculator
    {
        private readonly AaiContributionCalculatorConstants _constants;

        /// <summary>
        /// Constructs a new Contribution from Adjusted Available Income (AAI) calculator
        /// </summary>
        /// <param name="constants">Constants used in the calculation of Contribution from Adjusted Available
        /// Income (AAI)</param>
        public AaiContributionCalculator(AaiContributionCalculatorConstants constants)
        {
            _constants = constants;
        }

        /// <summary>
        /// Calculates Contribution from Adjusted Available Income (AAI)
        /// </summary>
        /// <param name="role">Role of the subject within the calculation</param>
        /// <param name="adjustedAvailableIncome">Adjusted Available Income</param>
        /// <returns>"Contribution from AAI"</returns>
        public double CalculateContributionFromAai(EfcCalculationRole role, double adjustedAvailableIncome)
        {
            if (role == EfcCalculationRole.DependentStudent
                || role == EfcCalculationRole.IndependentStudentWithoutDependents)
            {
                return Math.Round(adjustedAvailableIncome < 0 ? 0 : adjustedAvailableIncome,
                    MidpointRounding.AwayFromZero);
            }

            if (adjustedAvailableIncome < _constants.AaiContributionRanges[0])
            {
                return 0;
            }

            int baseRange = 0;
            int index = 0;

            // Loop through AAIContributionRanges until adjustedAvailableIncome param is within range
            for (int i = 0; i < _constants.AaiContributionRanges.Length; i++)
            {
                index = i;

                // If at end of ranges, set baseAmount to maximum range
                if (i == _constants.AaiContributionRanges.Length - 1)
                {
                    baseRange = _constants.AaiContributionRanges[i];
                    break;
                }

                // If adjustedAvailableIncome is within range
                if (adjustedAvailableIncome < _constants.AaiContributionRanges[i + 1])
                {
                    // If adjustedAvailableIncome is within first range, there is no baseAmount;
                    // otherwise, assign standard baseAmount
                    baseRange = (i == 0) ? 0 : _constants.AaiContributionRanges[i];
                    break;
                }
            }

            // Contribution From AAI = 
            //      (Base Amount for Range)
            //          + (((Adjusted Available Income) - (Lowest Value of Range)) * (Percent for Range))
            double contributionFromAai =
                _constants.AaiContributionBases[index]
                    + ((adjustedAvailableIncome - baseRange) * (_constants.AaiContributionPercents[index] * 0.01));

            return Math.Round((contributionFromAai < 0) ? 0 : contributionFromAai, MidpointRounding.AwayFromZero);
        }
    }
}