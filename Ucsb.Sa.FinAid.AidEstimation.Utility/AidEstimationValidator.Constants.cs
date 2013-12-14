namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    public partial class AidEstimationValidator
    {
        // Is Working?

        private const string LabelIsFirstParentWorking = @"""Did the First Parent Work?""";
        private const string LabelIsSecondParentWorking = @"""Did the Second Parent Work?""";
        private const string LabelIsStudentWorking = @"""Did the Student Work?""";
        private const string LabelIsSpouseWorking = @"""Did the Spouse Work?""";
        private const string ParamIsFirstParentWorking = "isFirstParentWorking";
        private const string ParamIsSecondParentWorking = "isSecondParentWorking";
        private const string ParamIsStudentWorking = "isStudentWorking";
        private const string ParamIsSpouseWorking = "isSpouseWorking";

        // Work Income

        private const string LabelFirstParentWorkIncome = @"""First Parent's Income Earned From Work""";
        private const string LabelSecondParentWorkIncome = @"""Second Parent's Income Earned From Work""";
        private const string LabelStudentWorkIncome = @"""Student's Income Earned From Work""";
        private const string LabelSpouseWorkIncome = @"""Spouse's Income Earned From Work""";
        private const string ParamFirstParentWorkIncome = "firstParentWorkIncome";
        private const string ParamSecondParentWorkIncome = "secondParentWorkIncome";
        private const string ParamStudentWorkIncome = "studentWorkIncome";
        private const string ParamSpouseWorkIncome = "spouseWorkIncome";

        // AGI

        private const string LabelParentAgi = @"""Parent(s)' Adjusted Gross Income (AGI)""";
        private const string LabelStudentAgi = @"""Student's Adjusted Gross Income (AGI)""";
        private const string LabelIndStudentAgi = @"""Student and Spouse's Adjusted Gross Income (AGI)""";
        private const string ParamParentAgi = "parentAgi";
        private const string ParamStudentAgi = "studentAgi";
        private const string ParamIndStudentAgi = "studentAgi";

        // Are Tax Filers?

        private const string LabelAreParentsTaxFilers = @"""Did Parent(s) File Taxes?""";
        private const string LabelIsStudentTaxFiler = @"""Did the Student File Taxes?""";
        private const string LabelIsIndStudentTaxFiler = @"""Did the Student and Spouse File Taxes?""";
        private const string ParamAreParentsTaxFilers = "areParentsTaxFilers";
        private const string ParamIsStudentTaxFiler = "isStudentTaxFiler";
        private const string ParamIsIndStudentTaxFiler = "isStudentTaxFiler";

        // Income Tax Paid

        private const string LabelParentIncomeTax = @"""Parent(s)' Income Taxes Paid""";
        private const string LabelStudentIncomeTax = @"""Student's Income Taxes Paid""";
        private const string LabelIndStudentIncomeTax = @"""Student and Spouse's Total Income Taxes Paid""";
        private const string ParamParentIncomeTax = "parentIncomeTax";
        private const string ParamStudentIncomeTax = "studentIncomeTax";
        private const string ParamIndStudentIncomeTax = "studentIncomeTax";

        // Untaxed Income And Benefits

        private const string LabelParentUntaxedIncomeAndBenefits = @"""Parent(s)' Untaxed Income and Benefits""";
        private const string LabelStudentUntaxedIncomeAndBenefits = @"""Student's Untaxed Income and Benefits""";
        private const string LabelIndStudentUntaxedIncomeAndBenefits
            = @"""Student and Spouse's Untaxed Income and Benefits""";
        private const string ParamParentUntaxedIncomeAndBenefits = "parentUntaxedIncomeAndBenefits";
        private const string ParamStudentUntaxedIncomeAndBenefits = "studentUntaxedIncomeAndBenefits";
        private const string ParamIndStudentUntaxedIncomeAndBenefits = "studentUntaxedIncomeAndBenefits";

        // Additional Financial Information

        private const string LabelParentAdditionalFinancialInfo = @"""Parent(s)' Additional Financial Information""";
        private const string LabelStudentAdditionalFinancialInfo = @"""Student's Additional Financial Information""";
        private const string LabelIndStudentAdditionalFinancialInfo = @"""Student and Spouse's Additional Financial Information""";
        private const string ParamParentAdditionalFinancialInfo = "parentAdditionalFinancialInfo";
        private const string ParamStudentAdditionalFinancialInfo = "studentAdditionalFinancialInfo";
        private const string ParamIndStudentAdditionalFinancialInfo = "studentAdditionalFinancialInfo";

        // Cash, Savings, Checking

        private const string LabelParentCashSavingsChecking = @"""Parent(s)' Cash, Savings, and Checking""";
        private const string LabelStudentCashSavingsChecking = @"""Student's Cash, Savings, and Checking""";
        private const string LabelIndStudentCashSavingsChecking
            = @"""Student and Spouse's Cash, Savings, and Checking""";
        private const string ParamParentCashSavingsChecking = "parentCashSavingsChecking";
        private const string ParamStudentCashSavingsChecking = "studentCashSavingsChecking";
        private const string ParamIndStudentCashSavingsChecking = "studentCashSavingsChecking";

        // Investment Net Worth

        private const string LabelParentInvestmentNetWorth = @"""Net Worth of Parent(s)' Investments""";
        private const string LabelStudentInvestmentNetWorth = @"""Net Worth of Student's Investments""";
        private const string LabelIndStudentInvestmentNetWorth = @"""Net Worth of Student and Spouse's Investments""";
        private const string ParamParentInvestmentNetWorth = "parentInvestmentNetWorth";
        private const string ParamStudentInvestmentNetWorth = "studentInvestmentNetWorth";
        private const string ParamIndStudentInvestmentNetWorth = "studentInvestmentNetWorth";

        // Business Farm Net Worth

        private const string LabelParentBusinessFarmNetWorth = @"""Net Worth of Parent(s)' Business and/or Investment Farm""";
        private const string LabelStudentBusinessFarmNetWorth = @"""Net Worth of Student's Business and/or Investment Farm""";
        private const string LabelIndStudentBusinessFarmNetWorth = @"""Net Worth of Student and Spouse's Business and/or Investment Farm""";
        private const string ParamParentBusinessFarmNetWorth = "parentBusinessFarmNetWorth";
        private const string ParamStudentBusinessFarmNetWorth = "studentBusinessFarmNetWorth";
        private const string ParamIndStudentBusinessFarmNetWorth = "studentBusinessFarmNetWorth";

        // Has Dependents

        private const string LabelIndStudentHasDep = @"""Student has Dependents""";
        private const string ParamIndStudentHasDep = "hasDependents";

        // Marital Status

        private const string ParamMaritalStatus = "maritalStatus";
        private const string LabelParentMaritalStatus = @"""Parent(s)' Marital Status""";
        private const string LabelIndStudentMaritalStatus = @"""Student's Marital Status""";

        // State of Residency

        private const string LabelStateOfResidency = @"""State of Residency""";
        private const string ParamStateOfResidency = "stateOfResidency";

        // Number in Household

        private const string LabelNumInHousehold = @"""Number in Household""";
        private const string ParamNumInHousehold = "numberInHousehold";

        // Number in College

        private const string LabelNumInCollege = @"""Number in College""";
        private const string ParamNumInCollege = "numberInCollege";

        // Age

        private const string LabelOldestParentAge = @"""Age of Oldest Parent""";
        private const string LabelIndStudentAge = @"""Student's Age""";
        private const string ParamOldestParentAge = "oldestParentAge";
        private const string ParamIndStudentAge = "studentAge";

        // Months of Enrollment

        private const string LabelMonthsOfEnrollment = @"""Months of Enrollment""";
        private const string ParamMonthsOfEnrollment = "monthsOfEnrollment";

        // Is Qualified for Simplified?

        private const string LabelIsQualifiedForSimplified = @"""Is Qualified for Simplified?""";
        private const string ParamIsQualifiedForSimplified = "isQualifiedForSimplified";
    }
}
