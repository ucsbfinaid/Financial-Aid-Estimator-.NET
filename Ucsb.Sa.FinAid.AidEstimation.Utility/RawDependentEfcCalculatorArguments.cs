namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    public class RawDependentEfcCalculatorArguments
    {

        // Work Income Values

        public string IsFirstParentWorking { get; set; }
        public string FirstParentWorkIncome { get; set; }
        public string IsSecondParentWorking { get; set; }
        public string SecondParentWorkIncome { get; set; }
        public string IsStudentWorking { get; set; }
        public string StudentWorkIncome { get; set; }


        // General Income Values

        public string ParentAgi { get; set; }
        public string AreParentsTaxFilers { get; set; }
        public string ParentIncomeTax { get; set; }
        public string ParentUntaxedIncomeAndBenefits { get; set; }
        public string ParentAdditionalFinancialInfo { get; set; }
        public string StudentAgi { get; set; }
        public string IsStudentTaxFiler { get; set; }
        public string StudentIncomeTax { get; set; }
        public string StudentUntaxedIncomeAndBenefits { get; set; }
        public string StudentAdditionalFinancialInfo { get; set; }


        // Asset Values

        public string ParentCashSavingsChecking { get; set; }
        public string ParentInvestmentNetWorth { get; set; }
        public string ParentBusinessFarmNetWorth { get; set; }
        public string StudentCashSavingsChecking { get; set; }
        public string StudentInvestmentNetWorth { get; set; }
        public string StudentBusinessFarmNetWorth { get; set; }


        // General Information

        public string MaritalStatus { get; set; }
        public string StateOfResidency { get; set; }
        public string NumberInHousehold { get; set; }
        public string NumberInCollege { get; set; }
        public string OldestParentAge { get; set; }
        public string IsQualifiedForSimplified { get; set; }
        public string MonthsOfEnrollment { get; set; }
    }
}
