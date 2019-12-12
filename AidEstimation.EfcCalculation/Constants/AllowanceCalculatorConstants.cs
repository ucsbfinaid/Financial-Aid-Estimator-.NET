namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants
{
    /// <summary>
    /// Constants used in the calculation of Total Allowances
    /// </summary>
    public class AllowanceCalculatorConstants
    {
        /// <summary>
        /// Threshold value used in determining a State and Other Tax Allowance percentage
        /// </summary>
        public int StateTaxAllowanceIncomeThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// State and Other Tax Allowance percentage values for Parents and Independent Students with Dependents
        /// </summary>
        public int[] ParentStateTaxAllowancePercents
        {
            get;
            set;
        }

        /// <summary>
        /// State and Other Tax Allowance percentage values for Students and Independent Students without Dependents
        /// </summary>
        public int[] StudentStateTaxAllowancePercents
        {
            get;
            set;
        }

        /// <summary>
        /// Threshold values used in calculating Social Security Tax Allowance
        /// </summary>
        public int[] SocialSecurityTaxIncomeThresholds
        {
            get;
            set;
        }

        /// <summary>
        /// Base values used in calculating Social Security Tax Allowance
        /// </summary>
        public double[] SocialSecurityTaxBases
        {
            get;
            set;
        }

        /// <summary>
        /// Percentage values used in calculating Social Security Tax Allowance
        /// </summary>
        public double[] SocialSecurityTaxPercentages
        {
            get;
            set;
        }

        /// <summary>
        /// Percentage used in calculating Employment Expense Allowance
        /// </summary>
        public double EmploymentExpensePercent
        {
            get;
            set;
        }

        /// <summary>
        /// Maximum Employment Expense Allowance value
        /// </summary>
        public double EmploymentExpenseMaximum
        {
            get;
            set;
        }

        /// <summary>
        /// Income Protection Allowance values for Parents
        /// </summary>
        public int[,] DependentParentIncomeProtectionAllowances
        {
            get;
            set;
        }

        /// <summary>
        /// Income Protection Allowance values for Independents with Dependents
        /// </summary>
        public int[,] IndependentWithDependentsIncomeProtectionAllowances
        {
            get;
            set;
        }

        /// <summary>
        /// Value subtracted for additional Number in College when calculating Income Protection Allowance values
        /// for Parents
        /// </summary>
        public int DependentAdditionalStudentAllowance
        {
            get;
            set;
        }

        /// <summary>
        /// Value added for additional Number in Household when calculating Income Protection Allowance values
        /// for Parents
        /// </summary>
        public int DependentAdditionalFamilyAllowance
        {
            get;
            set;
        }

        /// <summary>
        /// Value subtracted for additional Number in College when calculating Income Protection Allowance values
        /// for Independents with Dependents
        /// </summary>
        public int IndependentAdditionalStudentAllowance
        {
            get;
            set;
        }

        /// <summary>
        /// Value added for additional Number in Household when calculating Income Protection Allowance values
        /// for Independents with Dependents
        /// </summary>
        public int IndependentAdditionalFamilyAllowance
        {
            get;
            set;
        }

        /// <summary>
        /// Income Protection Allowance for a single Independent without Dependents
        /// </summary>
        public int SingleIndependentWithoutDependentsIncomeProtectionAllowance
        {
            get;
            set;
        }

        /// <summary>
        /// Income Protection Allowance for a married Independent without Dependents
        /// </summary>
        public int MarriedIndependentWithoutDependentsIncomeProtectionAllowance
        {
            get;
            set;
        }

        /// <summary>
        /// Income Protection Allowance for a Dependent Student
        /// </summary>
        public int DependentStudentIncomeProtectionAllowance
        {
            get;
            set;
        }
    }
}