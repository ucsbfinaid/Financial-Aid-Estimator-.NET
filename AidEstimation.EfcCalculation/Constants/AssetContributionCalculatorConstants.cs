namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants
{
    /// <summary>
    /// Constants used in the calculation of the Contribution from Adjusted Available Income (AAI)
    /// </summary>
    public class AssetContributionCalculatorConstants
    {
        /// <summary>
        /// Asset conversion rate for a parent
        /// </summary>
        public double DependentParentAssetRate
        {
            get;
            set;
        }

        /// <summary>
        /// Asset assessment rate for a student
        /// </summary>
        public double DependentStudentAssetRate
        {
            get;
            set;
        }

        /// <summary>
        /// Asset conversion rate for an Independent Student with Dependents
        /// </summary>
        public double IndependentWithDependentsAssetRate
        {
            get;
            set;
        }

        /// <summary>
        /// Asset conversion rate for an Independent Student without Dependents
        /// </summary>
        public double IndependentWithoutDependentsAssetRate
        {
            get;
            set;
        }

        /// <summary>
        /// Lowest age for the "Asset Protection Allowance" values
        /// </summary>
        public int AssetProtectionAllowanceLowestAge
        {
            get;
            set;
        }

        /// <summary>
        /// "Asset Protection Allowance" values for married persons
        /// </summary>
        public int[] MarriedAssetProtectionAllowances
        {
            get;
            set;
        }

        /// <summary>
        /// "Asset Protection Allowance" values for single persons
        /// </summary>
        public int[] SingleAssetProtectionAllowances
        {
            get;
            set;
        }

        /// <summary>
        /// Ranges used in calculating "Adjusted net worth of business/farm"
        /// </summary>
        public int[] BusinessFarmNetWorthAdjustmentRanges
        {
            get;
            set;
        }

        /// <summary>
        /// Base values used in calculating "Adjusted net worth of business/farm"
        /// </summary>
        public int[] BusinessFarmNetWorthAdjustmentBases
        {
            get;
            set;
        }

        /// <summary>
        /// Percentages used in calculating "Adjusted net worth of business/farm"
        /// </summary>
        public double[] BusinessFarmNetWorthAdjustmentPercents
        {
            get;
            set;
        }
    }
}