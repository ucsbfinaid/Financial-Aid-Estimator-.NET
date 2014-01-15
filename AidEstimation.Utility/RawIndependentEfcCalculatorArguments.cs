namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    public class RawIndependentEfcCalculatorArguments
    {
        // Work Income Values

        public string IsStudentWorking { get; set; }
        public string StudentWorkIncome { get; set; }
        public string IsSpouseWorking { get; set; }
        public string SpouseWorkIncome { get; set; }

        // General Income Values

        public string StudentAgi { get; set; }
        public string IsStudentTaxFiler { get; set; }
        public string StudentIncomeTax { get; set; }
        public string StudentUntaxedIncomeAndBenefits { get; set; }
        public string StudentAdditionalFinancialInfo { get; set; }

        // Asset Values

        public string StudentCashSavingsChecking { get; set; }
        public string StudentInvestmentNetWorth { get; set; }
        public string StudentBusinessFarmNetWorth { get; set; }
        public string HasDependents { get; set; }

        // General Info

        public string MaritalStatus { get; set; }
        public string StateOfResidency { get; set; }
        public string NumberInHousehold { get; set; }
        public string NumberInCollege { get; set; }
        public string StudentAge { get; set; }
        public string IsQualifiedForSimplified { get; set; }
        public string MonthsOfEnrollment { get; set; }
    }
}
