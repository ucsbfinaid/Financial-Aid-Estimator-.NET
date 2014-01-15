namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    public partial class AidEstimationValidator
    {
        // Is Working?

        public static string LabelIsFirstParentWorking = @"""Did the First Parent Work?""";
        public static string LabelIsSecondParentWorking = @"""Did the Second Parent Work?""";
        public static string LabelIsStudentWorking = @"""Did the Student Work?""";
        public static string LabelIsSpouseWorking = @"""Did the Spouse Work?""";
        public static string ParamIsFirstParentWorking = "isFirstParentWorking";
        public static string ParamIsSecondParentWorking = "isSecondParentWorking";
        public static string ParamIsStudentWorking = "isStudentWorking";
        public static string ParamIsSpouseWorking = "isSpouseWorking";

        // Work Income

        public static string LabelFirstParentWorkIncome = @"""First Parent's Income Earned From Work""";
        public static string LabelSecondParentWorkIncome = @"""Second Parent's Income Earned From Work""";
        public static string LabelStudentWorkIncome = @"""Student's Income Earned From Work""";
        public static string LabelSpouseWorkIncome = @"""Spouse's Income Earned From Work""";
        public static string ParamFirstParentWorkIncome = "firstParentWorkIncome";
        public static string ParamSecondParentWorkIncome = "secondParentWorkIncome";
        public static string ParamStudentWorkIncome = "studentWorkIncome";
        public static string ParamSpouseWorkIncome = "spouseWorkIncome";

        // AGI

        public static string LabelParentAgi = @"""Parent(s)' Adjusted Gross Income (AGI)""";
        public static string LabelStudentAgi = @"""Student's Adjusted Gross Income (AGI)""";
        public static string LabelIndStudentAgi = @"""Student and Spouse's Adjusted Gross Income (AGI)""";
        public static string ParamParentAgi = "parentAgi";
        public static string ParamStudentAgi = "studentAgi";
        public static string ParamIndStudentAgi = "studentAgi";

        // Are Tax Filers?

        public static string LabelAreParentsTaxFilers = @"""Did Parent(s) File Taxes?""";
        public static string LabelIsStudentTaxFiler = @"""Did the Student File Taxes?""";
        public static string LabelIsIndStudentTaxFiler = @"""Did the Student and Spouse File Taxes?""";
        public static string ParamAreParentsTaxFilers = "areParentsTaxFilers";
        public static string ParamIsStudentTaxFiler = "isStudentTaxFiler";
        public static string ParamIsIndStudentTaxFiler = "isStudentTaxFiler";

        // Income Tax Paid

        public static string LabelParentIncomeTax = @"""Parent(s)' Income Taxes Paid""";
        public static string LabelStudentIncomeTax = @"""Student's Income Taxes Paid""";
        public static string LabelIndStudentIncomeTax = @"""Student and Spouse's Total Income Taxes Paid""";
        public static string ParamParentIncomeTax = "parentIncomeTax";
        public static string ParamStudentIncomeTax = "studentIncomeTax";
        public static string ParamIndStudentIncomeTax = "studentIncomeTax";

        // Untaxed Income And Benefits

        public static string LabelParentUntaxedIncomeAndBenefits = @"""Parent(s)' Untaxed Income and Benefits""";
        public static string LabelStudentUntaxedIncomeAndBenefits = @"""Student's Untaxed Income and Benefits""";
        public static string LabelIndStudentUntaxedIncomeAndBenefits
            = @"""Student and Spouse's Untaxed Income and Benefits""";
        public static string ParamParentUntaxedIncomeAndBenefits = "parentUntaxedIncomeAndBenefits";
        public static string ParamStudentUntaxedIncomeAndBenefits = "studentUntaxedIncomeAndBenefits";
        public static string ParamIndStudentUntaxedIncomeAndBenefits = "studentUntaxedIncomeAndBenefits";

        // Additional Financial Information

        public static string LabelParentAdditionalFinancialInfo = @"""Parent(s)' Additional Financial Information""";
        public static string LabelStudentAdditionalFinancialInfo = @"""Student's Additional Financial Information""";
        public static string LabelIndStudentAdditionalFinancialInfo = @"""Student and Spouse's Additional Financial Information""";
        public static string ParamParentAdditionalFinancialInfo = "parentAdditionalFinancialInfo";
        public static string ParamStudentAdditionalFinancialInfo = "studentAdditionalFinancialInfo";
        public static string ParamIndStudentAdditionalFinancialInfo = "studentAdditionalFinancialInfo";

        // Cash, Savings, Checking

        public static string LabelParentCashSavingsChecking = @"""Parent(s)' Cash, Savings, and Checking""";
        public static string LabelStudentCashSavingsChecking = @"""Student's Cash, Savings, and Checking""";
        public static string LabelIndStudentCashSavingsChecking
            = @"""Student and Spouse's Cash, Savings, and Checking""";
        public static string ParamParentCashSavingsChecking = "parentCashSavingsChecking";
        public static string ParamStudentCashSavingsChecking = "studentCashSavingsChecking";
        public static string ParamIndStudentCashSavingsChecking = "studentCashSavingsChecking";

        // Investment Net Worth

        public static string LabelParentInvestmentNetWorth = @"""Net Worth of Parent(s)' Investments""";
        public static string LabelStudentInvestmentNetWorth = @"""Net Worth of Student's Investments""";
        public static string LabelIndStudentInvestmentNetWorth = @"""Net Worth of Student and Spouse's Investments""";
        public static string ParamParentInvestmentNetWorth = "parentInvestmentNetWorth";
        public static string ParamStudentInvestmentNetWorth = "studentInvestmentNetWorth";
        public static string ParamIndStudentInvestmentNetWorth = "studentInvestmentNetWorth";

        // Business Farm Net Worth

        public static string LabelParentBusinessFarmNetWorth = @"""Net Worth of Parent(s)' Business and/or Investment Farm""";
        public static string LabelStudentBusinessFarmNetWorth = @"""Net Worth of Student's Business and/or Investment Farm""";
        public static string LabelIndStudentBusinessFarmNetWorth = @"""Net Worth of Student and Spouse's Business and/or Investment Farm""";
        public static string ParamParentBusinessFarmNetWorth = "parentBusinessFarmNetWorth";
        public static string ParamStudentBusinessFarmNetWorth = "studentBusinessFarmNetWorth";
        public static string ParamIndStudentBusinessFarmNetWorth = "studentBusinessFarmNetWorth";

        // Has Dependents

        public static string LabelIndStudentHasDep = @"""Student has Dependents""";
        public static string ParamIndStudentHasDep = "hasDependents";

        // Marital Status

        public static string ParamMaritalStatus = "maritalStatus";
        public static string LabelParentMaritalStatus = @"""Parent(s)' Marital Status""";
        public static string LabelIndStudentMaritalStatus = @"""Student's Marital Status""";

        // State of Residency

        public static string LabelStateOfResidency = @"""State of Residency""";
        public static string ParamStateOfResidency = "stateOfResidency";

        // Number in Household

        public static string LabelNumInHousehold = @"""Number in Household""";
        public static string ParamNumInHousehold = "numberInHousehold";

        // Number in College

        public static string LabelNumInCollege = @"""Number in College""";
        public static string ParamNumInCollege = "numberInCollege";

        // Age

        public static string LabelOldestParentAge = @"""Age of Oldest Parent""";
        public static string LabelIndStudentAge = @"""Student's Age""";
        public static string ParamOldestParentAge = "oldestParentAge";
        public static string ParamIndStudentAge = "studentAge";

        // Months of Enrollment

        public static string LabelMonthsOfEnrollment = @"""Months of Enrollment""";
        public static string ParamMonthsOfEnrollment = "monthsOfEnrollment";

        // Is Qualified for Simplified?

        public static string LabelIsQualifiedForSimplified = @"""Is Qualified for Simplified?""";
        public static string ParamIsQualifiedForSimplified = "isQualifiedForSimplified";
    }
}
