namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments
{
    /// <summary>
    /// Parameters used in the calculation of a dependent student's Expected Family Contribution (EFC)
    /// </summary>
    public class DependentEfcCalculatorArguments
    {
        /// <summary>
        /// First parent
        /// </summary>
        public HouseholdMember FirstParent
        {
            get;
            set;
        }

        /// <summary>
        /// Second parent
        /// </summary>
        public HouseholdMember SecondParent
        {
            get;
            set;
        }

        /// <summary>
        /// Student
        /// </summary>
        public HouseholdMember Student
        {
            get;
            set;
        }

        /// <summary>
        /// Parent's Adjusted Gross Income (AGI)
        /// </summary>
        public double ParentAdjustedGrossIncome
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the parents are required to file taxes
        /// </summary>
        public bool AreParentsTaxFilers
        {
            get;
            set;
        }

        /// <summary>
        /// Parent's U.S. income tax paid
        /// </summary>
        public double ParentIncomeTaxPaid
        {
            get;
            set;
        }

        /// <summary>
        /// Parent's total untaxed income and benefits
        /// </summary>
        public double ParentUntaxedIncomeAndBenefits
        {
            get;
            set;
        }

        /// <summary>
        /// Parent's total additional financial information
        /// </summary>
        public double ParentAdditionalFinancialInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Student's Adjusted Gross Income (AGI)
        /// </summary>
        public double StudentAdjustedGrossIncome
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the student is required to file taxes
        /// </summary>
        public bool IsStudentTaxFiler
        {
            get;
            set;
        }

        /// <summary>
        /// Student's U.S. income tax paid
        /// </summary>
        public double StudentIncomeTaxPaid
        {
            get;
            set;
        }

        /// <summary>
        /// Student's total untaxed income and benefits
        /// </summary>
        public double StudentUntaxedIncomeAndBenefits
        {
            get;
            set;
        }

        /// <summary>
        /// Student's total additional financial information
        /// </summary>
        public double StudentAdditionalFinancialInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Parent's total cash, savings, and checkings
        /// </summary>
        public double ParentCashSavingsChecking
        {
            get;
            set;
        }

        /// <summary>
        /// Parent's net worth of investments
        /// </summary>
        public double ParentInvestmentNetWorth
        {
            get;
            set;
        }

        /// <summary>
        /// Parent's net worth of business and/or investment farm
        /// </summary>
        public double ParentBusinessFarmNetWorth
        {
            get;
            set;
        }

        /// <summary>
        /// Student's total cash, savings, and checking
        /// </summary>
        public double StudentCashSavingsChecking
        {
            get;
            set;
        }

        /// <summary>
        /// Student's net worth of investments
        /// </summary>
        public double StudentInvestmentNetWorth
        {
            get;
            set;
        }

        /// <summary>
        /// Student's net worth of business and/or investment farm
        /// </summary>
        public double StudentBusinessFarmNetWorth
        {
            get;
            set;
        }

        /// <summary>
        /// Parent's marital status
        /// </summary>
        public MaritalStatus MaritalStatus
        {
            get;
            set;
        }

        /// <summary>
        /// Student's state of residency
        /// </summary>
        public UnitedStatesStateOrTerritory StateOfResidency
        {
            get;
            set;
        }

        /// <summary>
        /// Number in the household
        /// </summary>
        public int NumberInHousehold
        {
            get;
            set;
        }

        /// <summary>
        /// Number of people in the household that are in college
        /// </summary>
        public int NumberInCollege
        {
            get;
            set;
        }

        /// <summary>
        /// Age of the oldest parent
        /// </summary>
        public int OldestParentAge
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the student qualifies for the simplified EFC calculation
        /// </summary>
        public bool IsQualifiedForSimplified
        {
            get;
            set;
        }

        /// <summary>
        /// Months that student will be enrolled in college
        /// </summary>
        public int MonthsOfEnrollment
        {
            get;
            set;
        }
    }
}